using System;
using BlazorRoslib.Core.ROS.Services;

namespace BlazorRoslib.Core.ROS.Rosapi
{
    public class ServiceRequestDetailsService : RosService<ServiceRequestDetailsService, TypeRequest, TypeDefResponse>
    {
        public override string Name => "/rosapi/service_request_details";

        public override string? Type => "rosapi/ServiceRequestDetails";
    }
}

