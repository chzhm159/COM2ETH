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
        
        public TcpServerChannel(IPAddress address, int port) : base(address, port) {        
            this.OptionNoDelay = true;
        }

       

        public override bool Start() {
            bool tcpStartSuc = base.Start();
            log.InfoFormat("TCP Server 启动 suc:{0}", tcpStartSuc);
            return tcpStartSuc;
        }
        protected override void OnStarting() {
        
        }

        /// <summary>
        /// Handle server started notification
        /// </summary>
        protected override void OnStarted() {
            log.InfoFormat("TCP Server 已启动 ip:{0},prot:{1}", this.Address.Normalize(),this.Port);
        }

        /// <summary>
        /// Handle server stopping notification
        /// </summary>
        protected override void OnStopping() {
            log.InfoFormat("TCP Server 停止中... ip:{0},prot:{1}", this.Address.Normalize(), this.Port);
        }
        /// <summary>
        /// Handle server stopped notification
        /// </summary>
        protected override void OnStopped() {
            log.InfoFormat("TCP Server 已停止... ip:{0},prot:{1}", this.Address.Normalize(), this.Port);
        }

        protected override TcpSession CreateSession() {
            C2TSession sess = new C2TSession(this);
            sess.DataReceived += this.DataReceived;
            return sess;
        }

        protected override void OnError(SocketError error) {
            log.ErrorFormat("TCP Server 出现异常 {0}",error.ToString());
        }
        public event EventHandler<TcpMsg>? DataReceived;
    }
    
}
