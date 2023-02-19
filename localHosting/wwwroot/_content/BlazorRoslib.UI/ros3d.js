import { ros } from '../BlazorRoslib.Core/rosInterop.js'

var tfClient;
var viewer;
const pcClients = new Array();
const scanClients = new Array();

export function initRos3d() {
	console.log('Init viewer')
	viewer = new ROS3D.Viewer({
		divID: 'viewerDiv',
		width: 800,
		height: 600,
		antialias: true
	});
	// Setup a client to listen to TFs.
	tfClient = new ROSLIB.TFClient({
		ros: ros,
		angularThres: 0.01,
		transThres: 0.01,
		rate: 10.0,
	});
	viewerDiv = document.getElementById("viewerDiv")
	new ResizeObserver(() => viewer.resize(viewerDiv.offsetWidth, viewerDiv.offsetHeight)).observe(viewerDiv);
}

export function updateFixedFrame(frameId) {
	tfClient.fixedFrame = frameId;
	tfClient.updateGoal();
}

export function addPointCloud(topic) {
	var tmpSub = new ROS3D.PointCloud2({
		ros: ros,
		tfClient: tfClient,
		rootObject: viewer.scene,
		topic: topic,
		material: { size: 0.03, color: 0x0000ff },
		max_pts: 10000000
	});
	var listenerEntry = new Object();
	listenerEntry['topic'] = topic;
	listenerEntry['pc2'] = tmpSub;
	pcClients.push(listenerEntry);
}

export function addLaserScan(topic) {
	var tmpSub = new ROS3D.LaserScan({
		ros: ros,
		tfClient: tfClient,
		topic: topic,
		rootObject: viewer.scene,
		material: { size: 0.05, color: 0x00ff00 }
	});
	var listenerEntry = new Object();
	listenerEntry['topic'] = topic;
	listenerEntry['scan'] = tmpSub;
	scanClients.push(listenerEntry);
}
