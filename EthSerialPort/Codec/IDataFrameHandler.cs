using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com2eth.serialport.Codec
{
    internal interface IDataFrameHandler
    {
        internal byte[] Decode(byte[] data);

    }
}
