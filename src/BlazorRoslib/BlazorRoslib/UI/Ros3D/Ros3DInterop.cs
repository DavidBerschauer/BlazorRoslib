using System;
using BlazorRoslib.Core.ROS;
using BlazorRoslib.Core.ROS.Services;
using BlazorRoslib.Core.ROS.Topics;
using BlazorRoslib.Core.Services;
using Microsoft.JSInterop;

namespace BlazorRoslib.UI.Ros3D;

public class Ros3DInterop : IAsyncDisposable
{
    public bool IgnoreAll = false;

    private ILocalStorageService _localStorageService;
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;
    private DotNetObjectReference<Ros3DInterop> objRef;
    private RoslibInterop ros;

    public Ros3DInterop(RoslibInterop ros, IJSRuntime jsRuntime, ILocalStorageService localStorageService)
    {
        this.ros = ros;
        ros.OnStateHasChanged += async (s) => await OnRosStateChanged();
        _localStorageService = localStorageService;
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/BlazorRoslib.UI/ros3d.js").AsTask());
        objRef = DotNetObjectReference.Create(this);
    }

    public async Task InitViewer()
    {
        await InvokeVoid("initRos3d");
    }

    public async Task OnRosStateChanged()
    {
        await InvokeVoid("updateTFClient");
    }

    public async Task AddPointCloud(string topic)
    {
        await InvokeVoid("addPointCloud", topic);
    }

    public async Task AddLaserScan(string topic)
    {
        await InvokeVoid("addLaserScan", topic);
    }

    public async Task UpdateFixedFrame(string frameId)
    {
        await InvokeVoid("updateFixedFrame", frameId);
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }

    private async Task InvokeVoid(string methodName, params object?[]? args)
    {
        if (IgnoreAll)
        {
            //Debug.WriteLine("Ignoring mapChange");
            return;
        }
        try
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(methodName, args);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

