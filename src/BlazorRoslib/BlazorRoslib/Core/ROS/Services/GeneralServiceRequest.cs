using System;
namespace BlazorRoslib.Core.ROS.Services
{
	public class GeneralServiceRequest : IServiceRequest
	{
        public string[] ArgNames { get; set; }
        public object[] Args { get; set; }

        public GeneralServiceRequest(string[] ArgNames, object[] Args)
        {
            this.ArgNames = ArgNames;
            this.Args = Args;
        }
    }
}

