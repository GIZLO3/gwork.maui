using gwork.maui.Data;
using gwork.maui.Pages;
using Microsoft.Extensions.Logging;

namespace gwork.maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddSingleton<LogInPage>();
            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton<UserDetailsPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
