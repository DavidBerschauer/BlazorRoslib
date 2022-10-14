using System;
using BlazorRoslib.Core.ROS.Services;
namespace BlazorRoslib.Core.ROS.Rosapi
{
	public class TypeRequest : IServiceRequest
	{
        public string Type { get; set; } = "";

        public string[] ArgNames { get; } = new[] { "type" };

        public string[] Args => new[] { Type };
    }
}

