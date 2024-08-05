using com2eth.serialport;
using com2eth.serialport.Codec;
using NetCoreServer;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace com2eth {
    internal class Program {
        static void Main(string[] args) {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("config/log4net.config"));
            NetSerialPort netComd = new NetSerialPort();
            AppCfg appcfg = AppCfg.Inst;
            string env = AppCfg.GetString("env");
            Console.WriteLine("Hello, World!");
        }

        static void Demo() {
            NetSerialPort netSerialPort = new NetSerialPort();
            netSerialPort.Config();
            netSerialPort.DataReceived += DataHandler;
            bool suc =netSerialPort.Open();


        }

        private static void DataHandler(object? sender, IFrame e) {
            
        }
    }
    
    class ChatSession : TcpSession {
        public ChatSession(TcpServer server) : base(server) { }

        protected override void OnConnected() {
            Console.WriteLine($"Chat TCP session with Id {Id} connected!");

            // Send invite message
            string message = "Hello from TCP chat! Please send a message or '!' to disconnect the client!";
            SendAsync(message);
        }

        protected override void OnDisconnected() {
            Console.WriteLine($"Chat TCP session with Id {Id} disconnected!");
        }

        protected override void OnReceived(byte[] buffer, long offset, long size) {
            string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            Console.WriteLine("Incoming: " + message);

            // Multicast message to all connected sessions
            Server.Multicast(message);

            // If the buffer starts with '!' the disconnect the current session
            if (message == "!")
                Disconnect();
        }

        protected override void OnError(SocketError error) {
            Console.WriteLine($"Chat TCP session caught an error with code {error}");
        }
    }

    class ChatServer : TcpServer {
        public ChatServer(IPAddress address, int port) : base(address, port) { }

        protected override TcpSession CreateSession() {
            return new ChatSession(this);
        }

        protected override void OnError(SocketError error) {
            Console.WriteLine($"Chat TCP server caught an error with code {error}");
        }
    }
}
