using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace com2eth.serialport {
    public class AppCfg
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AppCfg));
        private static readonly Lazy<AppCfg> _instance = new Lazy<AppCfg>(() => new AppCfg());
        public static AppCfg Inst => _instance.Value;
        private static IConfigurationRoot cfgRoot { get; set; }

        private AppCfg() {
            ConfigurationBuilder appCfgBuider = new ConfigurationBuilder();
            appCfgBuider.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "config"))
            .AddJsonFile("appsettings.json", true, reloadOnChange: true);
            cfgRoot = appCfgBuider.Build();
            
            string env = cfgRoot.GetValue<string>("env") ?? "prod";
            appCfgBuider.AddJsonFile($"appsettings.{env}.json", true, reloadOnChange: true); // 不同环境下的配置      
            cfgRoot = appCfgBuider.Build();
            
        }
        public static string Now() {            
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        }
        public static string Today() {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        
        public static string GetString(string key, string defaultValue = "none") {
            return cfgRoot.GetValue<string>(key) ?? defaultValue;
        }
        public static int GetInt(string key, int defaultValue = -1) {
            int v = cfgRoot.GetValue<int>(key);
            if (v == 0) {
                return defaultValue;
            } else {
                return v;
            }
        }
        // 实现一个将配置信息写会cfgRoot的函数
       

        public static float GetFloat(string key, int defaultValue = -1) {
            float v = cfgRoot.GetValue<float>(key);
            if (v == 0) {
                return defaultValue;
            }
            else {
                return v;
            }
        }
        static JsonSerializer serializer = new JsonSerializer();
        public static T? ReadJson<T>(string fp) {
            try {
                if (!File.Exists(fp)) {
                    log.ErrorFormat("Json文件:{0}  不存在!,已跳过!", fp);
                    return default(T);
                }
                using (StreamReader file = File.OpenText(fp)) {

                    serializer.NullValueHandling = NullValueHandling.Ignore;
                    JsonTextReader textReader = new JsonTextReader(file);

                    object? obj = serializer.Deserialize(textReader, typeof(T));
                    if (obj == null) {
                        return default(T);
                    } else {
                        return (T)obj;
                    }
                }
            } catch (Exception e) {
                log.ErrorFormat("Json文件[{2}]加载异常: {0},{1}", e.Message, e.StackTrace, fp);
                return default(T);
            }
        }
    }
}
