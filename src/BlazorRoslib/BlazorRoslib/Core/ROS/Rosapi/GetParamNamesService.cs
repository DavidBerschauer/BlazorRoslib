using System;
using BlazorRoslib.Core.ROS.Services;

namespace BlazorRoslib.Core.ROS.Rosapi
{
    public class GetParamNamesService : RosService<GetParamNamesService, IServiceRequest, GetParamNamesResponse>
    {
        public override string Name => "/rosapi/get_param_names";

        public override string? Type => "rosapi/GetParamNames";
    }

    public class GetParamNamesResponse : ServiceResponseBase
    {
        public string[]? names { get; set; }
    }
}

