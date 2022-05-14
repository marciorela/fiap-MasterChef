# fiap-MasterChef
Trabalho da FIAP sobre crud de Receitas

A solução é composta por dois projetos principais
- Web
- Api

O projeto de api fornece o suporte necessário para a aplicação Web. As configurações necessárias estão em appsettings.json:

```
{
  "ConnectionStrings": {
    "SqlServer": "<string de conexão com o banco de dados SQL Server>",
  },
  "Folders": {
    "Photo": "<caminho onde serão armazenados os arquivos de foto>"
  },
  "Token": {
    "SecretKey": "<chave de criptografia do token>",
    "TempoExpiracaoTokenMinutos": 1440
  },
  "Auth": {
    "ClientId": "<dados de autorização - comparado com os dados enviados pelo cliente>",
    "Secret": "<dados de autorização - comparado com os dados enviados pelo cliente>"
  }
}
  ```

O projeto web utiliza a api para o CRUD de receitas, precisando somente do local da api e configurações de token:

```
{
  "apiAddress": "<endereco da api>",
  "Auth": {
    "ClientId": "<dados de autorização - comparado com os dados armazenados no servidor>",
    "Secret": "<dados de autorização - comparado com os dados armazenados no servidor>"
  },
}
```

