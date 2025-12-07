# 🚀 Lead Management System - Full Stack

Sistema completo de gerenciamento de leads desenvolvido com .NET Core 6 e React.

## 📋 Índice

- [Sobre o Projeto](#sobre-o-projeto)
- [Tecnologias](#tecnologias)
- [Arquitetura](#arquitetura)
- [Pré-requisitos](#pré-requisitos)
- [Instalação e Execução](#instalação-e-execução)
  - [1. Banco de Dados (Docker)](#1-banco-de-dados-docker)
  - [2. Backend (.NET)](#2-backend-net)
  - [3. Frontend (React)](#3-frontend-react)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Funcionalidades](#funcionalidades)
- [API Endpoints](#api-endpoints)
- [Testes](#testes)
- [Troubleshooting](#troubleshooting)

---

## 📖 Sobre o Projeto

Sistema de gerenciamento de leads que permite visualizar, aceitar e recusar leads de vendas. Desenvolvido seguindo as melhores práticas de arquitetura de software, incluindo Clean Architecture, DDD e CQRS.

### Principais Características

✅ Interface web moderna e responsiva  
✅ API RESTful com .NET Core 6  
✅ Arquitetura em camadas (Clean Architecture)  
✅ CQRS com MediatR  
✅ Domain-Driven Design (DDD)  
✅ Banco de dados SQL Server em container Docker  
✅ Testes unitários  
✅ Desconto automático de 10% para leads > R$ 500  
✅ Notificação por email (simulada)  

---

## 🛠️ Tecnologias

### Backend
- **.NET Core 6** - Framework principal
- **Entity Framework Core** - ORM
- **MediatR** - CQRS pattern
- **SQL Server 2019** - Banco de dados
- **xUnit** - Testes unitários
- **Swagger** - Documentação da API

### Frontend
- **React 18** - Biblioteca UI
- **JavaScript ES6+** - Linguagem
- **Lucide React** - Biblioteca de ícones
- **CSS3** - Estilização

### DevOps
- **Docker** - Containerização
- **Docker Compose** - Orquestração de containers

---

## 🏗️ Arquitetura

```
┌─────────────────────────────────────────────────────────┐
│                      Frontend (React)                    │
│                   http://localhost:3000                  │
└────────────────────────┬────────────────────────────────┘
                         │ HTTP/REST
                         ▼
┌─────────────────────────────────────────────────────────┐
│              Backend API (.NET Core 6)                   │
│              https://localhost:7150/api                  │
│                                                          │
│  ┌─────────────────────────────────────────────────┐    │
│  │  Controllers (Presentation Layer)                │   │
│  └──────────────┬───────────────────────────────────┘   │
│                 │                                       │
│  ┌──────────────▼───────────────────────────────────┐   │
│  │  Application Layer                               │
│  │  • Commands  • Queries  • Handlers               │   │
│  └──────────────┬───────────────────────────────────┘   │
│                 │                                       │
│  ┌──────────────▼───────────────────────────────────┐   │
│  │  Domain Layer (DDD)                              │   │
│  │  • Entities  • Events  • Rules                   │   │
│  └──────────────┬───────────────────────────────────┘   │
│                 │                                       │
│  ┌──────────────▼───────────────────────────────────┐   │
│  │  Infrastructure Layer                            │   │
│  │  • EF Core  • Repositories  • Services           │   │
│  └──────────────┬───────────────────────────────────┘   │
└─────────────────┼───────────────────────────────────────┘
                  │
                  ▼
┌─────────────────────────────────────────────────────────┐
│         SQL Server 2019 (Docker Container)              │
│              localhost:1433                              │
└─────────────────────────────────────────────────────────┘
```

---

## ✅ Pré-requisitos

Antes de começar, você precisa ter instalado:

- **Docker Desktop** - [Download](https://www.docker.com/products/docker-desktop)
- **.NET 6 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/6.0)
- **Node.js 16+** - [Download](https://nodejs.org/)
- **Git** - [Download](https://git-scm.com/)

### Verificar Instalações

```bash
# Verificar Docker
docker --version
docker-compose --version

# Verificar .NET
dotnet --version

# Verificar Node.js
node --version
npm --version
```

---

## 🚀 Instalação e Execução

### 1️⃣ Banco de Dados (Docker)

O projeto utiliza SQL Server 2019 em container Docker.

#### Passo 1: Navegar até a pasta do backend

```bash
cd LeadManagement.Solution
```

#### Passo 2: Iniciar o SQL Server com Docker Compose

```bash
docker-compose up -d
```

Este comando irá:
- ✅ Baixar a imagem do SQL Server 2019 (primeira vez)
- ✅ Criar um container com SQL Server
- ✅ Configurar a senha: `SqlServer@123`
- ✅ Expor a porta `1433`
- ✅ Criar um volume persistente para os dados

#### Passo 3: Verificar se o container está rodando

```bash
docker ps
```

Você deve ver algo como:
```
CONTAINER ID   IMAGE                                        STATUS          PORTS
abc123def456   mcr.microsoft.com/mssql/server:2019-latest   Up 10 seconds   0.0.0.0:1433->1433/tcp
```

#### Passo 4: Testar conexão com o banco

```bash
# Usando sqlcmd (se instalado)
sqlcmd -S localhost,1433 -U sa -P "SqlServer@123" -Q "SELECT @@VERSION"

# OU usando Docker
docker exec -it <container_id> /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "SqlServer@123" -Q "SELECT @@VERSION"
```

#### ⚙️ Comandos Úteis do Docker

```bash
# Parar o container
docker-compose down

# Parar e remover volumes (CUIDADO: apaga os dados!)
docker-compose down -v

# Ver logs do container
docker-compose logs -f

# Reiniciar o container
docker-compose restart

# Parar apenas o container
docker-compose stop

# Iniciar container já existente
docker-compose start
```

---

### 2️⃣ Backend (.NET)

#### Passo 1: Navegar até a pasta da API

```bash
cd src/LeadManagement.API
```

#### Passo 2: Verificar a Connection String

Abra o arquivo `appsettings.json` e verifique:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=LeadManagementDb;User Id=sa;Password=SqlServer@123;TrustServerCertificate=true"
  }
}
```

> ⚠️ **Importante:** A senha deve ser `SqlServer@123` (mesma do docker-compose.yml)

#### Passo 3: Restaurar pacotes NuGet

```bash
# Na pasta src/LeadManagement.API
dotnet restore
```

#### Passo 4: Aplicar Migrations (criar banco e tabelas)

```bash
# Criar migration (se ainda não existir)
dotnet ef migrations add InitialCreate --project ../LeadManagement.Infrastructure

# Aplicar migration no banco
dotnet ef database update --project ../LeadManagement.Infrastructure
```

Este comando irá:
- ✅ Criar o banco de dados `LeadManagementDb`
- ✅ Criar a tabela `Leads` com todos os campos
- ✅ Criar índices para performance
- ✅ Inserir dados de exemplo (seed data)

#### Passo 5: Executar a API

```bash
dotnet run
```

A API estará disponível em:
- 🌐 **HTTPS**: https://localhost:5001
- 🌐 **HTTP**: http://localhost:5000
- 📚 **Swagger**: https://localhost:5001/swagger

#### Passo 6: Testar a API

Abra o navegador em: **https://localhost:5001/swagger**

Ou use curl:
```bash
curl -k https://localhost:5001/api/leads/invited
```

---

### 3️⃣ Frontend (React)

#### Passo 1: Navegar até a pasta do frontend

```bash
cd lead-management-ui
```

#### Passo 2: Instalar dependências

```bash
npm install
```

#### Passo 3: Configurar a API

Abra o arquivo `src/services/api.js` e configure:

```javascript
// Para usar dados mock (desenvolvimento sem backend)
const USE_MOCK_DATA = true;

// Para conectar na API real
const USE_MOCK_DATA = false;
const API_BASE = 'https://localhost:5001/api';
```

#### Passo 4: Executar o frontend

```bash
npm start
```

A aplicação abrirá automaticamente em: **http://localhost:3000**

---

## 📁 Estrutura do Projeto

```
LeadManagement/
│
├── docker-compose.yml                 # Configuração do SQL Server (Mantido)
│
├── src/                               # Backend (Arquitetura em Camadas)
│   ├── API/                           # Camada de Apresentação (Controllers, appsettings, Program.cs)
│   │   ├── Controllers/
│   │   ├── Properties/
│   │   ├── appsettings.json
│   │   └── Program.cs
│   │
│   ├── Application/                   # Camada de Aplicação (Lógica de Negócio, DTOs, Serviços de Aplicação)
│   │   ├── DTOs/
│   │   ├── Interfaces/                # Interfaces de Serviços de Aplicação (e.g., IUserService)
│   │   └── Services/                  # Implementações de Serviços de Aplicação
│   │
│   ├── Communication/                 # Camada de Comunicação (Pode ser para mensageria, e-mails, etc.)
│   │
│   ├── Domain/                        # Camada de Domínio (Entidades, Regras, Repositórios)
│   │   ├── Entities/
│   │   ├── Enums/
│   │   └── Repositories/              # Interfaces de Repositórios (e.g., ILeadRepository)
│   │
│   └── Infrastructure/                # Camada de Infraestrutura (Implementação de Repositórios, Contexto de Dados)
│
├── tests/
│   └── LeadManagement.Tests/          # Testes Unitários
│       └── LeadManagementTests.cs
│
└── lead-management-ui/                # Frontend (Mantido)
    ├── public/
    └── src/
        ├── components/
        │   └── CartaoLead/
        │
        ├── rotas/
        │   └── routes.jsx
        │
        ├── screens/
        │   ├── TelaInvites/
        │   └── TelaInvitesAceitos/
        │
        ├── services/
        │   └── api.js
        │
        └── App.jsx
```

---

## 🎯 Funcionalidades

### Tab "Convidados" (Invited)

- ✅ Listagem de leads com status "New"
- ✅ Visualizar informações do lead:
  - Nome do contato
  - Data de criação
  - Localização
  - Categoria
  - ID
  - Descrição
  - Preço
- ✅ **Aceitar Lead**:
  - Aplica desconto de 10% se preço > R$ 500
  - Envia notificação para vendas@test.com
  - Move para tab "Aceitos"
- ✅ **Recusar Lead**:
  - Remove da listagem
  - Atualiza status no banco

### Tab "Aceitos" (Accepted)

- ✅ Listagem de leads aceitos
- ✅ Informações completas do contato:
  - Nome completo
  - Telefone
  - Email
  - Todas as informações da tab anterior

---

## 📡 API Endpoints

### Base URL
```
https://localhost:7150/api
```

### Endpoints Disponíveis

#### 1. Listar Leads Convidados
```http
GET /api/leads/invited
```

**Resposta:**
```json
[
  {
    "id": 1,
    "contactFirstName": "João",
    "contactLastName": "Silva",
    "contactPhone": "+55 11 98765-4321",
    "contactEmail": "joao.silva@email.com",
    "suburb": "São Paulo",
    "category": "Residencial",
    "description": "Reforma completa de cozinha",
    "price": 1500.00,
    "status": "New",
    "dateCreated": "2024-12-04T10:30:00Z"
  }
]
```

#### 2. Listar Leads Aceitos
```http
GET /api/leads/accepted
```

#### 3. Aceitar Lead
```http
POST /api/leads/{id}/accept
```

**Resposta:**
```json
{
  "success": true,
  "message": "Lead aceito com sucesso",
  "finalPrice": 1350.00
}
```

#### 4. Recusar Lead
```http
POST /api/leads/{id}/decline
```

**Resposta:**
```json
{
  "success": true,
  "message": "Lead recusado com sucesso"
}
```

---

## 🧪 Testes

### Executar Testes Unitários

```bash
# Navegar para a pasta de testes
cd tests/LeadManagement.Tests

# Executar todos os testes
dotnet test

# Executar com detalhes
dotnet test --logger "console;verbosity=detailed"

# Executar com cobertura
dotnet test /p:CollectCoverage=true
```

### Testes Implementados

- ✅ Testes de domínio (Lead Entity)
- ✅ Testes de regras de negócio
- ✅ Testes de desconto automático
- ✅ Testes de validações

---

## 🔧 Troubleshooting

### Problema: Docker não inicia o SQL Server

**Solução:**
```bash
# Ver logs do container
docker-compose logs sqlserver

# Verificar se a porta 1433 está livre
netstat -an | find "1433"

# Remover container e volume, depois recriar
docker-compose down -v
docker-compose up -d
```

---

### Problema: Erro de conexão com o banco

**Erro:** `A connection was successfully established with the server, but then an error occurred`

**Solução:**
```bash
# 1. Verificar se o container está rodando
docker ps

# 2. Testar conexão
docker exec -it <container_id> /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "SqlServer@123"

# 3. Verificar connection string no appsettings.json
# Deve ter: TrustServerCertificate=true
```

---

### Problema: Migrations não aplicam

**Erro:** `Unable to create an object of type 'LeadManagementDbContext'`

**Solução:**
```bash
# Garantir que está na pasta correta
cd src/LeadManagement.API

# Verificar se o projeto Infrastructure existe
dotnet ef migrations list --project ../LeadManagement.Infrastructure

# Remover migrations antigas e recriar
dotnet ef migrations remove --project ../LeadManagement.Infrastructure
dotnet ef migrations add InitialCreate --project ../LeadManagement.Infrastructure
dotnet ef database update --project ../LeadManagement.Infrastructure
```

---

### Problema: Frontend não conecta na API

**Erro:** `CORS policy: No 'Access-Control-Allow-Origin' header`

**Solução:**
1. Verificar se a API está rodando em https://localhost:7150
2. Abrir `Program.cs` e verificar CORS:
```csharp

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000") // origem permitida
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

app.UseCors("DevCors");
```
3. No `api.js` do frontend, usar a URL correta:
```javascript
const API_BASE = 'https://localhost:7150/api';
```

---

### Problema: Porta já em uso

**Backend (7150):**
```bash
# Ver o que está usando a porta
netstat -ano | findstr :7150

# Matar o processo (Windows)
taskkill /PID <PID> /F

# Ou mudar a porta no launchSettings.json
```

**Frontend (3000):**
```bash
# Windows
set PORT=3001 && npm start

# Linux/Mac
PORT=3001 npm start
```

**SQL Server (1433):**
```bash
# Parar o Docker
docker-compose down

# Ou mudar a porta no docker-compose.yml
ports:
  - "1434:1433"  # Mudar para 1434
```

---

## 📊 Dados de Teste

O sistema já vem com dados de exemplo:

| ID | Nome | Categoria | Preço | Status |
|----|------|-----------|-------|--------|
| 1 | João Silva | Residencial | R$ 1.500 | New |
| 2 | Maria Santos | Comercial | R$ 450 | New |
| 3 | Pedro Oliveira | Residencial | R$ 800 | New |
| 4 | Ana Costa | Industrial | R$ 1.350 | Accepted |

---

## 🎓 Padrões e Práticas Implementados

### Backend
- ✅ Clean Architecture
- ✅ Domain-Driven Design (DDD)
- ✅ CQRS com MediatR
- ✅ Repository Pattern
- ✅ Domain Events
- ✅ Dependency Injection
- ✅ Async/Await
- ✅ Entity Framework Core

### Frontend
- ✅ Componentes modulares
- ✅ Separação de responsabilidades
- ✅ CSS Modular
- ✅ Service Layer
- ✅ Estado gerenciado no componente raiz
- ✅ Props drilling controlado

---

## 📝 Configurações Importantes

### Connection Strings

#### Docker SQL Server (Recomendado)
```json
"Server=localhost,1433;Database=LeadManagementDb;User Id=sa;Password=SqlServer@123;TrustServerCertificate=true"
```

#### LocalDB
```json
"Server=(localdb)\\mssqllocaldb;Database=LeadManagementDb;Trusted_Connection=true"
```

#### SQL Server Express
```json
"Server=.\\SQLEXPRESS;Database=LeadManagementDb;Trusted_Connection=true"
```

---

## 🚀 Deploy

### Backend
```bash
dotnet publish -c Release -o ./publish
```

### Frontend
```bash
npm run build
```

Os arquivos estarão na pasta `build/`.

---

## 📞 Suporte

Para problemas ou dúvidas:

1. Verifique a seção [Troubleshooting](#troubleshooting)
2. Consulte os logs:
   - Backend: Console da API
   - Docker: `docker-compose logs`
   - Frontend: Console do navegador (F12)
3. Verifique o Swagger: https://localhost:7150/swagger

---

## 📄 Licença

Este projeto foi desenvolvido como parte de um desafio técnico Full Stack .NET.

---

## ✅ Checklist de Setup Completo

- [ ] Docker Desktop instalado e rodando
- [ ] .NET 6 SDK instalado
- [ ] Node.js 16+ instalado
- [ ] `docker-compose up -d` executado com sucesso
- [ ] SQL Server rodando (verificar com `docker ps`)
- [ ] Backend: `dotnet restore` executado
- [ ] Backend: migrations aplicadas (`dotnet ef database update`)
- [ ] Backend: API rodando em https://localhost:5001
- [ ] Frontend: `npm install` executado
- [ ] Frontend: Aplicação rodando em http://localhost:3000
- [ ] Swagger acessível em https://localhost:5001/swagger
- [ ] Dados de teste aparecendo na UI

---

**Desenvolvido para o Desafio Full Stack .NET**


**Data:** Dezembro 2025
