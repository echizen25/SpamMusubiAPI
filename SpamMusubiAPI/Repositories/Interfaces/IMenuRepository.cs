using SpamMusubiAPI.DTOs;

namespace SpamMusubiAPI.Repositories.Interfaces;

public interface IMenuRepository
{
    Task<IEnumerable<MenuDto>> GetAllAsync();
    Task<MenuDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(MenuDto dto);
    Task<int> UpdateAsync(MenuDto dto);
    Task<int> DeleteAsync(int id);
}
