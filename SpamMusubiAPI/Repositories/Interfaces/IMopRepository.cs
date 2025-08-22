using SpamMusubiAPI.DTOs;

namespace SpamMusubiAPI.Repositories.Interfaces;

public interface IMopRepository
{
    Task<IEnumerable<MopDto>> GetAllAsync();
    Task<MopDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(MopDto dto);
    Task<int> UpdateAsync(MopDto dto);
    Task<int> DeleteAsync(int id);
}
