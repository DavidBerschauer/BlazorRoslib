export var ros;
const listeners = new Array();


export function initRos(host, port, protocol, dotnetHelper) {
    const full = protocol + '://' + host + ':' + port;
    console.log('Trying to connect to ' + full);
    ros = new ROSLIB.Ros({
        url: full
    });
    ros.on('connection', function () {
        console.log('Connected to websocket server.');
        dotnetHelper.invokeMethodAsync('OnConnection');
    });
    ros.on('error', function (error) {
        console.log('Error connecting to websocket server: ', error);
        dotnetHelper.invokeMethodAsync('OnError', error);
    });
    ros.on('close', function () {
        console.log('Connection to websocket server closed.');
        dotnetHelper.invokeMethodAsync('OnClose');
    });
    listeners.forEach((listenerEntry, index, Array) => {
        listenerEntry['listener'].unsubscribe();
        console.log('Unsubscribed ' + listenerEntry['name']);
        subscribeInternal(listenerEntry);
    });
}

export function getTopics(dotnetHelper, callbackName) {
    ros.getTopics((topics) => {
        dotnetHelper.invokeMethodAsync(callbackName, topics.topics, topics.types);    
    });
}

export function getServices(dotnetHelper, callbackName) {
    ros.getServices((services) => {
        dotnetHelper.invokeMethodAsync(callbackName, services);
    });
}

function subscribeInternal(listenerEntry) {
    var listener = new ROSLIB.Topic({
        ros: ros,
        name: listenerEntry['name'],
        messageType: listenerEntry['messageType']
    });
    listenerEntry['listener'] = listener;
    listenerEntry['listener'].subscribe(function (message) {
        //console.log('Received message on ' + listener.name);

        listenerEntry['dotnetHelper'].invokeMethodAsync(
            listenerEntry['callbackName'],
            listenerEntry['name'],
            listenerEntry['messageType'],
            JSON.stringify(message, null, 3));
    });
    console.log('Subscribed to ' + listenerEntry['name']);
}

export function subscribeTopic(name, messageType, dotnetHelper, callbackName) {
    var listenerEntry = new Object();
    listenerEntry['dotnetHelper'] = dotnetHelper;
    listenerEntry['name'] = name;
    listenerEntry['messageType'] = messageType;
    listenerEntry['callbackName'] = callbackName;
    listeners.push(listenerEntry);
    subscribeInternal(listenerEntry);
}

//Not used yet

export function unsubscribeAll() {
    listeners.forEach((listenerEntry, index, Array) => {
        listenerEntry['listener'].unsubscribe();
        console.log('Unsubscribed ' + listenerEntry['name']);
    });
}

export function callService(name, type, argNames, args, id, dotnetHelper, callbackName) {
    const service = new ROSLIB.Service({
        ros: ros,
        name: name,
        serviceType: type
    });
    const request = new ROSLIB.ServiceRequest();
    argNames.forEach((argName, index, Array) => {
        request[argName] = args[index];
    });
    console.log('Call Service ' + name + ' with arg: ' + JSON.stringify(request, null, 3))
    service.callService(request, function (result) {
        const json = JSON.stringify(result, null, 3)
        dotnetHelper.invokeMethodAsync(callbackName, id, json);
    });
}
