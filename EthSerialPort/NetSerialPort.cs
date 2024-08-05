using com2eth.serialport.Codec;
using log4net;
using RJCP.IO.Ports;
namespace com2eth.serialport {
    /// <summary>
    /// <para> 网络串口:实现将串口绑定到各种网络协议</para>
    /// <para> Direct: 直接模式,即就是本地直接调用串口</para>
    /// <para> TcpSerialPort:</para>
    /// 
    /// </summary>
    public class NetSerialPort {
        private static readonly ILog log = LogManager.GetLogger(typeof(NetSerialPort));
        //public event EventHandler<IFrame> DataReceived;
        SerialPort serialPort;
        public NetSerialPort() {
            
        }
        public void Config() {
            // 1. 设置模式
            // 2. 
            serialPort = new SerialPort();
            serialPort.Config("COM1", 9600, "none", 8, "2");
            serialPort.DataReceived += DataReceivedHandler;
        }
        public void DataReceivedHandler(Object sender, SerialDataReceivedEventArgs args) {
            SerialPort? com = sender as SerialPort;
            if (com != null) {
                com.WriteLine("hello world");
            }
        }
        public bool Open(bool retry = true) {
            bool suc = serialPort.Open();
            log.InfoFormat("串口开启状态:{0}",suc);
            return suc;
        }
        private void OnDataReceived(IFrame data) {
            //if (DataReceived != null) {
            //    DataReceived(this, data);
            //}
        }
        public bool Write(byte[] data) {
            // 
            return false;
        }
        public void Close() { 
        }
    }
}
