using System;
using BlazorRoslib.Core.ROS.Services;
namespace BlazorRoslib.Core.ROS.Rosapi
{
	public class SetParamRequest : IServiceRequest
	{
        public string Name { get; set; } = "";

        public string Value { get; set; } = "";

        public string[] ArgNames { get; } = new[] { "name", "value" };

        public string[] Args => new[] { Name, Value };
    }
}

