using SpamMusubiAPI.DTOs;

namespace SpamMusubiAPI.Repositories.Interfaces;

public interface IAddOnsRepository
{
    Task<IEnumerable<AddOnDto>> GetAllAsync();
    Task<AddOnDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(AddOnDto dto);
    Task<int> UpdateAsync(AddOnDto dto);
    Task<int> DeleteAsync(int id);
}
