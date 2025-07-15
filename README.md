# Perguntador

**Perguntador** é uma aplicação ASP.NET Core MVC que integra com a API do HuggingFace para gerar conteúdo dinâmico com base em diferentes seleções do usuário. A aplicação permite:

- Criar perguntas de entrevista com base em um assunto e tópicos informados.
- Resumir textos de forma objetiva.
- Gerar código-fonte com base em uma descrição.

## 🚀 Funcionalidades

- Geração de perguntas para entrevistas técnicas por nível de senioridade.
- Resumo automático de textos fornecidos pelo usuário.
- Geração de trechos de código com base em descrições funcionais.

## 🧠 Tecnologia de IA Utilizada

- Integração com modelo de linguagem via API da [HuggingFace](https://huggingface.co/) (`deepseek-ai/DeepSeek-V3-0324`).

## ⚙️ Tecnologias

- ASP.NET Core MVC
- C#
- Newtonsoft.Json
- HTTPClient
- HuggingFace API

## 🔧 Como usar

### 1. Clone o repositório:   
   git clone https://github.com/seuusuario/perguntador.git

### 2. Adicione sua chave da HuggingFace na linha:
	link: https://huggingface.co/docs/inference-providers/index?javascript-clients=fetch
	client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "your_key");

### 3. Execute a aplicação via Visual Studio ou com o comando:
	dotnet run

**As funcionalidades em Docker deste projeto preveem a inclusão de um banco de dados MySQL nos containers. No entanto, essa funcionalidade ainda não foi completamente implementada. Por isso, recomenda-se executar o projeto diretamente, sem utilizar as instâncias Docker.**

## 📁 Estrutura do Projeto
- Controllers/HomeController.cs — Controlador principal com lógica de interação com o modelo de linguagem.
- Views/Home/Index.cshtml — Página principal para entrada de dados e visualização de resultados.
- Models/ErrorViewModel.cs — Modelo de erro para tratamento de exceções.

## 📌 Observações
A chave da API HuggingFace deve ser mantida em segredo. Idealmente, utilize appsettings.json ou variáveis de ambiente para isso.
O projeto atual utiliza chamadas síncronas à API e pode ser ajustado para melhorar desempenho e segurança.


## Prints
<img width="1598" height="786" alt="image" src="https://github.com/user-attachments/assets/72bcd870-d507-44c0-8e25-93f9f2468668" />
<img width="1598" height="786" alt="image" src="https://github.com/user-attachments/assets/91f3c251-341a-4d3b-9ac8-9324bee3b475" />
<img width="1598" height="786" alt="image" src="https://github.com/user-attachments/assets/765bdd45-84d3-41d7-9095-08c5df13fa59" />
