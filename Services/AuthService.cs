using Horus_Challenge.Models;
using Horus_Challenge.Services.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text; 

namespace Horus_Challenge.Services;

public class AuthService(HttpClientService httpClientService) : IAuthService
{
    private readonly HttpClient client = httpClientService.GetClient(); 

    public async Task<User?> SignIn(string email, string password)
    {
        try
        {
            var json = JsonConvert.SerializeObject(new { Email = email, Password = password });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"UserSignIn", content);

            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Usuario o contraseña inválidos");
                return null;
            }

            var userResponse = await response.Content.ReadAsStringAsync();

            var user = JsonConvert.DeserializeObject<User>(userResponse);

            // set token
            httpClientService.SetAuthorizationToken(user!.AuthorizationToken);

            return user;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Ha ocurrido un error al iniciar sesión: {ex.Message}");
            return null;
        }
    }
}
