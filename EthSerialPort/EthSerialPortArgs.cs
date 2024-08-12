using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthSerialPort
{
    public class EthSerialPortArgs
    {
        private readonly NetSerialPortEventType m_EventType;
        public EthSerialPortArgs(NetSerialPortEventType eventType) {
            m_EventType = eventType;
        }
    }

    
    public enum NetSerialPortEventType 
    {     
        /// <summary>
       /// 链接断开
       /// </summary>
        ConnectFailed = 200        
    }
    
}
