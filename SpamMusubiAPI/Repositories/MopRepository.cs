using Dapper;
using System.Data;
using SpamMusubiAPI.DTOs;
using SpamMusubiAPI.Repositories.Interfaces;

namespace SpamMusubiAPI.Repositories;

public class MopRepository : IMopRepository
{
    private readonly IDbConnection _db;
    public MopRepository(IDbConnection db) => _db = db;

    public async Task<IEnumerable<MopDto>> GetAllAsync()
    {
        var sql = "SELECT mop_id AS MopId, mop AS Mop FROM lib_mode_of_payment ORDER BY mop";
        return await _db.QueryAsync<MopDto>(sql);
    }

    public async Task<MopDto?> GetByIdAsync(int id)
    {
        var sql = "SELECT mop_id AS MopId, mop AS Mop FROM lib_mode_of_payment WHERE mop_id=@Id";
        return await _db.QueryFirstOrDefaultAsync<MopDto>(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(MopDto dto)
    {
        var sql = "INSERT INTO lib_mode_of_payment (mop) VALUES (@Mop); SELECT CAST(SCOPE_IDENTITY() AS INT);";
        return await _db.ExecuteScalarAsync<int>(sql, dto);
    }

    public async Task<int> UpdateAsync(MopDto dto)
    {
        var sql = "UPDATE lib_mode_of_payment SET mop=@Mop WHERE mop_id=@MopId";
        return await _db.ExecuteAsync(sql, dto);
    }

    public async Task<int> DeleteAsync(int id)
    {
        var sql = "DELETE FROM lib_mode_of_payment WHERE mop_id=@Id";
        return await _db.ExecuteAsync(sql, new { Id = id });
    }
}
