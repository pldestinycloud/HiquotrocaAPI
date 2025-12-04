using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using Hiquotroca.Services;     
using System.Net.Http;         

namespace Hiquotroca
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
                    fonts.AddFont("Montserrat-Regular.ttf", "MontserratRegular");
                    fonts.AddFont("Montserrat-Bold.ttf", "MontserratBold");
                    fonts.AddFont("Montserrat-Light.ttf", "MontserratLight");
                });

            builder.Services.AddMauiBlazorWebView();

            #if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
            #endif

            builder.Services.AddMudServices();

            // HttpClient que comunica com a API
            builder.Services.AddSingleton(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7010/")
            });

            // Serviço de autenticação
            builder.Services.AddScoped<AuthApiService>();

            //---------------------------------------------------

            return builder.Build();
        }
    }
}

