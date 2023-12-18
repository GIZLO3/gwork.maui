using gwork.maui.Data;
using gwork.maui.Pages;
using gwork.maui.Pages.AdminPages;
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
            
            builder.Services.AddSingleton<UserDatabase>();

            builder.Services.AddTransient<LogInPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<UserDetailsPage>();
            builder.Services.AddTransient<OfferDetailsPage>();
            
            builder.Services.AddTransient<AdminPanelPage>();
            builder.Services.AddTransient<AddOrEditOfferPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
