# RPG-DB-API

Sistema simples para gerenciamento de itens de RPG via API REST e console interativo. Desenvolvido com ASP.NET Core, Entity Framework e SQLite.

---

## Funcionalidades

- Cadastro de itens com nome, raridade e preço
- Listagem, edição e remoção de itens
- API REST com Swagger para testes
- Console interativo para uso direto no terminal
- Banco de dados SQLite com migrações via EF Core

---

## Modelo de Item

```csharp
public class Item
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Raridade { get; set; } // Comum, Raro, Épico, Lendário
    public decimal Preco { get; set; }   // Em moedas de ouro
}
```
---

## Raridades disponíveis

| Código | Raridade  |
|--------|-----------|
| 1      | Comum     |
| 2      | Raro      |
| 3      | Épico     |
| 4      | Lendário  |

---

## Instalação de pacotes e configuração do ambiente

```bash
# Instalar pacotes do Entity Framework Core
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design

# Criar manifesto de ferramentas e instalar o CLI do EF
dotnet new tool-manifest
dotnet tool install dotnet-ef

# Após criar o código, rodar uma vez para gerar e aplicar a migration
dotnet ef migrations add InitialCreate
dotnet ef database update

# Adicionar biblioteca para documentação da API (Swagger)
dotnet add package Swashbuckle.AspNetCore

# Rodar o projeto
dotnet run

