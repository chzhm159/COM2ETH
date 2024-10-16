using com2eth.serialport;
using log4net;
using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpClient = NetCoreServer.TcpClient;

namespace EthSerialPort
{
    internal class TcpClientWapper : TcpClient,IPipeline
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TcpClientWapper));
        public TcpClientWapper(DnsEndPoint endpoint) : base(endpoint) {
        }

        public TcpClientWapper(IPEndPoint endpoint) : base(endpoint) {
        }

        public TcpClientWapper(IPAddress address, int port) : base(address, port) {
        }

        public TcpClientWapper(string address, int port) : base(address, port) {
        }
        public void DisconnectAndStop() {
            _stop = true;
            DisconnectAsync();
            while (IsConnected)
                Thread.Yield();
        }

        protected override void OnConnected() {
            log.InfoFormat($"Chat TCP client connected a new session with Id {Id}");
        }

        protected override void OnDisconnected() {
            log.InfoFormat($"Chat TCP client disconnected a session with Id {Id}");

            // Wait for a while...
            Thread.Sleep(1000);

            // Try to connect again
            if (!_stop)
                ConnectAsync();
        }

        protected override void OnReceived(byte[] buffer, long offset, long size) {
            log.DebugFormat(Encoding.UTF8.GetString(buffer, (int)offset, (int)size));
        }

        protected override void OnError(SocketError error) {
            log.ErrorFormat($"Chat TCP client caught an error with code {error}");
        }

        public void Config(NetSerialPortOptions opt) {
            throw new NotImplementedException();
        }

        public bool Open() {
            throw new NotImplementedException();
        }

        public bool Write(byte[] data) {
            throw new NotImplementedException();
        }

        public void Close() {
            throw new NotImplementedException();
        }

        private bool _stop;

        string IPipeline.IPipelneName {
            get => GetName();
            set => SetName(value);
        }
        private string GetName() {
            return "";
        }
        private void SetName(string name) {

        }
    }
}
