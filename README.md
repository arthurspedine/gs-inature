# iNature 🌱

Uma plataforma digital voltada para sustentabilidade e prevenção de desastres naturais.

## 📋 Sobre o Projeto

O **iNature** é uma plataforma que permite aos usuários:

* 📍 Verificar a **probabilidade de enchentes** na sua região
* 🚨 Reportar **enchentes**, **queimadas**, **desabamentos**
* 📰 Ler e publicar **notícias sustentáveis** (para perfis do tipo `JORNALISTA`)
* 📊 Acompanhar dados e reports de risco de forma clara, acessível e útil

Tudo isso em um só lugar, com foco em **prevenção, conscientização e engajamento social**.

## 🛠️ Tecnologias Utilizadas

- **Backend**: ASP.NET Core Web API
- **Autenticação**: JWT Bearer Token
- **Autorização**: Role-based (JORNALISTA, COMUM)
- **Banco de Dados**: Oracle Entity Framework Core

## 🛢️ Diagrama de Classes
![image](https://github.com/user-attachments/assets/2942e064-b632-4b10-a81d-4ed04f683a24)

## 🚀 Desenvolvimento

### Pré-requisitos

- .NET 6.0 ou superior
- Oracle Database
- Visual Studio 2022 ou VS Code

### Configuração do Ambiente

1. Clone o repositório:
```bash
git clone https://github.com/arthurspedine/gs-inature.git
cd gs-inature
```

2. Configure a string de conexão no `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "OracleConnection": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=URL_BANCO;"
  },
  "Jwt": {
    "Key": "KEY_JWT",
    "Issuer": "ISSUER_JWT",
    "Audience": "AUDIENCE_JWT",
    "ExpireMinutes": 10080
  },
}
```

3. Execute as migrações do banco de dados:
```bash
dotnet ef database update
```

4. Execute o projeto:
```bash
dotnet run
```

A API estará disponível em `https://localhost:5186`.

## 📡 Endpoints da API

### 🔐 Autenticação (`/api/Usuario`)

#### Registrar Usuário
- **POST** `/api/Usuario`
- **Descrição**: Registra um novo usuário na plataforma
- **Body**:
```json
{
  "nome": "João Silva",
  "email": "joao@email.com",
  "senha": "senha123456",
  "cargo": "COMUM"
}
```
- **Cargos disponíveis**: `COMUM`, `JORNALISTA`

#### Login
- **POST** `/api/Usuario/login`
- **Descrição**: Autentica um usuário e retorna o token JWT
- **Body**:
```json
{
  "email": "joao@email.com",
  "senha": "senha123456"
}
```

### 📰 Notícias (`/api/Noticia`)

#### Listar Todas as Notícias
- **GET** `/api/Noticia`
- **Descrição**: Lista todas as notícias publicadas
- **Autenticação**: Requerida
- **Headers**: `Authorization: Bearer {token}`

#### Buscar Notícia por ID
- **GET** `/api/Noticia/{id}`
- **Descrição**: Retorna uma notícia específica
- **Autenticação**: Requerida
- **Headers**: `Authorization: Bearer {token}`

#### Listar Minhas Notícias
- **GET** `/api/Noticia/minhas`
- **Descrição**: Lista as notícias do jornalista autenticado
- **Autenticação**: Requerida (Role: JORNALISTA)
- **Headers**: `Authorization: Bearer {token}`

#### Criar Notícia
- **POST** `/api/Noticia`
- **Descrição**: Cria uma nova notícia
- **Autenticação**: Requerida (Role: JORNALISTA)
- **Headers**: `Authorization: Bearer {token}`
- **Body**:
```json
{
  "titulo": "Título da Notícia (máx. 200 caracteres)",
  "resumo": "Resumo da notícia (máx. 500 caracteres)",
  "corpo": "Corpo completo da notícia (mín. 10 caracteres)"
}
```

#### Atualizar Notícia
- **PUT** `/api/Noticia/{id}`
- **Descrição**: Atualiza uma notícia existente
- **Autenticação**: Requerida (Role: JORNALISTA)
- **Headers**: `Authorization: Bearer {token}`
- **Body**: Mesmo formato do POST

#### Excluir Notícia
- **DELETE** `/api/Noticia/{id}`
- **Descrição**: Exclui uma notícia
- **Autenticação**: Requerida (Role: JORNALISTA)
- **Headers**: `Authorization: Bearer {token}`

### 🚨 Reports (`/api/Report`)

#### Criar Report
- **POST** `/api/Report`
- **Descrição**: Cria um novo report
- **Autenticação**: Requerida
- **Headers**: `Authorization: Bearer {token}`
- **Body**:
```json
{
  "titulo": "Título do Report (máx. 100 caracteres)",
  "corpo": "Descrição detalhada",
  "tipo": "ENCHENTE",
  "cidade": "São Paulo",
  "bairro": "Vila Madalena",
  "logradouro": "Rua Exemplo",
  "numero": 123
}
```
- **Tipos disponíveis**: `ENCHENTE`, `QUEIMADA`, `DESABAMENTO`

#### Listar Todos os Reports
- **GET** `/api/Report`
- **Descrição**: Lista todos os reports publicados
- **Autenticação**: Requerida
- **Headers**: `Authorization: Bearer {token}`

#### Listar Meus Reports
- **GET** `/api/Report/meus`
- **Descrição**: Lista os reports do usuário autenticado
- **Autenticação**: Requerida
- **Headers**: `Authorization: Bearer {token}`

### ✅ Confirmações de Report (`/api/ReportConfirmacao`)

#### Confirmar Report
- **POST** `/api/ReportConfirmacao/{id}`
- **Descrição**: Confirma um report existente
- **Autenticação**: Requerida
- **Headers**: `Authorization: Bearer {token}`

#### Remover Confirmação
- **DELETE** `/api/ReportConfirmacao/{id}`
- **Descrição**: Remove uma confirmação de report
- **Autenticação**: Requerida
- **Headers**: `Authorization: Bearer {token}`

## 🧪 Testes

### Testando com Postman/Insomnia

#### 1. Configuração Inicial

1. Importe a collection com a base URL: `http://localhost:5186/api`
2. Configure uma variável de ambiente `{{token}}` para armazenar o JWT

#### 2. Fluxo de Testes Recomendado

**Passo 1: Registrar Usuários**
```http
POST /api/Usuario
Content-Type: application/json

{
  "nome": "João Jornalista",
  "email": "joao@jornalista.com",
  "senha": "senha123456",
  "cargo": "JORNALISTA"
}
```

```http
POST /api/Usuario
Content-Type: application/json

{
  "nome": "Maria",
  "email": "maria@email.com",
  "senha": "senha123456",
  "cargo": "COMUM"
}
```

**Passo 2: Fazer Login**
```http
POST /api/Usuario/login
Content-Type: application/json

{
  "email": "joao@jornalista.com",
  "senha": "senha123456"
}
```

**Passo 3: Criar Notícia (como JORNALISTA)**
```http
POST /api/Noticia
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "titulo": "Nova Técnica de Reflorestamento Urbano",
  "resumo": "Pesquisadores desenvolvem método inovador para acelerar o crescimento de árvores em centros urbanos, contribuindo para a redução de enchentes.",
  "corpo": "Um estudo recente da Universidade de São Paulo apresentou uma nova técnica de reflorestamento urbano que promete revolucionar a forma como lidamos com enchentes nas grandes cidades. A metodologia combina espécies nativas com técnicas de plantio acelerado, resultando em um crescimento 40% mais rápido das árvores plantadas."
}
```

**Passo 4: Criar Report (como COMUM)**
```http
POST /api/Report
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "titulo": "Enchente na Marginal",
  "corpo": "Enchente de grande proporção na Marginal Tietê, causando transtornos no trânsito e alagamento de residências próximas.",
  "tipo": "ENCHENTE",
  "cidade": "São Paulo",
  "bairro": "Vila Leopoldina",
  "logradouro": "Marginal Tietê",
  "numero": 1000
}
```

**Passo 5: Confirmar Report**
```http
POST /api/ReportConfirmacao/1
Authorization: Bearer {{token}}
```

### Casos de Teste Importantes

#### Teste de Validação de Dados
- Tente registrar usuário com tipo inválido
- Tente criar report com tipo inválido
- Tente registrar usuário com email inválido

#### Teste de Autorização
- Tente criar notícia sem ser JORNALISTA
- Tente acessar endpoints protegidos sem token
- Tente editar notícia de outro usuário

#### Teste de Fluxo Completo
1. Registrar → Login → Criar Report → Confirmar Report
2. Registrar Jornalista → Login → Criar Notícia → Listar Notícias

## 📊 Códigos de Status HTTP

- **200 OK**: Requisição bem-sucedida
- **201 Created**: Recurso criado com sucesso
- **204 No Content**: Recurso excluído com sucesso
- **400 Bad Request**: Dados inválidos ou erro de validação
- **401 Unauthorized**: Token inválido ou ausente
- **403 Forbidden**: Usuário não tem permissão para a ação
- **404 Not Found**: Recurso não encontrado
- **500 Internal Server Error**: Erro interno do servidor

## 📝 Notas Importantes

- Todos os endpoints que requerem autenticação devem incluir o header `Authorization: Bearer {token}`
- O token JWT é obtido através do endpoint de login
- Apenas usuários com role `JORNALISTA` podem criar, editar e excluir notícias
- Reports podem ser criados por qualquer usuário autenticado
- Confirmações de reports são anônimas e podem ser feitas por qualquer usuário autenticado
