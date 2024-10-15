using com2eth.serialport.Codec;
using EthSerialPort;
using log4net;
using RJCP.IO.Ports;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.Security.Policy;
using EthSerialPort.Codec;
using System.IO;
namespace com2eth.serialport {
    /// <summary>
    /// 网络串口:可以在tcp客户端与串口通信灵活切换的类.
    /// <para> com: 本地直接调用串口通信</para>
    /// <para> tcp: 作为tcp客户端模式与远程服务端通信</para>
    /// 
    /// </summary>
    public class NetSerialPort {
        private static readonly ILog log = LogManager.GetLogger(typeof(NetSerialPort));
        private NetSerialPortOptions? cfg;
        /// <summary>
        /// 链接成功建立时被调用
        /// </summary>
        public event EventHandler? OnConnected;

        /// <summary>
        /// 当收到数据时被调用
        /// </summary>
        public event EventHandler<IFrame>? DataReceived;


        /// <summary>
        /// 检测到错误
        /// <para> ConnectFailed: 链接失败</para>
        ///  </summary>
        public event EventHandler<EthSerialPortArgs>? ErrorReceived;

        IDataFrameHandler dataFrameHandler;
        private IPipeline pipe;
        public string Model {  get; private set; }
        /// <summary>
        /// <para>tcp: tcp 客户端模式</para>
        /// <para>com: 串口直连模式</para>
        /// </summary>
        /// <param name="model"></param>
        public NetSerialPort(string model, IDataFrameHandler frameDecoder) {
            this.Model = model;
            dataFrameHandler = frameDecoder;
            if (string.Equals(Model, "com", StringComparison.OrdinalIgnoreCase)) {
                SerialPort com = new SerialPort();
                com.DataHandler += ComDataHandler;
                com.ErrorHander += ComErrorHandler;
                com.PinChangedHandler += PinChangedHandler;
                pipe = com;
            } else if (string.Equals(Model, "tcp", StringComparison.OrdinalIgnoreCase)) {
                // pipe = new SerialPort();
            } else {

            }
        }

        public void Config(NetSerialPortOptions opt) {
            cfg = opt;
            pipe.Config(opt);
        }
        
        public bool Open(bool retry = true) {
            if (cfg == null) {
                log.ErrorFormat("请先调用 .Config(opt) 方法. 传入必要的配置参数 ");
                return false;
            }
            bool suc = pipe.Open();
            log.InfoFormat("串口开启状态:{0}",suc);
            return suc;
        }
        public bool Write(byte[] data) {
            bool suc = pipe.Write(data);
            return false;
        }
        public void Close() {
            pipe.Close();
        }
        internal void ComDataHandler(object? sender, RJCP.IO.Ports.SerialDataReceivedEventArgs args) {            
            log.InfoFormat("COM口 收到数据:{0},{1}", sender, args.ToString());
            SerialPortStream? stream = sender as SerialPortStream;
            if (stream == null ) { 
                return; 
            }
            // SerialPortStream stream = com.GetComStream();
            int bToRead = stream.BytesToRead;            
            if (!stream.CanRead || bToRead < 1) {
                return;
            }            
            byte[] dataBuffer = new byte[bToRead];
            stream.Read(dataBuffer, 0, bToRead);            
            byte end = dataBuffer[bToRead - 1];

            List<IFrame>? frame = dataFrameHandler.Decode(dataBuffer);
            if (frame  != null){
                frame.ForEach(f => {
                    this.DataReceived?.Invoke(this, f);
                });
                
            }            
        }
        internal void ComErrorHandler(object? sender, RJCP.IO.Ports.SerialErrorReceivedEventArgs args) {
            log.InfoFormat("COM口 数据失败:{0}", args.ToString());
            
        }
        internal void PinChangedHandler(object? sender, RJCP.IO.Ports.SerialPinChangedEventArgs args) {
            log.InfoFormat("COM口 PinChanged事件:{0}", args.ToString());
            
        }
        
        
    }
}
