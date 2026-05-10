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

- .NET SDK 10 instalado

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

- o banco SQLite local e criado automaticamente
- as 30 perguntas e alternativas sao inseridas via seed
- o Swagger abre para teste dos endpoints

Se o navegador nao abrir sozinho, acesse:

- `http://localhost:5095/swagger`
- `https://localhost:7159/swagger`

## Observacao

Nao e necessario cadastrar perguntas manualmente. O sistema ja sobe com as perguntas seedadas no banco.
