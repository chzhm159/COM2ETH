using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com2eth.connector
{
    internal interface IConnector
    {
        internal EndpointType[] GetEndpointType() ;
    }
}
