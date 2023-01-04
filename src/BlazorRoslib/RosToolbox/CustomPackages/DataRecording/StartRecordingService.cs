using System;
using BlazorRoslib.Core.ROS.Services;

namespace RosToolbox.CustomPackages.DataRecording;

public class StartRecordingService : IService
{
    public string Name => @"/data_recording/start_recording";
    public string? Type => "data_recording/Record";
}

public class RecordRequest : IServiceRequest
{
    public string Bagname { get; set; } = "";

    public int Timeout { get; set; } = 0;

    public string[] ArgNames { get; } = new[] { "bagname", "timeout" };

    public object[] Args => new object[] { Bagname, Timeout };
}

public class RecordResponse : ServiceResponseBase
{
    public bool success { get; set; }
    public string? message { get; set; }
    public string[]? names { get; set; }
}
