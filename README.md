# Quiz Sergipe API

API em ASP.NET Core MVC + Entity Framework Core para um quiz sobre Sergipe.

## O que nao vai para o GitHub

O projeto ignora arquivos que nao devem ser versionados, como:

- `bin/` e `obj/`
- banco SQLite local (`*.db`, `*.db-shm`, `*.db-wal`)
- logs (`*.log`)
- arquivos locais da IDE (`.vs/`, `.vscode/`, `*.user`, `*.suo`)
- arquivos locais de ambiente e segredo

## Como clonar e rodar

Requisitos:

- .NET SDK 9 ou 10 instalado

Passos:

1. Clonar o repositorio:

```bash
git clone <URL_DO_REPOSITORIO>
cd quizsergipe-api
```

2. Restaurar os pacotes:

```bash
dotnet restore
```

3. Rodar a API:

```bash
dotnet run
```

## O que acontece ao rodar

- em desenvolvimento, o banco SQLite local e criado automaticamente
- as 30 perguntas e alternativas sao inseridas via seed
- o Swagger abre para teste dos endpoints

Se o navegador nao abrir sozinho, acesse:

- `http://localhost:5095/swagger`
- `https://localhost:7159/swagger`

## Observacao

Nao e necessario cadastrar perguntas manualmente. O sistema ja sobe com as perguntas seedadas no banco.

## CORS

As origens permitidas ficam em `Cors:AllowedOrigins`.

- desenvolvimento: `appsettings.Development.json`
- producao: `appsettings.Production.json` ou variaveis de ambiente

Exemplo de variavel de ambiente:

```bash
Cors__AllowedOrigins__0=https://meu-frontend.com
```

## Banco de dados

O provider fica em `Database:Provider`.

- `Sqlite` para desenvolvimento local
- `PostgreSql` para producao

Connection strings:

- `ConnectionStrings__QuizDbSqlite`
- `ConnectionStrings__QuizDbPostgreSql`

Exemplo para PostgreSQL:

```bash
Database__Provider=PostgreSql
ConnectionStrings__QuizDbPostgreSql=Host=localhost;Port=5432;Database=quizsergipe;Username=postgres;Password=postgres
```

## Migrations

Para criar ou aplicar migrations do PostgreSQL:

```bash
dotnet ef migrations add InitialPostgreSql
dotnet ef database update
```

## Deploy no Render

O projeto esta preparado para Render com:

- `Dockerfile` na raiz
- `render.yaml` na raiz
- banco Render Postgres referenciado automaticamente
- `health check` em `/health`

Passos:

1. Suba este repositorio para o GitHub.
2. No Render, clique em `New` > `Blueprint`.
3. Conecte o repositorio.
4. O Render vai ler o `render.yaml` e criar:
   - um Web Service `quizsergipe-api`
   - um banco `quizsergipe-db`
5. No fluxo de criacao, preencha o valor da variavel `Cors__AllowedOrigins__0` com a URL do seu frontend.
6. Conclua o deploy.

Observacoes:

- o backend sobe em `Production`
- o app usa `Database__Provider=PostgreSql`
- a connection string vem do proprio banco criado no Render
- em PostgreSQL, a aplicacao roda `Database.Migrate()` no startup

Fontes oficiais Render:

- Blueprints: https://render.com/docs/blueprint-spec
- Variaveis de ambiente: https://render.com/docs/configure-environment-variables
- Web Services: https://render.com/docs/web-services
