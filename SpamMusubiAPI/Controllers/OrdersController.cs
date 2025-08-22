using Microsoft.AspNetCore.Mvc;
using SpamMusubiAPI.DTOs;
using SpamMusubiAPI.Repositories.Interfaces;

namespace SpamMusubiAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersRepository _repo;
    public OrdersController(IOrdersRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null, [FromQuery] string? status = null)
        => Ok(await _repo.GetAllAsync(from, to, status));

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var order = await _repo.GetByIdAsync(id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpGet("by-day")]
    public async Task<IActionResult> GetByDay([FromQuery] DateTime date)
        => Ok(await _repo.GetByDayAsync(date));

    [HttpGet("calendar/summary")]
    public async Task<IActionResult> CalendarSummary([FromQuery] DateTime from, [FromQuery] DateTime to)
        => Ok(await _repo.GetCalendarSummaryAsync(from, to));

    [HttpPost]
    public async Task<IActionResult> Create(OrderDto dto)
    {
        var id = await _repo.CreateAsync(dto);
        dto.OrderId = id;
        return CreatedAtAction(nameof(Get), new { id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, OrderDto dto)
    {
        if (id != dto.OrderId) return BadRequest();
        var rows = await _repo.UpdateAsync(dto);
        return rows > 0 ? NoContent() : NotFound();
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
    {
        var rows = await _repo.UpdateStatusAsync(id, status);
        return rows > 0 ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var rows = await _repo.DeleteAsync(id);
        return rows > 0 ? NoContent() : NotFound();
    }
}
