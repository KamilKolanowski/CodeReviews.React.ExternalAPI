using ImageFetcherAPI.Models;
using Microsoft.Extensions.Options;

namespace ImageFetcherAPI.Services;

public class ExternalCatsApi
{
    private readonly HttpClient _httpClient;

    public ExternalCatsApi(IOptions<ApiSettings> options)
    {
        var settings = options.Value;
        _httpClient = new HttpClient { BaseAddress = new Uri(settings.BaseUrl) };

        _httpClient.DefaultRequestHeaders.Add("x-api-key", settings.ApiKey);
    }

    public async Task<IEnumerable<Cat>?> GetAllCatsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Cat>>("images/search?limit=100");
    }
}
