using System;
using Microsoft.Extensions.DependencyInjection;
using BlazorRoslib.Core;

namespace BlazorRoslib.UI;

public static class ServicesExtensions
{
    public static IServiceCollection UseBlazorRoslibUI(this IServiceCollection services, bool multipleConnections = false)
    {
        services.UseBlazorRoslib(multipleConnections);
        services.AddTransient<Ros3D.Ros3DInterop>();
        return services;
    }
}

