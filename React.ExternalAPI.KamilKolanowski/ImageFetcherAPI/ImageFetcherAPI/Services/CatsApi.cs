using ImageFetcherAPI.Models;
using ImageFetcherAPI.Repositories;

namespace ImageFetcherAPI.Services;

public class CatsApi : ICatsApi
{
    private readonly ICatsRepository _repository;

    public CatsApi(ICatsRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Cat>?> GetCatsAsync(int limit)
    {
        try
        {
            var cats = await _repository.GetCatsAsync(limit);
            return cats?.Take(limit);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error retrieving all cats.", ex);
        }
    }
    
    public async Task<IEnumerable<Cat>?> GetAllCatsAsync()
    {
        try
        {
            return await _repository.GetCatsAsync(int.MaxValue);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error retrieving all cats.", ex);
        }
    }

    public async Task<Cat?> GetCatAsync(string id)
    {
        try
        {
            return await _repository.GetCatByIdAsync(id);
        }
        catch (KeyNotFoundException ex)
        {
            throw new ApplicationException($"Cat with Id '{id}' not found.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error retrieving cat.", ex);
        }
    }

    public async Task CreateCatAsync(Cat cat)
    {
        try
        {
            await _repository.AddCatAsync(cat);
        }
        catch (ArgumentException ex)
        {
            throw new ApplicationException("Invalid cat data provided.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error creating cat.", ex);
        }
    }

    public async Task UpdateCatAsync(Cat cat)
    {
        try
        {
            await _repository.UpdateCatAsync(cat);
        }
        catch (KeyNotFoundException ex)
        {
            throw new ApplicationException("Cat to update not found.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error updating cat.", ex);
        }
    }

    public async Task DeleteCatAsync(string id)
    {
        try
        {
            await _repository.DeleteCatAsync(id);
        }
        catch (KeyNotFoundException ex)
        {
            throw new ApplicationException($"Cat with Id '{id}' not found for deletion.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error deleting cat.", ex);
        }
    }
}
