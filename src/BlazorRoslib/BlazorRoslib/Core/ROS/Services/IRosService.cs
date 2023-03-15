using System;
namespace BlazorRoslib.Core.ROS.Services
{
	public interface IRosService
    {
        string Name { get; }
        string? Type { get; }
    }
}

