using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Itens.Data;
using Itens.Models;

namespace Itens.Controllers;

[ApiController]
[Route("api/v1/[controller]")] // Utiliza nome da classe: Items
public class ItemsController : ControllerBase
{
    private readonly AppDbContext _db;
    public ItemsController(AppDbContext db) => _db = db;

    // GET /api/v1/items
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetAll()
        => Ok(await _db.Itens.OrderBy(i => i.Id).ToListAsync());

    // GET /api/v1/items/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Item>> GetById(int id)
        => await _db.Itens.FindAsync(id) is { } item ? Ok(item) : NotFound();

    // POST /api/v1/items
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Item item)
    {
        _db.Itens.Add(item);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    // PUT /api/v1/items/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Item item)
    {
        item.Id = id;

        if (!await _db.Itens.AnyAsync(i => i.Id == id)) return NotFound();

        _db.Entry(item).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return Ok();
    }

    // DELETE /api/v1/items/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Itens.FindAsync(id);
        if (item is null) return NotFound();

        _db.Itens.Remove(item);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
