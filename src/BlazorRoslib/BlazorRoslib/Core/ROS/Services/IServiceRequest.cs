using System;
namespace BlazorRoslib.Core.ROS.Services
{
	public interface IServiceRequest
	{
		string[] ArgNames { get; }
        string[] Args { get; }
    }
}

