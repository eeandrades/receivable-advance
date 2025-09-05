# ReceivableAdvance API

API para gestão de solicitações de antecipação de valores, permitindo que criadores solicitem parte de seus recebíveis futuros antes do prazo, mediante taxa.

**Tecnologias:** .NET 9, Minimal API, C# 13, SQLite (Dapper), Clean Architecture / DDD, Swagger/OpenAPI, xUnit/Moq para testes.

**Estrutura do projeto:**
```

## Banco de dados
Existe um banco de dados template (`receivable-advance-db.sqlite`) na pasta `src\ReceivableAdvance.Setup\Data`. 
Ele será copiado automaticamente junto com os binários no build do projeto

## Como executar

1. No diretório raíz, execute o comando 

```bash
dotnet build
```
2. Em seguida execute o comando
```bash
dotnet run --project ./src/ReceivableAdvance.WebApi/ReceivableAdvance.WebApi.csproj --urls "http://localhost:2322"
```
3. Para acessar o swagger , abra o navegador e vá para:
```
http://localhost:2322/swagger/index.html
```

