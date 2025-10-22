using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Itens.Data;
using Itens.Models;


var builder = WebApplication.CreateBuilder(args);

// Porta fixa (opcional, facilita testes)
builder.WebHost.UseUrls("http://localhost:5099");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=rpg.db"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

var webTask = app.RunAsync();
Console.WriteLine("API de Itens de RPG online em http://localhost:5099 (Swagger em /swagger)");
Console.WriteLine("== RPG Item Manager ==");

while (true)
{
    Console.WriteLine();
    Console.WriteLine("Escolha uma opção:");
    Console.WriteLine("1 - Cadastrar item");
    Console.WriteLine("2 - Listar itens");
    Console.WriteLine("3 - Atualizar item (por Id)");
    Console.WriteLine("4 - Remover item (por Id)");
    Console.WriteLine("0 - Sair");
    Console.Write("> ");

    var opt = Console.ReadLine();

    if (opt == "0") break;

    switch (opt)
    {
        case "1": await CreateItemAsync(); break;
        case "2": await ListItemsAsync(); break;
        case "3": await UpdateItemAsync(); break;
        case "4": await DeleteItemAsync(); break;
        default: Console.WriteLine("Opção inválida."); break;
    }
}

await app.StopAsync();
await webTask;

async Task CreateItemAsync()
{
    Console.Write("Nome do item: ");
    var nome = (Console.ReadLine() ?? "").Trim();

    Console.Write("Raridade (comum, raro, épico, lendário): ");
    var raridade = (Console.ReadLine() ?? "").Trim();

    Console.Write("Preço (em moedas de ouro): ");
    if (!decimal.TryParse(Console.ReadLine(), out var preco))
    {
        Console.WriteLine("Preço inválido.");
        return;
    }

    if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(raridade))
    {
        Console.WriteLine("Nome e raridade são obrigatórios.");
        return;
    }

    using var db = new AppDbContext();
    var item = new Item { Nome = nome, Raridade = raridade, Preco = preco };
    db.Itens.Add(item);
    await db.SaveChangesAsync();
    Console.WriteLine($"Item cadastrado com sucesso! Id: {item.Id}");
}

async Task ListItemsAsync()
{
    using var db = new AppDbContext();
    var itens = await db.Itens.OrderBy(i => i.Id).ToListAsync();

    if (itens.Count == 0) { Console.WriteLine("Nenhum item encontrado."); return; }

    Console.WriteLine("Id | Nome                 | Raridade     | Preço");
    foreach (var i in itens)
        Console.WriteLine($"{i.Id,2} | {i.Nome,-20} | {i.Raridade,-12} | {i.Preco,6}g");
}

async Task UpdateItemAsync()
{
    Console.Write("Informe o Id do item a atualizar: ");
    if (!int.TryParse(Console.ReadLine(), out var id)) { Console.WriteLine("Id inválido."); return; }

    using var db = new AppDbContext();
    var item = await db.Itens.FirstOrDefaultAsync(i => i.Id == id);
    if (item is null) { Console.WriteLine("Item não encontrado."); return; }

    Console.WriteLine($"Atualizando Id {item.Id}. Deixe em branco para manter.");
    Console.WriteLine($"Nome atual   : {item.Nome}");
    Console.Write("Novo nome    : ");
    var newNome = (Console.ReadLine() ?? "").Trim();

    Console.WriteLine($"Raridade atual: {item.Raridade}");
    Console.Write("Nova raridade : ");
    var newRaridade = (Console.ReadLine() ?? "").Trim();

    Console.WriteLine($"Preço atual  : {item.Preco}g");
    Console.Write("Novo preço    : ");
    var precoInput = Console.ReadLine();
    decimal? newPreco = null;
    if (decimal.TryParse(precoInput, out var p)) newPreco = p;

    if (!string.IsNullOrWhiteSpace(newNome)) item.Nome = newNome;
    if (!string.IsNullOrWhiteSpace(newRaridade)) item.Raridade = newRaridade;
    if (newPreco.HasValue) item.Preco = newPreco.Value;

    await db.SaveChangesAsync();
    Console.WriteLine("Item atualizado com sucesso.");
}

async Task DeleteItemAsync()
{
    Console.Write("Informe o Id do item a remover: ");
    if (!int.TryParse(Console.ReadLine(), out var id)) { Console.WriteLine("Id inválido."); return; }

    using var db = new AppDbContext();
    var item = await db.Itens.FirstOrDefaultAsync(i => i.Id == id);
    if (item is null) { Console.WriteLine("Item não encontrado."); return; }

    db.Itens.Remove(item);
    await db.SaveChangesAsync();
    Console.WriteLine("Item removido com sucesso.");
}
