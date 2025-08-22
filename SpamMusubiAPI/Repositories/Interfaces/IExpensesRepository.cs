using SpamMusubiAPI.DTOs;

namespace SpamMusubiAPI.Repositories.Interfaces;

public interface IExpensesRepository
{
    Task<IEnumerable<ExpenseDto>> GetAllAsync(DateTime? from = null, DateTime? to = null);
    Task<ExpenseDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(ExpenseDto dto);
    Task<int> UpdateAsync(ExpenseDto dto);
    Task<int> DeleteAsync(int id);
}
