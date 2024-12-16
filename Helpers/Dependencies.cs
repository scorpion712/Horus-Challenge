using Horus_Challenge.Services.Interfaces;
using Horus_Challenge.Services;
using Horus_Challenge.ViewModels;
using Horus_Challenge.Views;

namespace Horus_Challenge.Helpers; 

public static class Dependencies
{
    public static IServiceCollection AddServicesLayer(this IServiceCollection services)
    {
        #region Register Services 
        services.AddSingleton<HttpClientService>();
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<IChallengesService, ChallengesService>();

        #endregion Register Services

        #region Register View Models

        services.AddTransient<LoginViewModel>();
        services.AddTransient<GamificationViewModel>();

        #endregion Register View Models 

        #region Register Pages

        services.AddTransient<LoginPage>();
        services.AddTransient<GamificationPage>();

        #endregion Register Pages 

        return services;
    }
}
