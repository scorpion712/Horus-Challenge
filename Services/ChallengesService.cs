using Horus_Challenge.Models;
using Horus_Challenge.Services.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Horus_Challenge.Services;

public class ChallengesService(HttpClientService httpClientService) : IChallengesService
{
    private readonly HttpClient client = httpClientService.GetClient();

    public async Task<IEnumerable<Challenge>?> GetAll()
    {
        try
        {
            var response = await client.GetAsync($"Challenges");

            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("No se pudieron obtener los retos.");
                return null;
            }

            var challenges = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<Challenge>>(challenges);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Ha ocurrido un error al intentar obtener los retos: {ex.Message}");
            return null;
        }
    }
}
