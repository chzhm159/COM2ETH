using com2eth.serialport;
using com2eth.serialport.Codec;
using EthSerialPort;
using EthSerialPort.Codec;
using log4net;
using NetCoreServer;
using System.Formats.Tar;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace com2eth {
    internal class Program {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args) {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("config/log4net.config"));

            LineBaseDecoderTest();
            Demo();
            while (true) {
                var name = Console.ReadLine();
                
            }

        }
        static void LineBaseDecoderTest() {
            LineBasedFrame decoder = new LineBasedFrame("\r\n");
            string str = "abcde\r\n";
            byte[] p1 = new byte[] { 0x61, 0x62, 0x63 };
            byte[] p2 = new byte[] { 0x64, 0x65 };
            byte[] p3 = new byte[] { 0x0D };
            byte[] p4 = new byte[] { 0x0A, 0x61, 0x62, 0x63 };
            byte[] p5 = new byte[] { 0x64, 0x65, 0x0D, 0x0A };
            byte[] p10 = new byte[] { 0x61, 0x62, 0x63, 0x64, 0x65, 0x0D, 0x0A };
            byte[] p20 = new byte[] { 0x61, 0x62, 0x63, 0x64, 0x65, 0x0D, 0x0A, 0x61, 0x62, 0x63, 0x64, 0x65, 0x0D, 0x0A };
            byte[][] test = new byte[][] {p1,p2,p3,p4,p5,p10,p20 };
            for (int i = 0; i < test.Length; i++) {
                List<IFrame>? FList = decoder.DecodeWithByte(test[i]);
                if (FList != null) {
                    FList.ForEach(f => {
                        bool matched = string.Equals(str,f.String);
                        log.DebugFormat("收到完整数据包:raw:[{0}], string:[{1}],与预期是否一致:{2}",string.Join("-",f.RawBytes),f.String, matched);
                    });
                }
            }
            
        }
        static void Demo() {
            Task.Run(() => {
                LineBasedFrame decoder = new LineBasedFrame("\r\n");



                NetSerialPort netSerialPort = new NetSerialPort("com", decoder);
                NetSerialPortOptions opt =new NetSerialPortOptions();
                netSerialPort.Config(opt);
                netSerialPort.DataReceived += DataHandler;
                bool suc = netSerialPort.Open();
            });

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
