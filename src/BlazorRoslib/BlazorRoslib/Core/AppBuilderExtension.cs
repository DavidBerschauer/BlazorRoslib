using System;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorRoslib.Core
{
    public static class ServicesExtensions
    {
        public static IServiceCollection UseRoslibBlazor(this IServiceCollection services, bool multipleConnections = false)
        {
            services.AddSingleton<Services.ILocalStorageService, Services.LocalStorageService>();
            if(multipleConnections)
                services.AddTransient<ROS.RoslibInterop>();
            else
                services.AddSingleton<ROS.RoslibInterop>();
            return services;
        }
    }
}

