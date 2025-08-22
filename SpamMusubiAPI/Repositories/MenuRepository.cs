using Dapper;
using System.Data;
using SpamMusubiAPI.DTOs;
using SpamMusubiAPI.Repositories.Interfaces;

namespace SpamMusubiAPI.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly IDbConnection _db;
    public MenuRepository(IDbConnection db) => _db = db;

    public async Task<IEnumerable<MenuDto>> GetAllAsync()
    {
        var sql = "SELECT menu_id AS MenuId, menu AS Menu, price FROM main_menu ORDER BY menu";
        return await _db.QueryAsync<MenuDto>(sql);
    }

    public async Task<MenuDto?> GetByIdAsync(int id)
    {
        var sql = "SELECT menu_id AS MenuId, menu AS Menu, price FROM main_menu WHERE menu_id = @Id";
        return await _db.QueryFirstOrDefaultAsync<MenuDto>(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(MenuDto dto)
    {
        var sql = "INSERT INTO main_menu (menu, price) VALUES (@Menu, @Price); SELECT CAST(SCOPE_IDENTITY() AS INT);";
        return await _db.ExecuteScalarAsync<int>(sql, dto);
    }

    public async Task<int> UpdateAsync(MenuDto dto)
    {
        var sql = "UPDATE main_menu SET menu=@Menu, price=@Price WHERE menu_id=@MenuId";
        return await _db.ExecuteAsync(sql, dto);
    }

    public async Task<int> DeleteAsync(int id)
    {
        var sql = "DELETE FROM main_menu WHERE menu_id=@Id";
        return await _db.ExecuteAsync(sql, new { Id = id });
    }
}
