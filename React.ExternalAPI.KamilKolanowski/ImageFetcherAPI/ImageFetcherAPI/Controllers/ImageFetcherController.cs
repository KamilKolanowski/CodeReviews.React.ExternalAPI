using ImageFetcherAPI.Models;
using ImageFetcherAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImageFetcherAPI.Controllers;

[ApiController]
[Route("api")]
public class ImageFetcherController : ControllerBase
{
    private readonly CatSyncService _catSyncService;
    private readonly ICatsApi _catsApi;

    public ImageFetcherController(CatSyncService catSyncService, ICatsApi catsApi)
    {
        _catsApi = catsApi;
        _catSyncService = catSyncService;
    }

    [HttpPost("sync")]
    public async Task<IActionResult> SyncCats()
    {
        await _catSyncService.SyncCatsAsync();
        return Ok("Synced");
    }
    
    [HttpGet("cats")]
    public async Task<ActionResult<IEnumerable<Cat>>> GetCats([FromQuery] int? limit)
    {
        if (limit.HasValue)
        {
            var allCats = await _catsApi.GetAllCatsAsync();
            var randomCats = allCats?
                .OrderBy(_ => Guid.NewGuid())
                .Take(limit.Value)
                .ToList();

            return Ok(randomCats);
        }
        var cats = await _catsApi.GetAllCatsAsync();
        return Ok(cats);
    }
}
