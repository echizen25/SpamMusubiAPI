using Microsoft.AspNetCore.Mvc;
using SpamMusubiAPI.DTOs;
using SpamMusubiAPI.Repositories.Interfaces;

namespace SpamMusubiAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddOnsController : ControllerBase
{
    private readonly IAddOnsRepository _repo;
    public AddOnsController(IAddOnsRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _repo.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddOnDto dto)
    {
        var id = await _repo.CreateAsync(dto);
        dto.AddsOnId = id;
        return CreatedAtAction(nameof(Get), new { id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AddOnDto dto)
    {
        if (id != dto.AddsOnId) return BadRequest();
        var rows = await _repo.UpdateAsync(dto);
        return rows > 0 ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var rows = await _repo.DeleteAsync(id);
        return rows > 0 ? NoContent() : NotFound();
    }
}
