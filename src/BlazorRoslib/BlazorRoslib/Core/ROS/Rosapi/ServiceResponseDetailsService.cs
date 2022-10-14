using System;
using BlazorRoslib.Core.ROS.Services;

namespace BlazorRoslib.Core.ROS.Rosapi
{
    public class ServiceResonseDetailsService : IService
    {
        public string Name => "/rosapi/service_response_details";

        public string? Type => "rosapi/ServiceResponseDetails";
    }
}

