using com2eth.connector;
using com2eth.pages;
using com2eth.serialport;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace com2eth
{
    public partial class Main : Form {
        private static readonly ILog log = LogManager.GetLogger(typeof(Main));

        Tcp2Com tcp2com;
        int maxLinds = 1000;
        public Main() {
            InitializeComponent();
            MainService msi = MainService.Inst;
            msi.SetMain(this);

            AppCfg appcfg = AppCfg.Inst;

            tcp2com = new Tcp2Com();
            // RAL 5005. 0, 83, 135, #005387            
        }

        internal void OnMsg(string msg) {
            if (this.InvokeRequired) {
                this.BeginInvoke(ShowMsg, msg);
            } else {
                ShowMsg(msg);
            }
        }

        private void ShowMsg(string msg) {
            string text = string.Format("[{0}]: {1} \r\n", AppCfg.Now(), msg);
            app_msg.AppendText(text);
            // 让文本框获取焦点  
            // this.app_console.Focus();
            //设置光标的位置到文本尾  
            app_msg.Select(app_msg.TextLength - 1, 0);
            //滚动到控件光标处  
            app_msg.ScrollToCaret();
        }
        private void app_msg_TextChanged(object sender, EventArgs e) {
            if (app_msg.Lines.Length < maxLinds) {
                return;
            }
            // 直接减少 1/2
            int start = app_msg.Lines.Length / 2;//  - maxLinds;
            int length = app_msg.Lines.Length - start;
            string[] lines = app_msg.Lines;
            string[] toshow = new string[length];
            Array.Copy(lines, start, toshow, 0, length);
            // Array.Resize(ref lines, maxLinds);
            app_msg.Lines = toshow;
            //让文本框获取焦点  
            app_msg.Focus();
            //设置光标的位置到文本尾  
            app_msg.Select(app_msg.TextLength - 1, 0);
            //滚动到控件光标处  
            app_msg.ScrollToCaret();
        }

        private void ToolStripMenuItem_import_Click(object sender, EventArgs e) {

        }

        private void ToolStripMenuItem_export_Click(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {
            OnMsg("test");
            AppCfg.GetString("test");

        }

        private void Main_Load(object sender, EventArgs e) {
            MainService msi = MainService.Inst;
            msi.Load();
        }

        private void tools_btn_new_Click(object sender, EventArgs e) {

        }

        internal void UpdateBundles(Bundles bundles) {
            if (this.InvokeRequired) {
                this.BeginInvoke(UpdateBundlesDelegate, bundles);
            } else {
                UpdateBundlesDelegate(bundles);
            }
        }

        private void UpdateBundlesDelegate(Bundles bindMap) {
            if (bindMap == null || bindMap.bundles == null) {
                log.Error("未能加载Bind信息!");
                OnMsg("未能加载有效配置信息");
                return;
            }

            bindMap.bundles.ForEach(bind => {
                TreeNode node = new TreeNode(bind.name);
                node.Tag = bind;
                bundles_tree.Nodes.Add(node);
            });
            if (bundles_tree.Nodes.Count > 0) {
                TreeNode node = bundles_tree.Nodes[0];
                bundles_tree.SelectedNode = node;
                EndpointMapper? bm = node.Tag as EndpointMapper;

                tcp2com.Dock = DockStyle.Fill;
                tcp2com.Config(bm);
                m_pn_center.Controls.Clear();
                m_pn_center.Controls.Add(tcp2com);
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult ret = MessageBox.Show("是否关闭程序?", "关闭", MessageBoxButtons.YesNo);
            if (ret != DialogResult.Yes) {
                e.Cancel = true;
                return;
            }
        }

        private void tools_btn_del_Click(object sender, EventArgs e) {

        }
    }
}
