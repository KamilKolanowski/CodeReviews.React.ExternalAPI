using ImageFetcherAPI.Data;
using ImageFetcherAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageFetcherAPI.Repositories;

public class CatsRepository : ICatsRepository
{
    private readonly ImageFetcherDbContext _context;

    public CatsRepository(ImageFetcherDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cat>> GetCatsAsync(int limit)
    {
        return await _context.Cats.Take(limit).ToListAsync();
    }

    public async Task<Cat?> GetCatByIdAsync(string id)
    {
        return await _context.Cats.FindAsync(id);
    }

    public async Task AddCatAsync(Cat cat)
    {
        _context.Cats.Add(cat);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCatAsync(Cat cat)
    {
        _context.Cats.Update(cat);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCatAsync(string id)
    {
        var cat = await GetCatByIdAsync(id);
        if (cat != null)
        {
            _context.Cats.Remove(cat);
            await _context.SaveChangesAsync();
        }
    }
}
