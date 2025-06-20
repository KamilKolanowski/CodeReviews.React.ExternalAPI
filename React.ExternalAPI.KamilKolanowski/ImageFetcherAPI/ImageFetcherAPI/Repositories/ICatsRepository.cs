using ImageFetcherAPI.Models;

namespace ImageFetcherAPI.Repositories;

public interface ICatsRepository
{
    Task<IEnumerable<Cat>> GetCatsAsync(int limit);
    Task<Cat?> GetCatByIdAsync(string id);
    Task AddCatAsync(Cat cat);
    Task UpdateCatAsync(Cat cat);
    Task DeleteCatAsync(string id);
}
