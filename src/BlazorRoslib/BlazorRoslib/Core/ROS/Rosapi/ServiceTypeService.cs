using System;
using BlazorRoslib.Core.ROS.Services;

namespace BlazorRoslib.Core.ROS.Rosapi
{
    public class ServiceTypeService : IService
    {
        public string Name => "/rosapi/service_type";

        public string? Type => "rosapi/ServiceType";
    }
}

