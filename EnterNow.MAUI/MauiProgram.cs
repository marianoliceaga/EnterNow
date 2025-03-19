using EnterNow.MAUI;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using ZXing.Net.Maui;

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
            });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddSingleton<HttpClient>(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001/api/") });

        builder.Services.AddAuthorizationCore();
        //TODO fix this builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
        builder.Services.AddSingleton<TokenService>();
        builder.Services.AddTransient<QrScannerService>();

        return builder.Build();
    }
}
