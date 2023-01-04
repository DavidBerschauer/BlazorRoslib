using System;
namespace BlazorRoslib.Core.ROS.Services.StdSrvs
{
	public class TriggerResponse : ServiceResponseBase
    {
        public bool success { get; set; }
        public string? message { get; set; }
    }
}

