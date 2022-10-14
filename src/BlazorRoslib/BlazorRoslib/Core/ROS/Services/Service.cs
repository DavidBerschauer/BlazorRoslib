using System;
namespace BlazorRoslib.Core.ROS.Services
{
	public class Service : IService
	{
		public string Name { get; set; }
        public string? Type { get; set; }
    }
}

