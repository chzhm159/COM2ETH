using com2eth.connector;
using com2eth.serialport;
using log4net;
using Microsoft.VisualBasic.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com2eth {
    internal class MainService {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainService));
        private static readonly Lazy<MainService> _ms = new Lazy<MainService>(() => new MainService());
        public static MainService Inst => _ms.Value;
        private Main main;
        public string ConfigDir = "config";
        string BindsName = "binds.json";
        private MainService() {}
        public void SetMain(Main main) {
            this.main = main;
        }

        internal void Load() {
            LoadBinds();
        }
        public Bundles Current { get; private set; }
        private string bindFP;
        public void LoadBinds() {            
            string bFP = Path.Combine(ConfigDir, BindsName);

            // 加载配置文件,判断 bind 类型.
            BindUtils utils = BindUtils.Inst;
            Bundles bindingMap = utils.Load(bFP);
            Current = bindingMap;
            bindFP = bFP;
            main.UpdateBundles(bindingMap);            
        }
        public  T? DeepClone<T>(T obj) {
            if (obj == null) {
                return default;
            }

            // 使用 JSON 序列化和反序列化来实现深度克隆
            string json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(json);
        }

        internal void ShowMsg(string msg, params object?[] args) {
            string txt = string.Format(msg,args);
            this.main?.OnMsg(txt);
        }
        internal bool Save(EndpointMapper endpointMapper) {
            if (Current == null || Current.bundles==null) {
                this.main?.OnMsg("未加载任何绑定文件!");
                return false;
            }

            int idx = Current.bundles.FindIndex(bundle => {
                return string.Equals(bundle.name, endpointMapper.name);
            });
            
            if (idx <0) {
                this.main?.OnMsg("未查找到对应的配置");
                return false;
            }
            EndpointEntity? newEA = DeepClone<EndpointEntity>(endpointMapper.endpoint_a);
            EndpointEntity? newEB = DeepClone<EndpointEntity>(endpointMapper.endpoint_b);
            JsonSerializerSettings jss = new JsonSerializerSettings();
            jss.NullValueHandling = NullValueHandling.Ignore;
            // newEA.ShouldSerializedatabits = false;
            Current.bundles[idx].endpoint_a = newEA;

            Current.bundles[idx].endpoint_b = newEB;
            // newEB.ShouldSerializeport = false;

            string json = JsonConvert.SerializeObject(Current,Formatting.Indented, jss);
            return AppCfg.SaveToFile(bindFP,json);
        }
    }
}
