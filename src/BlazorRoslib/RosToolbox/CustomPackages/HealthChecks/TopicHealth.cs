using System;
namespace RosToolbox.CustomPackages.HealthChecks;

public class TopicHealth : BlazorRoslib.Core.ROS.Topics.TopicMsgBase
{
    public const int TYPE_N_A = 0;
    public const int TYPE_TIMEOUT = 1;
    public const int TYPE_WARNING = 2;
    public const int TYPE_OK = 3;
    public int type { get; set; }
    public string? title { get; set; }
    public float hz { get; set; }

    //public enum Type{
    //    N_A,
    //    Timeout,
    //    Warning,
    //    Ok
    //}
}

