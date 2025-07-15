using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Perguntador.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Reflection;
using System.Text;

namespace Perguntador.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> GerarConteudo(
    int selecao,
    string assuntoInput = "",
    string topicoInput = "",
    string textoResumo = "",
    string descricaoCodigo = "")
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri("https://router.huggingface.co/v1/chat/completions")
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "your_key");

                string prompt;

                switch (selecao)
                {
                    case 1: // Perguntas de entrevista
                        prompt = $@"
                    Crie 5 perguntas para entrevista de emprego, divididas por n�vel de senioridade.

                    - Assunto: {assuntoInput}
                    - T�picos: {topicoInput}

                    Formato de sa�da:
                    T�pico
                    Senioridade
                    1. Pergunta 1

                    2. Pergunta 2

                    ...

                    Somente as perguntas. Sem introdu��es ou explica��es adicionais.
                    ";
                        break;

                    case 2: // Resumo de texto
                        prompt = $@"
                    Resuma o seguinte texto de forma objetiva e clara, mantendo os pontos principais:

                    Texto:
                    ""{textoResumo}""

                    Formato:
                    - Resumo com no m�ximo 5 frases.
                    ";
                        break;

                    case 3: // Gera��o de c�digo
                        prompt = $@"
                    Gere um c�digo baseado na descri��o abaixo. Use boas pr�ticas, coment�rios e a linguagem mais adequada.

                    Descri��o:
                    {descricaoCodigo}

                    Formato:
                    - C�digo formatado
                    - Coment�rios explicativos
                    ";
                        break;

                    default:
                        return View("~/Views/Home/Index.cshtml", model: "Sele��o inv�lida.");
                }

                var reqBody = new
                {
                    model = "deepseek-ai/DeepSeek-V3-0324",
                    messages = new List<object>
            {
                new { role = "user", content = prompt.Trim() }
            }
                };

                var reqMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    Content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, MediaTypeNames.Application.Json)
                };

                var response = await client.SendAsync(reqMessage).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var apiResJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var apiRes = JsonConvert.DeserializeObject<apiResponseModel>(apiResJson);

                var content = apiRes.choices[0].message.content;
                var formattedContent = content
                    .Replace("###", "\n\n")
                    .Replace("---", "\n\n")
                    .Replace("\n", Environment.NewLine)
                    .Trim();
                
                return View("~/Views/Home/Index.cshtml", model: formattedContent);
            }
            catch (Exception ex)
            {
                return View("~/Views/Home/Index.cshtml", model: "Erro ao gerar conte�do: " + ex.Message);
            }
        }



        #region Models
        class apiResponseModel
        {
            public string id { get; set; }
            public int created { get; set; }
            public string model { get; set; }
            public List<choice> choices { get; set; }
        }

        class choice
        {
            public int index { get; set; }
            public message message { get; set; }
        }

        class message
        {
            public string role { get; set; }
            public string content { get; set; }
        }
        #endregion
    }
}
