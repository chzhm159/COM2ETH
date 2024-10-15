using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RJCP.IO.Ports;
using log4net;
using com2eth.serialport;
using EthSerialPort.Codec;
using EthSerialPort;
using com2eth.serialport.Codec;

namespace com2eth.server
{
   
    /// <summary>
    /// 提供 Tcp Server 的通道
    /// </summary>
    internal class TcpServerChannel : TcpServer
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TcpServerChannel));
        
        

        // SerialPortCfg comCfg;
        public TcpServerChannel(IPAddress address, int port) : base(address, port) {
            // this.comCfg =  spc;
            this.OptionNoDelay = true;
        }

       

        public override bool Start() {
            bool tcpStartSuc = base.Start();

            return tcpStartSuc;
        }
        protected override void OnStarting() {
        
        }

        /// <summary>
        /// Handle server started notification
        /// </summary>
        protected override void OnStarted() { 
        
        }

        /// <summary>
        /// Handle server stopping notification
        /// </summary>
        protected override void OnStopping() { }
        /// <summary>
        /// Handle server stopped notification
        /// </summary>
        protected override void OnStopped() { }

        protected override TcpSession CreateSession() {
            return new C2TSession(this);
        }

        protected override void OnError(SocketError error) {
            log.ErrorFormat("TCP server caught an error with code {0}",error.ToString());
        }
        public event EventHandler<IFrame>? DataReceived;
    }
    
}
