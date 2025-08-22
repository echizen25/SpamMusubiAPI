using SpamMusubiAPI.DTOs;

namespace SpamMusubiAPI.Repositories.Interfaces;

public interface IOrdersRepository
{
    Task<IEnumerable<OrderDto>> GetAllAsync(DateTime? from = null, DateTime? to = null, string? status = null);
    Task<OrderDto?> GetByIdAsync(int id);
    Task<IEnumerable<OrderDto>> GetByDayAsync(DateTime date);
    Task<IEnumerable<(DateTime Day, int Count, decimal Total)>> GetCalendarSummaryAsync(DateTime from, DateTime to);
    Task<int> CreateAsync(OrderDto dto);
    Task<int> UpdateAsync(OrderDto dto);
    Task<int> DeleteAsync(int id);
    Task<int> UpdateStatusAsync(int id, string status);
}
