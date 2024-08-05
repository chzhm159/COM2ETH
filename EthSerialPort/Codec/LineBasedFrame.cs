using com2eth.serialport.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthSerialPort.Codec
{
    /// <summary>
    /// 以 \n (0x0A) 或者 \r\n(0x0D 0x0A) 为结束符的数据帧解析类
    /// </summary>
    internal class LineBasedFrame : IDataFrameHandler
    {
        /// <summary>
        /// 默认以 \r(0A )为结束符
        /// </summary>
        private byte[] endLine = new byte[1] {0x0A};
        internal LineBasedFrame(byte[] endLine) { }

        byte[] IDataFrameHandler.Decode(byte[] data) {

            return null;
        }
    }
}
