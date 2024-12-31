using com2eth.serialport;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com2eth.connector
{
    internal class BindUtils
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AppCfg));
        private static readonly Lazy<BindUtils> _instance = new Lazy<BindUtils>(() => new BindUtils());
        public static BindUtils Inst => _instance.Value;
        JsonSerializer serializer = new JsonSerializer();
        internal Bundles? Bundles { get; set; }
        internal Bundles Load(string fp ) {
            try {
                using (StreamReader file = File.OpenText(fp)) {  
                    serializer.NullValueHandling = NullValueHandling.Ignore;
                    JsonTextReader textReader = new JsonTextReader(file);
                    Bundles = serializer.Deserialize<Bundles>(textReader);
                }
            } catch (Exception e) {
                log.ErrorFormat("配置文件加载异常:{0},{1}",e.Message,e.StackTrace);
                Bundles = null;
            }
            return Bundles;
        }        
    }
    public class Bundles
    {
        public List<EndpointMapper>? bundles { get; set; }
    }
    public class EndpointMapper
    {
        public string name { get; set; }
        public string desc { get; set; }
        public EndpointEntity? endpoint_a { get; set; }
        public EndpointEntity? endpoint_b { get; set; }
    }

    public class EndpointEntity
    {
        public string name { get; set; }
        public string desc { get; set; }
        public string type {  get; set; }
        public string ip { get; set; } = "127.0.0.1";
        public int port {  get; set; }

        public string addr {  get; set; }
        
        public string com {  get; set; }
        public string parity {  get; set; }
        public int databits {  get; set; }
        public int baudrate {  get; set; }
        public string stopbits {  get; set; }
    }
    internal class StringEnum {
        internal static string GetStringValue(Enum em) {
            Type emType = em.GetType();
            IEnumerable<StringValue> svs = emType.GetCustomAttributes<StringValue>();
            string name="none";
            if (svs.Any()) {
                name = svs.First().Value;
            }
            return name;
        }
        internal static EndpointType FromStringValue(string em) {
            if (string.Equals(em, "tcp_server",StringComparison.OrdinalIgnoreCase)) {
                return EndpointType.TCP_SERVER;
            }else if (string.Equals(em, "serial_port", StringComparison.OrdinalIgnoreCase)) {
                return EndpointType.SERIALPORT;
            } else {
                return EndpointType.TCP_SERVER;
            }
        }
    }
    internal enum EndpointType
    {
        [StringValue("tcp_server")] TCP_SERVER,
        [StringValue("serial_port")] SERIALPORT
    }
    internal class StringValue : System.Attribute
    {
        private string _value;
        public StringValue(string value) {
            _value = value;
        }
        public string Value {
            get { return _value; }
        }
    }
}
