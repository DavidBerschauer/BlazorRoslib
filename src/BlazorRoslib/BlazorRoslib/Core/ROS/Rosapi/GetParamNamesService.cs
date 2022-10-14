using System;
using BlazorRoslib.Core.ROS.Services;

namespace BlazorRoslib.Core.ROS.Rosapi
{
    public class GetParamNamesService : IService
    {
        public string Name => "/rosapi/get_param_names";

        public string? Type => "rosapi/GetParamNames";
    }
}

