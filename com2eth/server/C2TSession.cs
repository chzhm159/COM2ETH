using com2eth.serialport.Codec;
using log4net;
using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace com2eth.server
{
    internal class C2TSession: TcpSession
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(C2TSession));
        string connected_ack = "cid:{0}";

        internal event EventHandler<TcpMsg>? DataReceived;

        public C2TSession(TcpServer server) : base(server) { 
            
        }

        protected override void OnConnected() {
            log.InfoFormat("TCP Client connected! Id:{0}",Id);
            // Send ack message
            string ack = string.Format(connected_ack, Id);
            SendAsync(ack);
        }

        protected override void OnDisconnected() {
            log.InfoFormat("Chat TCP disconnected! Id {0}", Id);
        }

        protected override void OnReceived(byte[] buffer, long offset, long size) {
            // string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            // log.InfoFormat("Incoming: " + message);

            // Multicast message to all connected sessions
            // Server.Multicast(message);

            // If the buffer starts with '!' the disconnect the current session
            //if (message == "!")
            //    Disconnect();
            TcpMsg msg = new TcpMsg(buffer,offset,size);
            DataReceived?.Invoke(this,msg);
        }

        protected override void OnError(SocketError error) {
            log.Error($"Chat TCP session caught an error with code {error}");
        }
    }
    public class TcpMsg : EventArgs
    {
        public TcpMsg( ) { }
        public TcpMsg(byte[] buf,long ofs,long s) {
            this.buffer = buf;
            this.offset = ofs;
            this.size   = s;
        }
        public byte[]? buffer { get; set; }
        public long offset { get; set; }
        public long size { get; set; }

    }
}
