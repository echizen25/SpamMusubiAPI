using Microsoft.AspNetCore.Mvc;
using SpamMusubiAPI.DTOs;
using SpamMusubiAPI.Repositories.Interfaces;

namespace SpamMusubiAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModeOfPaymentController : ControllerBase
{
    private readonly IMopRepository _repo;
    public ModeOfPaymentController(IMopRepository repo) => _repo = repo;

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
    public async Task<IActionResult> Create(MopDto dto)
    {
        var id = await _repo.CreateAsync(dto);
        dto.MopId = id;
        return CreatedAtAction(nameof(Get), new { id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MopDto dto)
    {
        if (id != dto.MopId) return BadRequest();
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
