using Dapper;
using System.Data;
using SpamMusubiAPI.DTOs;
using SpamMusubiAPI.Repositories.Interfaces;

namespace SpamMusubiAPI.Repositories;

public class ExpensesRepository : IExpensesRepository
{
    private readonly IDbConnection _db;
    public ExpensesRepository(IDbConnection db) => _db = db;

    public async Task<IEnumerable<ExpenseDto>> GetAllAsync(DateTime? from = null, DateTime? to = null)
    {
        var sql = @"SELECT expense_id AS ExpenseId, expense_date AS ExpenseDate, category, description, amount
                    FROM expenses
                    WHERE (@from IS NULL OR expense_date >= @from)
                      AND (@to IS NULL OR expense_date < DATEADD(day, 1, @to))
                    ORDER BY expense_date DESC;";
        return await _db.QueryAsync<ExpenseDto>(sql, new { from, to });
    }

    public async Task<ExpenseDto?> GetByIdAsync(int id)
    {
        var sql = @"SELECT expense_id AS ExpenseId, expense_date AS ExpenseDate, category, description, amount
                    FROM expenses WHERE expense_id=@Id";
        return await _db.QueryFirstOrDefaultAsync<ExpenseDto>(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(ExpenseDto dto)
    {
        var sql = "INSERT INTO expenses (expense_date, category, description, amount) VALUES (@ExpenseDate, @Category, @Description, @Amount); SELECT CAST(SCOPE_IDENTITY() AS INT);";
        return await _db.ExecuteScalarAsync<int>(sql, dto);
    }

    public async Task<int> UpdateAsync(ExpenseDto dto)
    {
        var sql = "UPDATE expenses SET expense_date=@ExpenseDate, category=@Category, description=@Description, amount=@Amount WHERE expense_id=@ExpenseId";
        return await _db.ExecuteAsync(sql, dto);
    }

    public async Task<int> DeleteAsync(int id)
    {
        var sql = "DELETE FROM expenses WHERE expense_id=@Id";
        return await _db.ExecuteAsync(sql, new { Id = id });
    }
}
