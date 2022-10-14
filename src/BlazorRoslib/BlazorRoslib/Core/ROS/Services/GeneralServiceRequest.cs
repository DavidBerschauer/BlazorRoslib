using System;
namespace BlazorRoslib.Core.ROS.Services
{
	public class GeneralServiceRequest : IServiceRequest
	{
        public string[] ArgNames { get; set; }
        public string[] Args { get; set; }

        public GeneralServiceRequest(string[] ArgNames, string[] Args)
        {
            this.ArgNames = ArgNames;
            this.Args = Args;
        }
    }
}

