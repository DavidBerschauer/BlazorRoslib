# BlazorRoslib
Based on
- [roslib.js](https://github.com/RobotWebTools/roslibjs)
- [Blazor Webassembly](https://learn.microsoft.com/de-de/aspnet/core/blazor/?view=aspnetcore-6.0#blazor-webassembly)
- [MudBlazor](https://github.com/MudBlazor/MudBlazor/)

## GitHub Pages: [Blazor Toolbox for ROS](https://davidberschauer.github.io/BlazorRoslib/)


## ROS Setup
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
    <include file="$(find rosbridge_server)/launch/rosbridge_websocket.launch"> 
        <arg name="ssl" default="true" />
        <arg name="certfile" default="/etc/ssl/certs/ssl-cert-snakeoil.pem" />
        <arg name="keyfile" default="/etc/ssl/certs/ssl-cert-snakeoil.key" />
    </include>
    ```
    OR add these args to ``` /opt/ros/kinetic/share/rosbridge_server/launch/rosbridge_websocket.launch```
    ```xml
    <arg name="ssl" default="true" />
    <arg name="certfile" default="/etc/ssl/certs/ssl-cert-snakeoil.pem" />
    <arg name="keyfile" default="/etc/ssl/certs/ssl-cert-snakeoil.key" />
    ```
5. This self-signed certificate is usually not accepted by the browser. As a workaround, you can open the full wss-URL in the browser and manually accept the certificate. You can find the required link on the ROS-Settings page.

To avoid using SSL you can setup a webserver for static files (e.g nginx) and host your own ROS-Toolbox.