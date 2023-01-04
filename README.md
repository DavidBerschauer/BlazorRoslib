# BlazorRoslib
Based on
- [roslib.js](https://github.com/RobotWebTools/roslibjs)
- [Blazor Webassembly](https://learn.microsoft.com/de-de/aspnet/core/blazor/?view=aspnetcore-6.0#blazor-webassembly)
- [MudBlazor](https://github.com/MudBlazor/MudBlazor/)

## GitHub Pages: [Blazor Toolbox for ROS](https://davidberschauer.github.io/BlazorRoslib/)


## ROS Setup
Example launchfile: [webApp.launch](/ROS%20example/webApp.launch)

Please read the following documentation!

### Topics / Services / Params
BlazorRoslib requires a [rosbridge_websocket Node](http://wiki.ros.org/rosbridge_suite/Tutorials/RunningRosbridge).

When BlazorRoslib is accessed via https (required with GitHub Pages), the browser requires a SSL connection to ROS. 

Therefore the rosbridge server has to be setup for wss ([originally described here](https://github.com/UbiquityRobotics/speech_commands#Installation)):

1. Set up a self-signed certificate ("snakeoil") (usually already done by ubuntu)
2. Copy /etc/ssl/private/ssl-cert-snakeoil.key to /etc/ssl/certs/ (may need sudo)
3. chmod /etc/ssl/certs/ssl-cert-snakeoil.key so it is readable. /etc/ssl/certs/ssl-cert-snakeoil.pem should already be readable.
    ```bash
    sudo chmod 644 /etc/ssl/certs/ssl-cert-snakeoil.key
    ```
3. Setup a launchfile 
    ```xml
    <launch>
        <include file="$(find rosbridge_server)/launch/rosbridge_websocket.launch"> 
            <arg name="ssl" default="true" />
            <arg name="certfile" default="/etc/ssl/certs/ssl-cert-snakeoil.pem" />
            <arg name="keyfile" default="/etc/ssl/certs/ssl-cert-snakeoil.key" />
        </include>
    </launch>
    ```
    OR add these args to ``` /opt/ros/kinetic/share/rosbridge_server/launch/rosbridge_websocket.launch```
    ```xml
    <arg name="ssl" default="true" />
    <arg name="certfile" default="/etc/ssl/certs/ssl-cert-snakeoil.pem" />
    <arg name="keyfile" default="/etc/ssl/certs/ssl-cert-snakeoil.key" />
    ```
5. This self-signed certificate is usually not accepted by the browser. As a workaround, you can open the full wss-URL in the browser and manually accept the certificate. You can find the required link on the ROS-Settings page.

To avoid using SSL you can setup a webserver for static files (e.g nginx) and host your own ROS-Toolbox.

### Live Video
For live video a [web_video_server node](http://wiki.ros.org/web_video_server) is required. 

Setup:

Add the node to a launchfile:

```xml
<node name="web_video_server" pkg="web_video_server" type="web_video_server" output="screen" />
```
OR 

Start the node from the console:
```sh
rosrun web_video_server web_video_server
```

### Rosbag Recording
For remote recording rosbags the [rosbag_recording_services](https://github.com/DavidBerschauer/rosbag_recording_services) is required. Please follow the installation instructions over there.

