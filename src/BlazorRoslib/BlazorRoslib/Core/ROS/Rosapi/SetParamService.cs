using System;
using BlazorRoslib.Core.ROS.Services;

namespace BlazorRoslib.Core.ROS.Rosapi
{
    public class SetParamService : IService
    {
        public string Name => "/rosapi/set_param";

        public string? Type => "rosapi/SetParam";
    }
}

