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

## Console interativo

Ao rodar o projeto com `dotnet run`, você verá o seguinte menu:

```text
== RPG Item Manager ==
Escolha uma opção:
1 - Cadastrar item
2 - Listar itens
3 - Atualizar item (por Id)
4 - Remover item (por Id)
0 - Sair
```
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
```
---
## Rotas da API

| Método | Rota                | Descrição               |
|--------|---------------------|--------------------------|
| GET    | `/api/v1/items`     | Lista todos os itens     |
| GET    | `/api/v1/items/{id}`| Busca item por Id        |
| POST   | `/api/v1/items`     | Cria um novo item        |
| PUT    | `/api/v1/items/{id}`| Atualiza item existente  |
| DELETE | `/api/v1/items/{id}`| Remove item por Id       |

---
## Exemplos de requisições

### 🔹 Criar item (POST)

```http
POST /api/v1/items
Content-Type: application/json

{
  "nome": "Espada Flamejante",
  "raridade": "Épico",
  "preco": 250.00
}
```
### Buscar item por Id (GET)
```http
GET /api/v1/items/1
```
### Atualizar item (PUT)
```http
PUT /api/v1/items/1
Content-Type: application/json

{
  "nome": "Espada Congelante",
  "raridade": "Lendário",
  "preco": 400.00
}
```
### Remover item (DELETE)
```http
DELETE /api/v1/items/1
```

Esses exemplos mostram como interagir com sua API usando ferramentas como Postman.
