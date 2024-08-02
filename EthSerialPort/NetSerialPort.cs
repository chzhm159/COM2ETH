using RJCP.IO.Ports;
namespace com2eth.serialport {
    /// <summary>
    // 网络串口:本类具有2中形态:
    // 1.串口模式: 这就是普通串口通信方式
    // 2.Tcp Client模式: 切换为一个Tcp Client 模式,方便与串口服务器通信
    /// </summary>
    public class NetSerialPort {
        /// <summary>
        ///RJCP.IO.Ports.SerialPortStream 对象
        /// </summary>
        private SerialPortStream serialPort;
        /// <summary>
        /// Com口号,例如 COM2
        /// </summary>
        public string PortName { get; private set; }
        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate { get; private  set; }
        /// <summary>
        /// 奇偶校验 
        /// </summary>
        public string Parity { get; private set; }
        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits { get; private set; }
        /// <summary>
        /// 停止位
        /// </summary>
        public string StopBits { get; private set; }

        public event EventHandler<SerialErrorReceivedEventArgs> ErrorReceived;

        public event EventHandler<SerialDataReceivedEventArgs> DataReceived;

        public event EventHandler<SerialPinChangedEventArgs> PinChanged;

        
    }
}
