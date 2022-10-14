using System;
using BlazorRoslib.Core.ROS.Services;
namespace BlazorRoslib.Core.ROS.Rosapi
{
	public class TypeDefResponse : ServiceResponseBase
	{
		public TypeDefEntry[]? typedefs { get; set; }
	}

	public class TypeDefEntry
	{
		public string? type { get; set; }
        public string[]? fieldnames { get; set; }
        public string[]? fieldtypes { get; set; }
    }
}

