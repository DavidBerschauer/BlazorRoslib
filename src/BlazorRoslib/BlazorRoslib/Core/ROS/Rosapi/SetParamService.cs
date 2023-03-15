using System;
using BlazorRoslib.Core.ROS.Services;

namespace BlazorRoslib.Core.ROS.Rosapi
{
    public class SetParamService : RosService<SetParamService, SetParamRequest, ServiceResponseBase>
    {
        public override string Name => "/rosapi/set_param";

        public override string? Type => "rosapi/SetParam";
    }

    public class SetParamRequest : IServiceRequest
    {
        public string Name { get; set; } = "";

        public string Value { get; set; } = "";

        public string[] ArgNames { get; } = new[] { "name", "value" };

        public object[] Args => new[] { Name, Value };
    }
}

