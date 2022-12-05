using System;
namespace BlazorRoslib.Core.ROS.Services
{
	public interface IServiceRequest
	{
		string[] ArgNames { get; }
        object[] Args { get; }
    }
}

