using System;
using BlazorRoslib.Core.ROS.Services;

namespace BlazorRoslib.Core.ROS.Rosapi
{
    public class GetParamService : RosService<GetParamService, GetParamRequest, GetParamResponse>
    {
        public override string Name => "/rosapi/get_param";

        public override string? Type => "rosapi/GetParam";
    }

    public class GetParamRequest : IServiceRequest
    {
        public string Name { get; set; } = "";

        public string Default { get; set; } = "";

        public string[] ArgNames { get; } = new[] { "name", "default" };

        public object[] Args => new[] { Name, Default };
    }

    public class GetParamResponse : ServiceResponseBase
    {
        public string value { get; set; }
    }
}

