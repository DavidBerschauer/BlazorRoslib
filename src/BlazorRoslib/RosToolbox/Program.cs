using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RosToolbox;
using BlazorRoslib.Core;
using MudBlazor.Services;
using MudBlazor;
using RosToolbox.Helpers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
    .UseRoslibBlazor()
    .AddSingleton<CustomNavManager>();

builder.Services.AddMudServices(c =>
{
    c.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
    c.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
    c.SnackbarConfiguration.ShowCloseIcon = true;
});
await builder.Build().RunAsync();

