using com2eth.connector;
using com2eth.serialport;
using com2eth.serialport.Codec;
using com2eth.server;
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
        private readonly static ManualResetEvent _shutdownBlock = new ManualResetEvent(false);
        static void Main(string[] args) {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("config/log4net.config"));

            Bootstrap bootstrap = new Bootstrap();
            bootstrap.BootAsync(args).ContinueWith(st =>{
                log.InfoFormat("启动完成,匹配到服务:{0}", st.Result);
                if (!st.Result) {
                    _shutdownBlock.Set();
                }
            });
            _shutdownBlock.WaitOne();
        }
        private static void Console_CancelPressed(object sender, ConsoleCancelEventArgs e) { 
            e.Cancel = true;
            _shutdownBlock.Set();
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
    }    
}
