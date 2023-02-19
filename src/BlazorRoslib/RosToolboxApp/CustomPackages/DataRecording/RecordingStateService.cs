using System;
using BlazorRoslib.Core.ROS.Services;

namespace RosToolboxApp.CustomPackages.DataRecording;

public class RecordingStateService : IService
{
    public string Name => @"/data_recording/recording_state";
    public string? Type => "data_recording/RecordState";
}

public class RecordStateResponse : ServiceResponseBase
{
    public bool recording { get; set; }
    public string? filename { get; set; }
    public string? filesize { get; set; }
    public string[]? topics { get; set; }
    public string? remaining_time { get; set; }
}