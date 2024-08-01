using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

    }
}
