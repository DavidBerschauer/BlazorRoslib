using System;
using BlazorRoslib.Core.ROS.Services;

namespace BlazorRoslib.Core.ROS.Rosapi
{
    public class GetParamService : IService
    {
        public string Name => "/rosapi/get_param";

        public string? Type => "rosapi/GetParam";
    }
}

