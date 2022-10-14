using System;
using BlazorRoslib.Core.ROS.Services;
namespace BlazorRoslib.Core.ROS.Rosapi
{
	public class GetParamNamesResponse : ServiceResponseBase
	{
		public string[]? names { get; set; }
	}
}

