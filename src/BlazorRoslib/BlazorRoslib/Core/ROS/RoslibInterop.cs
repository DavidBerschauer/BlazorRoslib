using System;
using System.Diagnostics;
using Microsoft.JSInterop;
using System.Text.Json;
using BlazorRoslib.Core.Services;
using System.Xml.Linq;
using BlazorRoslib.Core.ROS.Topics;
using BlazorRoslib.Core.ROS.Services;

namespace BlazorRoslib.Core.ROS;

public enum RosInteropState
{
    NotInitialized,
    Connected,
    ConnectionClosed,
    ConnectionError
}

public enum RosInteropProtocol
{
    WS,
    WSS
}

public delegate void OnStateHasChanged(RosInteropState state);


//TODO Add Error Callbacks for all
	public class RoslibInterop : IAsyncDisposable
{
    public bool IgnoreAll = false;

    public OnStateHasChanged OnStateHasChanged { get; set; }
    private RosInteropState _state;
    public RosInteropState State {
        get => _state;
        private set
        {
            if (_state == value) return;
            _state = value;
            OnStateHasChanged?.Invoke(_state);
        }
    }
    public string ErrorMsg { get; private set; } = "";

    private string _rosHost = "";
    public string RosHost
    {
        get => _rosHost;
        set
        {
            if (_rosHost?.Equals(value) ?? false) return;
            _rosHost = value;
            Task.Run(async () => await _localStorageService.SetItem("RosHost", _rosHost));
            UpdateRos();
        }
    }

    private int _rosPort = 9090;
    public int RosPort
    {
        get => _rosPort;
        set
        {
            if (_rosPort == value) return;
            _rosPort = value;
            Task.Run(async () => await _localStorageService.SetItem("RosPort", _rosPort));
            UpdateRos();
        }
    }

    private RosInteropProtocol _rosProtocol = RosInteropProtocol.WSS;
    public RosInteropProtocol RosProtocol
    {
        get => _rosProtocol;
        set
        {
            if (_rosProtocol == value) return;
            _rosProtocol = value;
            Task.Run(async () => await _localStorageService.SetItem("RosProtocol", _rosProtocol));
            UpdateRos();
        }
    }

    public string RosUrl => $"{ProtocolString(RosProtocol)}://{RosHost}:{RosPort}";

    private ILocalStorageService _localStorageService;
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;
    private DotNetObjectReference<RoslibInterop> objRef;
    private readonly Dictionary<TopicDictKey, List<Action<string, string, string>>> topicDict;
    private readonly Dictionary<int, Action<string>> serviceDict;
    private int lastServiceId = 0;
    private readonly List<Action<IEnumerable<Topic>>> allTopicSubscribers;
    private readonly List<Action<IEnumerable<IService>>> allServicesSubscribers;

    public RoslibInterop(IJSRuntime jsRuntime, ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
        OnStateHasChanged = (_) => { };
        State = RosInteropState.NotInitialized;
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/BlazorRoslib.Core/rosInterop.js").AsTask());
        objRef = DotNetObjectReference.Create(this);
        topicDict = new Dictionary<TopicDictKey, List<Action<string, string, string>>>();
        serviceDict = new Dictionary<int, Action<string>>();
        allTopicSubscribers = new List<Action<IEnumerable<Topic>>>();
        allServicesSubscribers = new List<Action<IEnumerable<IService>>>();
        Task.Run(async () =>
        {
            RosHost = await _localStorageService.GetItem<string>("RosHost", "") ?? "";
            RosPort = await _localStorageService.GetItem<int>("RosPort", 9090);
            RosProtocol = await _localStorageService.GetItem<RosInteropProtocol>("RosProtocol", RosInteropProtocol.WSS);
        });
    }

    public void UpdateRos()
    {
        Task.Run(async () =>
        {
            await InitRos(RosHost, RosPort, ProtocolString(RosProtocol));
        });
    }

    public async Task InitRos(string host, int port = 8081, string protocol = "wss")
    {
        State = RosInteropState.NotInitialized;
        await InvokeVoid("initRos", host, port, protocol, objRef);
        await Task.Delay(2000);
        if(State == RosInteropState.NotInitialized)
        {
            OnError("Timeout");
        }
    }

#region Topics
    public async Task GetTopics(Action<IEnumerable<Topic>> callback)
    {
        allTopicSubscribers.Add(callback);
        await InvokeVoid("getTopics", objRef, nameof(OnGetTopics));
    }


    public Task SubscribeTopic(Topic topic, Action<string, string, string> callback)
    {
        return SubscribeTopic(topic.Name, topic.MessageType, callback);
    }

    public async Task SubscribeTopic(string name, string messageType, Action<string, string, string> callback)
    {
        var topicKey = new TopicDictKey() { Name = name, MesssageType = messageType };
        if (topicDict.ContainsKey(topicKey))
        {
            topicDict[topicKey].Add(callback);
        }
        else
        {
            var newList = new List<Action<string, string, string>>();
            newList.Add(callback);
            topicDict[topicKey] = newList;
            //TODO Alles testen mit unsubscribe!
            await InvokeVoid("subscribeTopic", name, messageType, objRef, nameof(OnTopic));
        }
    }

    public Task SubscribeTopic<T>(Topic topic, Action<T> callback) where T : TopicMsgBase
    {
        return SubscribeTopic<T>(topic.Name, topic.MessageType, callback);  
    }

    public Task SubscribeTopic<T>(string name, string messageType, Action<T> callback) where T : TopicMsgBase
    {
        return SubscribeTopic(name, messageType, (_, _, json) => DeserializeAndCall(json, callback));
    }
#endregion

#region Services

    public async Task GetServices(Action<IEnumerable<IService>> callback)
    {
        allServicesSubscribers.Add(callback);
        await InvokeVoid("getServices", objRef, nameof(OnGetServices));
    }

    public Task IsServiceAvailable(IService service, Action<bool> callback)
    {
        return IsServiceAvailable(service.Name, callback);
    }

    public async Task IsServiceAvailable(string service, Action<bool> callback)
    {
        await GetServices((services) =>
        {
            callback(services.Any((s) => s.Name.Equals(service)));
        });
    }

    public Task CallService<T>(IService service, Action<T> callback) where T : ServiceResponseBase
    {
        return CallService(service, null, callback);
    }

    public Task CallService(IService service, Action<string> callback)
    {
        return CallService(service.Name, service.Type ?? "", null, callback);
    }

    public Task CallService<T>(IService service, IServiceRequest? request, Action<T> callback) where T : ServiceResponseBase
    {
        return CallService(service, request, (json) => DeserializeAndCall(json, callback));
    }

    public Task CallService(IService service, IServiceRequest? request, Action<string> callback)
    {
        return CallService(service.Name, service.Type ?? "", request, callback);
    }

    public Task CallService<T>(string name, string type, Action<T> callback) where T : ServiceResponseBase
    {
        return CallService(name, type, null, callback);
    }

    public Task CallService(string name, string type, Action<string> callback)
    {
        return CallService(name, type, null, callback);
    }

    public Task CallService<T>(string name, string type, IServiceRequest? request, Action<T> callback) where T : ServiceResponseBase
    {
        return CallService(name, type, request, (json) => DeserializeAndCall(json, callback));
    }

    public async Task CallService(string name, string type, IServiceRequest? request , Action<string> callback)
    { 
        lastServiceId++;
        serviceDict[lastServiceId] = callback;
        await InvokeVoid("callService", name, type, request?.ArgNames ?? Array.Empty<string>(), request?.Args ?? Array.Empty<string>(),
            lastServiceId, objRef, nameof(OnServiceResponse));
    }

#endregion

#region JS Callbacks

    [JSInvokable]
    public void OnConnection()
    {
        ErrorMsg = "";
        State = RosInteropState.Connected;
    }

    [JSInvokable]
    public void OnClose()
    {
        ErrorMsg = "";
        State = RosInteropState.ConnectionClosed;
    }

    [JSInvokable]
    public void OnError(string errorMsg)
    {
        //TODO evtl in C# auswerten
        ErrorMsg = errorMsg;
        State = RosInteropState.ConnectionError;
    }

    [JSInvokable]
    public void OnGetTopics(string[] names, string[] types)
    {
        List<Topic> list = new List<Topic>();
        int count = names.Length;
        Console.WriteLine($"Got {count} topics");
        for (int i=0; i < count; i++)
        {
            list.Add(new Topic() { Name = names[i], MessageType = types[i] });
        }
        foreach(var action in allTopicSubscribers)
        {
            action.Invoke(list);
        }
        allTopicSubscribers.Clear();
    }

    [JSInvokable]
    public void OnGetServices(string[] names)
    {
        List<IService> list = new List<IService>();
        int count = names.Length;
        Console.WriteLine($"Got {count} services");
        for (int i = 0; i < count; i++)
        {
            list.Add(new Service() { Name = names[i]});
        }
        foreach (var action in allServicesSubscribers)
        {
            action.Invoke(list);
        }
        allServicesSubscribers.Clear();
    }

    [JSInvokable]
    public void OnTopic(string name, string messageType, string json)
    {
        foreach(var key in topicDict.Keys)
        {
            if((key.Name?.Equals(name) ?? false)
                && (key.Name?.Equals(name) ?? false))
            {
                var entry = topicDict[key];
                foreach(var action in entry)
                {
                    action(name, messageType, json);
                }
            }
        }
    }

    [JSInvokable]
    public void OnServiceResponse(int id, string json)
    {
        if (serviceDict.ContainsKey(id))
        {
            serviceDict[id]?.Invoke(json);
            serviceDict.Remove(id);
        }
        else
        {
            Console.WriteLine($"ServiceResponse for key {id} ignored: no listener registered");
        }
        
    }
#endregion

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }

#region helpers
    private void DeserializeAndCall<T>(string json, Action<T> callback)
    {
        try
        {
            var msg = JsonSerializer.Deserialize<T>(json);
            if (msg is T validMsg)
            {
                callback(validMsg);
            }
            else
            {
                Console.WriteLine("Desirializing failed");
            }
        }
        catch(Exception e) {
            Console.WriteLine($"Desirializing '{json}' into {typeof(T).FullName} failed: {e.Message}");
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

    private string ProtocolString(RosInteropProtocol protocol)
    {
        return protocol switch
        {
            RosInteropProtocol.WS => "ws",
            _ => "wss"
        };
    }
#endregion
}

public class TopicDictKey
{
    public string? Name;
    public string? MesssageType;

    public override int GetHashCode()
    {
        return Name?.GetHashCode() ?? 0;
    }

    public override bool Equals(object? obj)
    {
        if(obj is TopicDictKey other)
        {
            return (other.Name?.Equals(Name) ?? false)
                && (other.MesssageType?.Equals(MesssageType) ?? false);
        }
        return false;
    }
}


