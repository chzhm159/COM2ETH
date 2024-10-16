using com2eth.connector;
using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace com2eth.server
{
    /// <summary>
    /// 提供Web访问与配置本软件的服务. 基于NetCoreServer.Http
    /// </summary>
    internal class Com2AdminServer
    {
        internal static EndpointType[] endpoints = new EndpointType[] { EndpointType.TCP_SERVER, EndpointType.SERIALPORT };

    }
}
