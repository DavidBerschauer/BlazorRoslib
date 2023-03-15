using System;
using BlazorRoslib.Core.ROS.Services;

namespace BlazorRoslib.Core.ROS.Rosapi
{
    public class ServiceTypeService : RosService<ServiceTypeService, ServiceTypeRequest, TypeResponse>
    {
        public override string Name => "/rosapi/service_type";

        public override string? Type => "rosapi/ServiceType";
    }

    public class ServiceTypeRequest : IServiceRequest
    {
        public required string Service { get; set; }

        public string[] ArgNames { get; } = new[] { "service" };

        public object[] Args => new[] { Service };
    }
}

