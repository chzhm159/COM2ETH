using EthSerialPort;
using log4net;
using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com2eth.serialport
{
    internal class SerialPort: IPipeline
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(NetSerialPort));
        /// <summary>
        ///RJCP.IO.Ports.SerialPortStream 对象
        /// </summary>
        private SerialPortStream com;
        /// <summary>
        /// Com口号,例如 COM2
        /// </summary>
        internal string PortName { get; private set; }
        /// <summary>
        /// 波特率
        /// </summary>
        internal int BaudRate { get; private set; }
        /// <summary>
        /// 奇偶校验 
        /// </summary>
        internal string Parity { get; private set; }
        /// <summary>
        /// 数据位
        /// </summary>
        internal int DataBits { get; private set; }
        /// <summary>
        /// 停止位
        /// </summary>
        internal string StopBits { get; private set; }
        string IPipeline.IPipelneName {
            get => GetName();
            set => SetName(value);
        }
        private string GetName() {
            return "";
        }
        private void SetName(string name) {
            
        }
        internal event EventHandler<SerialDataReceivedEventArgs>? DataHandler;

        internal event EventHandler<SerialErrorReceivedEventArgs>? ErrorHander;

        internal event EventHandler<SerialPinChangedEventArgs>? PinChangedHandler;

        
        private int _state = 0;
        void IPipeline.Config(NetSerialPortOptions opt) {
            this.PortName = opt.PortName;
            this.BaudRate = opt.BaudRate;
            this.Parity = opt.Parity;
            this.DataBits = opt.DataBits;
            this.StopBits = opt.StopBits;
            _state = 1;
        }
        internal SerialPortStream GetComStream() {
            return com;
        }
        void IPipeline.Close() {

        }

        bool IPipeline.Open() {
            
            try {
                
                com = new SerialPortStream();
                com.PortName = this.PortName;
                com.BaudRate = this.BaudRate;

                com.Parity = MaperParity(this.Parity);
                com.DataBits = this.DataBits;
                com.StopBits = MaperStopBits(this.StopBits);
                com.DataReceived += DataHandler;
                com.ErrorReceived += ErrorHander;
                com.PinChanged += PinChangedHandler;
                com.Open();
                return true;
            } catch (Exception ex) {
                log.ErrorFormat("COM:[{0}] 打开失败! \r\n {1},{2}", this.PortName, ex.Message, ex.StackTrace);
                return false;
            }

        }

        
        internal void Close() {
            try {
                com.Close();
                com.Dispose();
                com = null;
            }catch(Exception ex) {
                log.ErrorFormat("COM[{0}],关闭异常:{1},{2}",this.PortName,ex.Message,ex.StackTrace);
            }
        }


        internal bool WriteLine(string msg) {
            if (com == null || !com.CanWrite) {
                return false;
            }
            com.WriteLine(msg);
            return true;
        }
        bool IPipeline.Write(byte[] msg) {
            if (com == null || !com.CanWrite) {
                return false;
            }
            com.Write(msg);            ;
            return true;
        }


        #region 工具函数

        private RJCP.IO.Ports.Parity MaperParity(string parity) {
            string pstr = parity.ToLower();
            RJCP.IO.Ports.Parity p = RJCP.IO.Ports.Parity.None;
            switch (pstr) {
                case "even":
                    p = RJCP.IO.Ports.Parity.Even;
                    break;
                case "mark":
                    p = RJCP.IO.Ports.Parity.Mark;
                    break;
                case "none":
                    p = RJCP.IO.Ports.Parity.None;
                    break;
                case "odd":
                    p = RJCP.IO.Ports.Parity.Odd;
                    break;
                case "space":
                    p = RJCP.IO.Ports.Parity.Space;
                    break;
            }
            return p;
        }
        private RJCP.IO.Ports.StopBits MaperStopBits(string stopbits) {
            string sstr = stopbits.ToLower();
            RJCP.IO.Ports.StopBits s = RJCP.IO.Ports.StopBits.One;
            switch (sstr) {
                case "0":
                    s = RJCP.IO.Ports.StopBits.One;
                    break;
                case "1":
                    s = RJCP.IO.Ports.StopBits.One;
                    break;
                case "1.5":
                    s = RJCP.IO.Ports.StopBits.One5;
                    break;
                case "2":
                    s = s = RJCP.IO.Ports.StopBits.Two;
                    break;
            }
            return s;
        }


        
        #endregion
    }
}
