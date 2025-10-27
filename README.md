# RPG-DB-API

API REST e console interativo para gerenciamento de itens de RPG simples. Desenvolvido com ASP.NET Core 9, Entity Framework Core e SQLite.

---

## Funcionalidades

- Cadastro, listagem, edição e remoção de itens
- API REST com Swagger para testes
- Console interativo no terminal
- Banco de dados SQLite com migrações automáticas

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
## Como executar o projeto

```bash
# Restaurar pacotes
dotnet restore

# Criar manifesto de ferramentas e instalar CLI do EF
dotnet new tool-manifest
dotnet tool install dotnet-ef

# Criar e aplicar a migration
dotnet ef migrations add InitialCreate
dotnet ef database update

# Rodar o projeto
dotnet run

```
---
## Console interativo

Ao rodar o projeto com `dotnet run`, além da API REST, você verá no terminal um menu interativo para gerenciar os itens diretamente:

```text
== RPG Item Manager ==
Escolha uma opção:
1 - Cadastrar item
2 - Listar itens
3 - Atualizar item (por Id)
4 - Remover item (por Id)
0 - Sair

```
Pelo terminal você pode escolher a ação que deseja executar e o programa seguirá pelo o que você escolheu.

---
## Rotas da API

| Método | Rota                | Descrição               |
|--------|---------------------|--------------------------|
| GET    | `/api/v1/items`     | Lista todos os itens     |
| GET    | `/api/v1/items/{id}`| Busca item por Id        |
| POST   | `/api/v1/items`     | Cria um novo item        |
| PUT    | `/api/v1/items/{id}`| Atualiza item existente  |
| DELETE | `/api/v1/items/{id}`| Remove item por Id       |

## Exemplos de requisições

### Criar item (POST)

```http
POST /api/v1/items
Content-Type: application/json

{
  "nome": "Espada Flamejante",
  "raridade": "Épico",
  "preco": 2500
}
```
---
### Buscar item por ID
```http

GET /api/v1/items/1
```
---

### Atualizar item por ID
```http
PUT /api/v1/items/1
Content-Type: application/json

{
  "nome": "Espada Congelante",
  "raridade": "Lendário",
  "preco": 4000
}
```

### Remover item por ID
```http
DELETE /api/v1/items/1
```

Esses exemplos mostram como interagir com sua API usando ferramentas como Postman, Insomnia entre outros.
