using Horus_Challenge.Services;
using Horus_Challenge.Services.Interfaces;
using Horus_Challenge.ViewModels;
using Horus_Challenge.Views;
using Microsoft.Extensions.Logging;
using Mopups.Hosting;
using Mopups.PreBaked.Interfaces;

namespace Horus_Challenge
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureMopups()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Gilroy-Regular.ttf", "GilroyRegular");
                    fonts.AddFont("Gilroy-SemiBold.ttf", "GilroySemibold");
                });
            
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddSingleton<HttpClientService>();
            builder.Services.AddSingleton<IAuthService, AuthService>();
#if DEBUG
            builder.Logging.AddDebug();
#endif


            // Entries handler to hide underline on Entry
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if __ANDROID__
    handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif __IOS__
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            });

            return builder.Build();
        }
    }
}
