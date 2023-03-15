using System;
namespace BlazorRoslib.Core.ROS.Services
{
	public class Service : IRosService
    {
		public required string Name { get; set; }
        public string? Type { get; set; }
    }
}

