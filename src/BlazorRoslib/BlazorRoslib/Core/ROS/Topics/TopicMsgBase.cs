using System;
namespace BlazorRoslib.Core.ROS.Topics
{
	public abstract class TopicMsgBase
	{
		public MsgHeader? header { get; set; }

	}

	public class MsgHeader
	{
		public int seq { get; set; }
        public string? frame_id { get; set; }
		public HeaderStamp? stamp { get; set; }
    }

	public class HeaderStamp
	{
        public int secs { get; set; }
        public int nsecs { get; set; }
    }
}

