using System;
namespace BlazorRoslib.Core.ROS.Services
{
	public interface IService
	{
        string Name { get; }
        string? Type { get; }
    }
}

