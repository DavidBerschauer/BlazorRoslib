using System;
using BlazorRoslib.Core.ROS.Services;

namespace BlazorRoslib.Core.ROS.Rosapi
{
    public class ServiceResonseDetailsService : RosService<ServiceResonseDetailsService, TypeRequest, TypeDefResponse>
    {
        public override string Name => "/rosapi/service_response_details";

        public override string? Type => "rosapi/ServiceResponseDetails";
    }
}

