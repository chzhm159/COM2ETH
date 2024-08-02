# COM2ETH
串口转TCP工具
![功能设计图](https://github.com/chzhm159/COM2ETH/blob/main/imgs/design.PNG?raw=true)

## 组件介绍

* Serial Port Device: 各种串口通信的设备
* Serial Port Stream (PC)
  - 运行在PC上的串口通信组件
  - 支持串口数据读取和写入

* Net Serial Port(库): Serial Port Stream 的包装工具,主要有2个作用:
  - 可作为 Library 嵌入到自己的程序中,可以完成对PC上串口的读取和写入
  - 可作为 Serial Port Net Server 的 Client 端. 实现与 Serial Port Net Server 的读取和写入.

* Serial Port Net Server: Serial Port Stream 的包装工具. 实现将串口(COM)与网络端口(Port)的绑定,特性如下:
  - 多绑定: 例如 COM1 <-> 9001,COM2 <-> 9002,COM3 <-> 9003. 可以将PC上多个COM口映射为 Socket端口.
  - 多协议: 客户端协议支持 TCP/IP,MQTT,HTTP等
  - 多系统: 支持Window,Linux
  - 可作为独立服务: 可作为串口数据采集服务独立运行,用于数据采集.

## 使用方式

* 串口服务器:
  - 在接有串口设备的电脑(工控机/PC/IPC)上安装本程序,配置好COM口与网络端口的映射关系后.即可实现通过网络读写COM口所链接的设备(即软件版串口转网口模块)

* 远程调试: 当现有电脑没有COM口(DB9)时实现远程调试
  - 程序中使用 Net Serial Port库.
  - 在连接了串口设备的电脑上(工控机/PC/IPC),安装本程序,然后以 Serial Port Net Server 的形式启动.
  - Net Serial Port库设置为网络模式,即可与 Serial Port Net Server 通信.实现对串口设备数据的读写.
  - 调试完毕之后,Net Serial Port库设置为Direct模式即可.无需修改任何业务代码.