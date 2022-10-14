using System;
using BlazorRoslib.Core.ROS.Services;

namespace BlazorRoslib.Core.ROS.Rosapi
{
    public class ServiceRequestDetailsService : IService
    {
        public string Name => "/rosapi/service_request_details";

        public string? Type => "rosapi/ServiceRequestDetails";
    }
}

