using com2eth.connector;
using com2eth.serialport;
using log4net;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com2eth {
    internal class MainService {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainService));
        private Main main;
        public string ConfigDir = "config";
        string BindsName = "binds.json";
        public MainService(Main main) {
            this.main = main;
        }

        internal void Load() {
            LoadBinds();
        }
        public void LoadBinds() {            
            string bingFP = Path.Combine(ConfigDir, BindsName);
            // 加载配置文件,判断 bind 类型.
            BindUtils utils = BindUtils.Inst;
            Bundles bindingMap = utils.Load(bingFP);
            
            main.UpdateBundles(bindingMap);
            
        }
    }
}
