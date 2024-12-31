using com2eth.connector;
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

namespace com2eth.pages {
    public partial class Tcp2Com : UserControl {
        public Tcp2Com() {
            InitializeComponent();
        }
        internal void Config(EndpointEntity eA, EndpointEntity eB) {
            if (this.InvokeRequired) {
                this.BeginInvoke(ConfigDelegate,eA,eB);
            } else {
                ConfigDelegate(eA,eB);
            }
        }
        private void ConfigDelegate(EndpointEntity eA, EndpointEntity eB) {
            this.a_tb_ip.Text = eA.ip;
            this.a_tb_port.Text = eA.port.ToString();
            
            this.b_tb_com.Text = eB.com;
            this.b_tb_baudrate.Text = eB.baudrate.ToString();
            this.b_tb_databit.Text = eB.databits.ToString();
            this.b_tb_parity.Text = eB.parity;
            this.b_tb_stopbit.Text = eB.stopbits;
        }
        private void Tcp2Com_Resize(object sender, EventArgs e) {
            this.splitContainer1.SplitterDistance = this.splitContainer1.Width / 2;
        }

        private void pn_B_Paint(object sender, PaintEventArgs e) {

        }
    }
}
