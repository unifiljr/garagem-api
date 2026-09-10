# 🚗 GaragemAPI

**Faculdade:** Unifil
**Curso:** Engenharia de Software
**Disciplina:** Extensão VII

Sistema web para gerenciamento de estoque de veículos, permitindo cadastrar, listar, buscar, editar, vender e excluir veículos de uma garagem/concessionária.

🔗 **Aplicação em produção:** https://garagem-api-bbqq.onrender.com/index.html
📄 **Página do projeto (diagramas e documentação):** https://unifiljr.github.io/garagem-api/
💻 **Repositório:** https://github.com/unifiljr/garagem-api

> ⚠️ A aplicação está hospedada no plano gratuito do Render, que hiberna após 15 minutos sem uso. O primeiro acesso após um período de inatividade pode levar até 1 minuto para carregar.

---

## Visão Geral

O sistema permite:
- Cadastrar novos veículos (marca, modelo, ano, preço, status)
- Listar todos os veículos cadastrados
- Buscar veículos por marca ou modelo
- Editar informações de um veículo
- Marcar um veículo como vendido
- Excluir veículos do estoque
- Consultar valores de referência na tabela FIPE

## Arquitetura

O projeto segue uma arquitetura simples de API REST + frontend estático:

```
Navegador (HTML/CSS/JS)  →  API REST (ASP.NET Core)  →  Banco de Dados (SQLite)
```

- O **frontend** (arquivos em `wwwroot/`) é servido diretamente pelo ASP.NET Core como arquivos estáticos.
- O **backend** expõe endpoints REST em `/api/carros` para as operações de CRUD.
- O **banco de dados** SQLite é criado e populado automaticamente na inicialização da aplicação (via Entity Framework Core `EnsureCreated()`), incluindo os status pré-cadastrados (Disponível, Vendido, Pendente, Manutenção).

Diagramas detalhados de classes, entidade-relacionamento, casos de uso e prototipação de telas estão disponíveis na [página do projeto](https://unifiljr.github.io/garagem-api/).

## Tecnologias Utilizadas

**Backend:**
- C# / ASP.NET Core 8
- Entity Framework Core 8
- SQLite

**Frontend:**
- HTML5, CSS3, JavaScript (vanilla)
- Bootstrap 5

**Testes:**
- xUnit + EF Core InMemory (testes unitários do backend)
- Cypress (testes end-to-end do frontend)

**Hospedagem:**
- Render (API + frontend estático, via Docker)
- GitHub Pages (página de documentação e diagramas)

## Estrutura do Projeto

```
GaragemAPI/
├── GaragemAPI/                 → Projeto principal (API + frontend)
│   ├── Controllers/            → Endpoints da API
│   ├── Database/               → DbContext (Entity Framework)
│   ├── Models/                 → Entidades e DTOs
│   └── wwwroot/                → Páginas HTML/CSS/JS do frontend
├── GaragemAPI.Tests/            → Testes unitários (xUnit)
├── cypress-tests/               → Testes end-to-end (Cypress)
├── docs/                        → Página do GitHub Pages (diagramas)
├── Dockerfile                   → Configuração para deploy no Render
└── GaragemAPI.sln
```

## Como Configurar e Executar Localmente

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (apenas se for rodar os testes com Cypress)

### Executando o backend + frontend

```bash
cd GaragemAPI
dotnet restore
dotnet run
```

A aplicação sobe localmente em `https://localhost:7001` (a porta exata aparece no terminal ao rodar). O banco de dados SQLite (`garagem.db`) é criado automaticamente na primeira execução, já com os status pré-cadastrados.

Acesse no navegador: `https://localhost:7001/dashboard.html`

### Executando os testes do backend

```bash
dotnet test GaragemAPI.sln
```

### Executando os testes end-to-end (Cypress)

```bash
cd cypress-tests
npm install
npx cypress open
```

> A aplicação (`dotnet run`) precisa estar rodando antes de executar os testes do Cypress.

## Deploy

A aplicação é publicada automaticamente no **Render** a cada push na branch `main`, usando o `Dockerfile` presente na raiz do repositório:

1. O Render clona o repositório e builda a imagem Docker (build multi-stage: compila com o SDK do .NET e publica em uma imagem final mais leve).
2. A aplicação escuta na porta definida pela variável `ASPNETCORE_URLS`, configurada no `Dockerfile`.
3. Como o plano é gratuito, o banco SQLite utiliza armazenamento efêmero (os dados podem ser reiniciados quando a instância hiberna ou é reimplantada).

A página de documentação (diagramas e informações do projeto) é publicada separadamente via **GitHub Pages**, a partir da pasta `/docs` do repositório.

## Testes Realizados

- **Testes automatizados de backend:** cobrindo os endpoints de listagem, cadastro e exclusão de veículos, incluindo casos de sucesso e de erro (ex: exclusão de veículo inexistente).
- **Testes automatizados end-to-end:** cobrindo os fluxos de cadastro, busca e exclusão de veículos diretamente na interface.
- **Teste de usabilidade:** realizado com uma usuária real, testando o fluxo de cadastro de veículos. Resultado: tarefa classificada como "Muito fácil", sem dificuldades relatadas, destacando a organização e clareza da interface.

## Melhorias Realizadas na Etapa de Refinamento

Durante os testes, foram identificados e corrigidos os seguintes problemas:
- Ausência do endpoint `PUT` na API, que impedia as funcionalidades de edição e venda de veículos.
- Exibição incorreta da cor de status na lista de veículos, causada por uma referência a um campo inexistente no retorno da API.


