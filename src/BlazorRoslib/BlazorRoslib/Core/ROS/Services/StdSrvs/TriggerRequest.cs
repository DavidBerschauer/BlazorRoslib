using System;
namespace BlazorRoslib.Core.ROS.Services.StdSrvs
{
	public class TriggerRequest : IServiceRequest
	{
		public TriggerRequest()
		{
		}

        public string[] ArgNames => Array.Empty<string>();

        public object[] Args => Array.Empty<string>();
    }
}

