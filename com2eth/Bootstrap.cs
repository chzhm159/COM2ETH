using com2eth.connector;
using com2eth.serialport;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com2eth
{
    internal class Bootstrap
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Bootstrap));
        List<Type> Connectors = new List<Type>();
        internal Bootstrap() {
            Config();
        }
        internal void Config() {
            // TODO 这里后续重构吧.先硬编码
            Connectors.Add(typeof(ComAndTcpServerConnector));
        }
        internal Task<bool> BootAsync(string[] args) {
            return Task.Run(Task<bool>? () => {

                bool hasService = false;
                // 加载配置文件,判断 bind 类型.
                BindUtils utils = BindUtils.Inst;
                utils.Load();
                if (utils.Bundles == null || utils.Bundles.bundles == null) {
                    log.Error("未能加载Bind信息!");
                    return Task.FromResult(hasService) ;
                }
                string tcpServer = StringEnum.GetStringValue(EndpointType.TCP_SERVER);
                string serialPort = StringEnum.GetStringValue(EndpointType.SERIALPORT);

                utils.Bundles.bundles.ForEach(bind => {
                    bool matched = MatchHandler(bind);
                    if (!hasService && matched) {
                        hasService = true;
                    }
                });
                return Task.FromResult(hasService);
            });            
        }
        private bool MatchHandler(EndpointMapper endpoints) {
            bool hasService = false;
            if(endpoints.endpoint_a==null || endpoints.endpoint_b==null) { return hasService; }

            Connectors.ForEach(cntType => {
                try {
                    
                    System.Reflection.FieldInfo? acceptEndpoints = cntType.GetField("Endpoints");
                    if (acceptEndpoints == null) {
                        return;
                    }
                    EndpointType[]? canHandler = new EndpointType[2];                    
                    canHandler = (EndpointType[])acceptEndpoints.GetValue(null)!;

                    EndpointType epA = StringEnum.FromStringValue(endpoints.endpoint_a.type);
                    EndpointType epB = StringEnum.FromStringValue(endpoints.endpoint_b.type);
                    if (canHandler.Contains(epA) && canHandler.Contains(epB)) {
                        // BindingFlags flags =  BindingFlags.Public;
                        object? service = Activator.CreateInstance(cntType);
                        System.Reflection.MethodInfo? setEndpoint = cntType.GetMethod("SetEndpoint");
                        System.Reflection.MethodInfo? Start = cntType.GetMethod("Start");
                        if (setEndpoint == null || Start == null) {
                            log.ErrorFormat("链接器:{0},未定义 SetEndpoint 和 Start方法", cntType.FullName);
                            return;
                        }
                        setEndpoint.Invoke(service, new object[] { endpoints.endpoint_a, endpoints.endpoint_b });
                        Start.Invoke(service, null);
                        hasService = true;
                    }
                } catch (Exception ex) {
                    log.ErrorFormat("链接器处理异常:{0},{1}",ex.Message,ex.StackTrace);
                }
                
            });
            return hasService;
        }
    }
}
