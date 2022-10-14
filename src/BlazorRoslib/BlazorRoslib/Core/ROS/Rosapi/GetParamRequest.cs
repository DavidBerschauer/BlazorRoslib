using System;
using BlazorRoslib.Core.ROS.Services;
namespace BlazorRoslib.Core.ROS.Rosapi
{
	public class GetParamRequest : IServiceRequest
	{
        public string Name { get; set; } = "";

        public string Default { get; set; } = "";

        public string[] ArgNames { get; } = new[] { "name", "default" };

        public string[] Args => new[] { Name, Default };
    }
}

