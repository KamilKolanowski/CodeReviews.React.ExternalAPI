using ImageFetcherAPI.Models;

namespace ImageFetcherAPI.Services;

public interface ICatsApi
{
    Task<IEnumerable<Cat>?> GetCatsAsync(int limit);
    Task<IEnumerable<Cat>?> GetAllCatsAsync();
    Task<Cat?> GetCatAsync(string id);
    Task CreateCatAsync(Cat cat);
    Task UpdateCatAsync(Cat cat);
    Task DeleteCatAsync(string id);
}
