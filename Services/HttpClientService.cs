using Horus_Challenge.Helpers.Utils;

namespace Horus_Challenge.Services;

public class HttpClientService
{
    private readonly HttpClient _client;

    public HttpClientService()
    {
        var httpClientHandler = new HttpClientHandler();
        _client = new HttpClient(httpClientHandler)
        {
            BaseAddress = new Uri(Constants.API_BASE_URL),
        };
    }

    public HttpClient GetClient() => _client;
}
