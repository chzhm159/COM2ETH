using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthSerialPort
{
    internal interface IPipeline {
        public string IPipelneName { get; set; }
        public void Config(NetSerialPortOptions opt);
        public bool Open();
        public bool Write(byte[] data);
        public void Close();
    }
    /// <summary>
    /// 大杂烩,包含所有配置项
    /// </summary>
    public class NetSerialPortOptions
    {
        /// <summary>
        /// Com口号,例如 COM2
        /// </summary>
        public string PortName { get;  set; }
        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate { get;  set; }
        /// <summary>
        /// 奇偶校验 
        /// </summary>
        public string Parity { get;  set; }
        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits { get;  set; }
        public string StopBits { get;  set; }
    }
}
