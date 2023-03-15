using System;
using BlazorRoslib.Core.ROS.Services;
using BlazorRoslib.Core.ROS.Services.StdSrvs;

namespace RosToolbox.CustomPackages.DataRecording;

public class StopRecordingService : RosService<StopRecordingService, IServiceRequest, TriggerResponse>
{
    public override string Name => @"/data_recording/stop_recording";
    public override string? Type => "std_srvs/Trigger";
}

