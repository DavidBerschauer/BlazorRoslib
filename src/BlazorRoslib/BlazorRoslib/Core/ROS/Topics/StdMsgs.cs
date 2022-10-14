using System;
namespace BlazorRoslib.Core.ROS.Topics
{
	public class StdMsgs<T> : TopicMsgBase
	{
		public T? data { get; set; }
	}
}

