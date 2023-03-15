using System;
namespace BlazorRoslib.Core.ROS.Services
{
	public abstract class RosService<T, Request, Response> : IRosService
		where T : RosService<T, Request, Response>, new()
		where Request : IServiceRequest
		where Response : ServiceResponseBase
	{
		public static T Instance { get; } = new T();

        public abstract string Name { get; }
        public abstract string? Type { get; }

        
	}
}

