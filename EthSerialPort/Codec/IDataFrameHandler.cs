using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com2eth.serialport.Codec
{
    public interface IDataFrameHandler
    {

        public List<IFrame>? Decode(byte[] data);

    }
}
