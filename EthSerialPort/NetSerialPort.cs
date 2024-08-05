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
        public event EventHandler<IFrame> DataReceived;
        public NetSerialPort() {
            
        }
        public void Config() {
            // 1. 设置模式
            // 2. 
        }
        public bool Open(bool retry = true) {
            return false;
        }
        private void OnDataReceived(IFrame data) {
            if (DataReceived != null) {
                DataReceived(this, data);
            }
        }
        public bool Write(byte[] data) {
            // 
            return false;
        }
        public void Close() { 
        }
    }
}
