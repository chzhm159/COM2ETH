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
    internal class SerialPort
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

        internal event EventHandler<SerialDataReceivedEventArgs> DataReceived;

        internal event EventHandler<SerialErrorReceivedEventArgs> ErrorReceived;

        internal event EventHandler<SerialPinChangedEventArgs> PinChanged;

        internal SerialPort() {
            
        }
        internal void Config(string com,int baudRate,string parity,int dataBits,string stopBits) {
            this.PortName = com;
            this.BaudRate = baudRate;
            this.Parity = parity;
            this.DataBits = dataBits;
            this.StopBits = stopBits;
        }

        //internal SerialPortStream GetSerialPortStream() {
        //    return this.com;
        //}
        internal bool Open() {
            try {

                com = new SerialPortStream();
                com.PortName = this.PortName;
                com.BaudRate = this.BaudRate;

                com.Parity = MaperParity(this.Parity);
                com.DataBits = this.DataBits;
                com.StopBits = MaperStopBits(this.StopBits);
                com.DataReceived += DataReceivedHandler;
                com.ErrorReceived += ErrorReceivedHandler;
                com.PinChanged += PinChangedHandler;
                com.Open();
                return true;
            } catch (Exception ex) {
                log.ErrorFormat("COM:[{0}] 打开失败! \r\n {1},{2}", this.PortName, ex.Message, ex.StackTrace);
                return false;
            }

        }

        internal void DataReceivedHandler(object? sender, RJCP.IO.Ports.SerialDataReceivedEventArgs args) {
            log.InfoFormat("收到数据:{0},{1}",sender, args.ToString());
            this.DataReceived?.Invoke(this, args);

        }
        internal void ErrorReceivedHandler(object? sender, RJCP.IO.Ports.SerialErrorReceivedEventArgs args) {
            log.InfoFormat("数据失败:{0}", args.ToString());
            this.ErrorReceived?.Invoke(this, args);
        }
        internal void PinChangedHandler(object? sender, RJCP.IO.Ports.SerialPinChangedEventArgs args) {
            log.InfoFormat("PinChanged事件:{0}", args.ToString());
            this.PinChanged?.Invoke(this, args);
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
        internal bool Write(byte[] msg) {
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
