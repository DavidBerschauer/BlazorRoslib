using System;
using BlazorRoslib.Core.ROS.Services;

namespace RosToolboxApp.CustomPackages.DataRecording;

public class StopRecordingService : IService
{
    public string Name => @"/data_recording/stop_recording";
    public string? Type => "std_srvs/Trigger";
}

