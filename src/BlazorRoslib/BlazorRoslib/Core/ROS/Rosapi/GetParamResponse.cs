using System;
using BlazorRoslib.Core.ROS.Services;
namespace BlazorRoslib.Core.ROS.Rosapi
{
	public class GetParamResponse : ServiceResponseBase
	{
		public string value { get; set; }
	}
}

