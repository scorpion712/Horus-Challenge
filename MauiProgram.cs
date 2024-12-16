using Horus_Challenge.Helpers;
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

            builder.Services.AddServicesLayer();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
