using System;
using BlazorRoslib.Core.ROS.Services;

namespace RosToolbox.CustomPackages.DataRecording;

public class RecordingStateService : RosService<RecordingStateService, IServiceRequest, RecordStateResponse>
{
    public override string Name => @"/data_recording/recording_state";
    public override string? Type => "data_recording/RecordState";
}

public class RecordStateResponse : ServiceResponseBase
{
    public bool recording { get; set; }
    public string? filename { get; set; }
    public string? filesize { get; set; }
    public string[]? topics { get; set; }
    public string? remaining_time { get; set; }
}