using Microsoft.Extensions.Logging;
using Divisas.DataAccess;
using Divisas.Views;
using Divisas.ViewModels;

namespace Divisas
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

            var dbContext = new CurrencyDbContext();
            dbContext.Database.EnsureCreated();
            dbContext.Dispose();

            builder.Services.AddDbContext<CurrencyDbContext>();

            builder.Services.AddTransient<CurrencyForm>();
            builder.Services.AddTransient<NewCurrencyViewModel>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<CurrencyListViewModel>();

            builder.Services.AddTransient<CurrencyList>();
            builder.Services.AddTransient<Splash>();
            builder.Services.AddTransient<PageSelector>();
            builder.Services.AddTransient<Conversion>();
            builder.Services.AddTransient<Settings>();

            Routing.RegisterRoute(nameof(CurrencyForm), typeof(CurrencyForm));
            Routing.RegisterRoute(nameof(Settings), typeof(Settings));
            Routing.RegisterRoute(nameof(CurrencyList), typeof(CurrencyList));
            Routing.RegisterRoute(nameof(Conversion), typeof(Conversion));



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
