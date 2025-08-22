using Dapper;
using System.Data;
using SpamMusubiAPI.DTOs;
using SpamMusubiAPI.Repositories.Interfaces;

namespace SpamMusubiAPI.Repositories;

public class AddOnsRepository : IAddOnsRepository
{
    private readonly IDbConnection _db;
    public AddOnsRepository(IDbConnection db) => _db = db;

    public async Task<IEnumerable<AddOnDto>> GetAllAsync()
    {
        var sql = "SELECT adds_on_id AS AddsOnId, add_ons AS AddOns, price FROM adds_on ORDER BY add_ons";
        return await _db.QueryAsync<AddOnDto>(sql);
    }

    public async Task<AddOnDto?> GetByIdAsync(int id)
    {
        var sql = "SELECT adds_on_id AS AddsOnId, add_ons AS AddOns, price FROM adds_on WHERE adds_on_id=@Id";
        return await _db.QueryFirstOrDefaultAsync<AddOnDto>(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(AddOnDto dto)
    {
        var sql = "INSERT INTO adds_on (add_ons, price) VALUES (@AddOns, @Price); SELECT CAST(SCOPE_IDENTITY() AS INT);";
        return await _db.ExecuteScalarAsync<int>(sql, dto);
    }

    public async Task<int> UpdateAsync(AddOnDto dto)
    {
        var sql = "UPDATE adds_on SET add_ons=@AddOns, price=@Price WHERE adds_on_id=@AddsOnId";
        return await _db.ExecuteAsync(sql, dto);
    }

    public async Task<int> DeleteAsync(int id)
    {
        var sql = "DELETE FROM adds_on WHERE adds_on_id=@Id";
        return await _db.ExecuteAsync(sql, new { Id = id });
    }
}
