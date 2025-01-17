using com2eth.connector;
using com2eth.serialport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace com2eth.pages {
    public partial class Tcp2Com : UserControl {
        EndpointMapper endpointMapper;

        public Tcp2Com() {
            InitializeComponent();
            InitComComboxList();
        }
        private void InitComComboxList() {
            b_cb_parity.Items.Clear();
            List<string> paritys = AppCfg.GetParity();
            paritys.ForEach(p => b_cb_parity.Items.Add(p));
            b_cb_parity.SelectedIndex = 0;

            b_cb_stopbit.Items.Clear();
            List<string> stopbits = AppCfg.GetStopbit();
            stopbits.ForEach(s => b_cb_stopbit.Items.Add(s));            
            b_cb_stopbit.SelectedIndex = 0;
        }

        internal void Config(EndpointMapper em) {
            this.endpointMapper = em;
            if (this.InvokeRequired) {
                this.BeginInvoke(ConfigDelegate, em.endpoint_a, em.endpoint_b);
            } else {
                ConfigDelegate(em.endpoint_a, em.endpoint_b);
            }
        }
        private void ConfigDelegate(EndpointEntity eA, EndpointEntity eB) {
            this.a_lb_name.Text = eA.name;
            this.a_tb_ip.Text = eA.ip;
            this.a_tb_port.Text = eA.port.ToString();

            this.b_lb_name.Text = eB.name;
            this.b_tb_com.Text = eB.com;
            this.b_tb_baudrate.Text = eB.baudrate.ToString();
            this.b_tb_databit.Text = eB.databits.ToString();
            
            
            this.b_tb_parity.Text = eB.parity;
            this.b_tb_stopbit.Text = eB.stopbits;
            int sbIdx = 0;
            
            for(int si = 0; si < b_cb_stopbit.Items.Count; si++) {
                string sbitem = b_cb_stopbit.Items[si].ToString();
                if (string.Equals(sbitem, eB.stopbits, StringComparison.OrdinalIgnoreCase)) {
                    sbIdx = si;
                    break;
                }
            }
            b_cb_stopbit.SelectedIndex = sbIdx;
            int pIdx = 0;
            for (int i = 0; i < b_cb_parity.Items.Count; i++) {
                string pitem = b_cb_parity.Items[i].ToString();
                if (string.Equals(pitem, eB.parity, StringComparison.OrdinalIgnoreCase)) {
                    pIdx = i;
                    break;
                }
            }
            b_cb_parity.SelectedIndex = pIdx;
        }
        private void Tcp2Com_Resize(object sender, EventArgs e) {
            this.splitContainer1.SplitterDistance = this.splitContainer1.Width / 2;
        }


        private void a_btn_save_Click(object sender, EventArgs e) {
            string tcpIP = this.a_tb_ip.Text;

            try {
                IPAddress ip = IPAddress.Parse(tcpIP);
            } catch (Exception ex) {
                MessageBox.Show("IP地址格式错误!");
                return;
            }
            string tcpPorStr = this.a_tb_port.Text;
            int port = 8888;
            try {
                port = int.Parse(tcpPorStr);
                if (port < 0 || port > 65535) {
                    throw new Exception();
                }
            } catch (Exception ex) {
                MessageBox.Show("端口号必须为[0-65535]的数字!");
                return;
            }
            this.endpointMapper.endpoint_a.ip = tcpIP;
            this.endpointMapper.endpoint_a.port = port;
            MainService msi = MainService.Inst;
            bool suc = msi.Save(this.endpointMapper);
            msi.ShowMsg("{0}保存成功!", this.endpointMapper.name);
        }

        private void button1_Click(object sender, EventArgs e) {

            string comTxt = this.b_tb_com.Text;
            string parityTxt = this.b_cb_parity.SelectedItem.ToString() ;
            
            string baudrateTxt = this.b_tb_baudrate.Text;
            string databitTxt =this.b_tb_databit.Text ;

            string stopbitTxt = this.b_cb_stopbit.SelectedItem.ToString(); ;

            bool comEmpty = string.IsNullOrEmpty(comTxt);
            bool brEmpty = string.IsNullOrEmpty(baudrateTxt);
            bool dbEmpty = string.IsNullOrEmpty(databitTxt);
            bool pEmpty = string.IsNullOrEmpty(parityTxt);
            bool sbEmpty = string.IsNullOrEmpty(stopbitTxt);
            if(comEmpty || brEmpty || dbEmpty || pEmpty || sbEmpty) {
                MessageBox.Show("串口参数不能有空项!");
                return;
            }
            int baudrate = 9600;
            int databit = 1;
            
            try {
                baudrate = int.Parse(baudrateTxt);
                databit = int.Parse(databitTxt);
                float.Parse(stopbitTxt);
            }catch {
                MessageBox.Show("串口参数,'波特率','数据位'必须是数字类型.'停止位'有效数值为[0,1,1.5,2]!");
                return;
            }

            this.endpointMapper.endpoint_b.com = comTxt;
            this.endpointMapper.endpoint_b.baudrate = baudrate;
            this.endpointMapper.endpoint_b.parity= parityTxt;
            this.endpointMapper.endpoint_b.databits= databit;
            this.endpointMapper.endpoint_b.stopbits= stopbitTxt;
            MainService msi = MainService.Inst;
            bool suc = msi.Save(this.endpointMapper);
            msi.ShowMsg("{0}保存成功!", this.endpointMapper.name);
        }
    }
}
