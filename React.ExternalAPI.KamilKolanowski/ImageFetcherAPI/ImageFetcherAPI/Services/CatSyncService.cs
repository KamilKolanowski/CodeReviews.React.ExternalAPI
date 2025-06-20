using ImageFetcherAPI.Repositories;

namespace ImageFetcherAPI.Services;

public class CatSyncService
{
    private readonly ExternalCatsApi _externalCatsApi;
    private readonly ICatsRepository _catsRepository;

    public CatSyncService(ExternalCatsApi externalCatsApi, ICatsRepository catsRepository)
    {
        _externalCatsApi = externalCatsApi;
        _catsRepository = catsRepository;
    }

    public async Task SyncCatsAsync()
    {
        try
        {
            var externalCats = await _externalCatsApi.GetAllCatsAsync();
            if (externalCats == null || !externalCats.Any())
            {
                return;
            }

            foreach (var cat in externalCats)
            {
                var existingCat = await _catsRepository.GetCatByIdAsync(cat.Id);
                if (existingCat == null)
                {
                    await _catsRepository.AddCatAsync(cat);
                }
                else
                {
                    existingCat.Name = cat.Name;
                    await _catsRepository.UpdateCatAsync(existingCat);
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error syncing cats.", ex);
        }
    }
}
