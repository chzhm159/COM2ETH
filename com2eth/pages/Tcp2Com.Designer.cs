﻿namespace com2eth.pages {
    partial class Tcp2Com {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            splitContainer1 = new SplitContainer();
            pn_A = new Panel();
            a_btn_save = new Button();
            a_tb_port = new TextBox();
            a_tb_ip = new TextBox();
            a_lb_msg = new Label();
            a_lb_port = new Label();
            a_lb_ip = new Label();
            a_lb_name = new Label();
            pn_B = new Panel();
            button1 = new Button();
            b_tb_baudrate = new TextBox();
            b_tb_com = new TextBox();
            b_lb_msg = new Label();
            b_lb_baudrate = new Label();
            b_lb_com = new Label();
            label4 = new Label();
            label2 = new Label();
            b_tb_parity = new TextBox();
            label3 = new Label();
            b_tb_databit = new TextBox();
            label5 = new Label();
            b_tb_stopbit = new TextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            pn_A.SuspendLayout();
            pn_B.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Margin = new Padding(4);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(pn_A);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(pn_B);
            splitContainer1.Size = new Size(820, 400);
            splitContainer1.SplitterDistance = 410;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 0;
            // 
            // pn_A
            // 
            pn_A.Controls.Add(a_btn_save);
            pn_A.Controls.Add(a_tb_port);
            pn_A.Controls.Add(a_tb_ip);
            pn_A.Controls.Add(a_lb_msg);
            pn_A.Controls.Add(a_lb_port);
            pn_A.Controls.Add(a_lb_ip);
            pn_A.Controls.Add(a_lb_name);
            pn_A.Dock = DockStyle.Fill;
            pn_A.Location = new Point(0, 0);
            pn_A.Margin = new Padding(4);
            pn_A.Name = "pn_A";
            pn_A.Padding = new Padding(4);
            pn_A.Size = new Size(410, 400);
            pn_A.TabIndex = 0;
            // 
            // a_btn_save
            // 
            a_btn_save.Location = new Point(203, 186);
            a_btn_save.Name = "a_btn_save";
            a_btn_save.Size = new Size(75, 30);
            a_btn_save.TabIndex = 3;
            a_btn_save.Text = "保存";
            a_btn_save.UseVisualStyleBackColor = true;
            // 
            // a_tb_port
            // 
            a_tb_port.Location = new Point(107, 105);
            a_tb_port.Name = "a_tb_port";
            a_tb_port.Size = new Size(171, 28);
            a_tb_port.TabIndex = 2;
            // 
            // a_tb_ip
            // 
            a_tb_ip.Location = new Point(107, 57);
            a_tb_ip.Name = "a_tb_ip";
            a_tb_ip.Size = new Size(171, 28);
            a_tb_ip.TabIndex = 2;
            // 
            // a_lb_msg
            // 
            a_lb_msg.Location = new Point(18, 208);
            a_lb_msg.Name = "a_lb_msg";
            a_lb_msg.Size = new Size(334, 30);
            a_lb_msg.TabIndex = 1;
            a_lb_msg.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // a_lb_port
            // 
            a_lb_port.Location = new Point(28, 105);
            a_lb_port.Name = "a_lb_port";
            a_lb_port.Size = new Size(73, 30);
            a_lb_port.TabIndex = 1;
            a_lb_port.Text = "端口:";
            a_lb_port.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // a_lb_ip
            // 
            a_lb_ip.Location = new Point(28, 55);
            a_lb_ip.Name = "a_lb_ip";
            a_lb_ip.Size = new Size(73, 30);
            a_lb_ip.TabIndex = 1;
            a_lb_ip.Text = "IP地址:";
            a_lb_ip.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // a_lb_name
            // 
            a_lb_name.Dock = DockStyle.Top;
            a_lb_name.Location = new Point(4, 4);
            a_lb_name.Margin = new Padding(4, 0, 4, 0);
            a_lb_name.Name = "a_lb_name";
            a_lb_name.Size = new Size(402, 30);
            a_lb_name.TabIndex = 0;
            a_lb_name.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pn_B
            // 
            pn_B.Controls.Add(button1);
            pn_B.Controls.Add(b_tb_stopbit);
            pn_B.Controls.Add(b_tb_databit);
            pn_B.Controls.Add(b_tb_parity);
            pn_B.Controls.Add(label5);
            pn_B.Controls.Add(b_tb_baudrate);
            pn_B.Controls.Add(label3);
            pn_B.Controls.Add(b_tb_com);
            pn_B.Controls.Add(label2);
            pn_B.Controls.Add(b_lb_msg);
            pn_B.Controls.Add(b_lb_baudrate);
            pn_B.Controls.Add(b_lb_com);
            pn_B.Controls.Add(label4);
            pn_B.Dock = DockStyle.Fill;
            pn_B.Location = new Point(0, 0);
            pn_B.Margin = new Padding(4);
            pn_B.Name = "pn_B";
            pn_B.Padding = new Padding(4);
            pn_B.Size = new Size(404, 400);
            pn_B.TabIndex = 0;
            pn_B.Paint += pn_B_Paint;
            // 
            // button1
            // 
            button1.Location = new Point(209, 296);
            button1.Name = "button1";
            button1.Size = new Size(75, 30);
            button1.TabIndex = 10;
            button1.Text = "保存";
            button1.UseVisualStyleBackColor = true;
            // 
            // b_tb_baudrate
            // 
            b_tb_baudrate.Location = new Point(177, 107);
            b_tb_baudrate.Name = "b_tb_baudrate";
            b_tb_baudrate.Size = new Size(107, 28);
            b_tb_baudrate.TabIndex = 8;
            // 
            // b_tb_com
            // 
            b_tb_com.Location = new Point(177, 55);
            b_tb_com.Name = "b_tb_com";
            b_tb_com.Size = new Size(107, 28);
            b_tb_com.TabIndex = 9;
            // 
            // b_lb_msg
            // 
            b_lb_msg.Location = new Point(17, 338);
            b_lb_msg.Name = "b_lb_msg";
            b_lb_msg.Size = new Size(334, 30);
            b_lb_msg.TabIndex = 5;
            b_lb_msg.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // b_lb_baudrate
            // 
            b_lb_baudrate.Location = new Point(71, 107);
            b_lb_baudrate.Name = "b_lb_baudrate";
            b_lb_baudrate.Size = new Size(100, 30);
            b_lb_baudrate.TabIndex = 6;
            b_lb_baudrate.Text = "波特率:";
            b_lb_baudrate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // b_lb_com
            // 
            b_lb_com.Location = new Point(71, 55);
            b_lb_com.Name = "b_lb_com";
            b_lb_com.Size = new Size(100, 30);
            b_lb_com.TabIndex = 7;
            b_lb_com.Text = "COM口:";
            b_lb_com.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.Dock = DockStyle.Top;
            label4.Location = new Point(4, 4);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(396, 30);
            label4.TabIndex = 4;
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Location = new Point(71, 153);
            label2.Name = "label2";
            label2.Size = new Size(100, 30);
            label2.TabIndex = 6;
            label2.Text = "奇偶校验:";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // b_tb_parity
            // 
            b_tb_parity.Location = new Point(177, 153);
            b_tb_parity.Name = "b_tb_parity";
            b_tb_parity.Size = new Size(107, 28);
            b_tb_parity.TabIndex = 8;
            // 
            // label3
            // 
            label3.Location = new Point(71, 201);
            label3.Name = "label3";
            label3.Size = new Size(100, 30);
            label3.TabIndex = 6;
            label3.Text = "数据位:";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // b_tb_databit
            // 
            b_tb_databit.Location = new Point(177, 201);
            b_tb_databit.Name = "b_tb_databit";
            b_tb_databit.Size = new Size(107, 28);
            b_tb_databit.TabIndex = 8;
            // 
            // label5
            // 
            label5.Location = new Point(71, 245);
            label5.Name = "label5";
            label5.Size = new Size(100, 30);
            label5.TabIndex = 6;
            label5.Text = "停止位:";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // b_tb_stopbit
            // 
            b_tb_stopbit.Location = new Point(177, 247);
            b_tb_stopbit.Name = "b_tb_stopbit";
            b_tb_stopbit.Size = new Size(107, 28);
            b_tb_stopbit.TabIndex = 8;
            // 
            // Tcp2Com
            // 
            AutoScaleDimensions = new SizeF(10F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Font = new Font("Microsoft YaHei UI", 12F);
            Margin = new Padding(4);
            Name = "Tcp2Com";
            Size = new Size(820, 400);
            Resize += Tcp2Com_Resize;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            pn_A.ResumeLayout(false);
            pn_A.PerformLayout();
            pn_B.ResumeLayout(false);
            pn_B.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Panel pn_A;
        private Panel pn_B;
        private Label a_lb_name;
        private Label a_lb_ip;
        private Label a_lb_port;
        private TextBox a_tb_ip;
        private TextBox a_tb_port;
        private Button a_btn_save;
        private Label a_lb_msg;
        private Button button1;
        private TextBox b_tb_baudrate;
        private TextBox b_tb_com;
        private Label b_lb_msg;
        private Label b_lb_baudrate;
        private Label b_lb_com;
        private Label label4;
        private TextBox b_tb_databit;
        private TextBox b_tb_parity;
        private Label label3;
        private Label label2;
        private TextBox b_tb_stopbit;
        private Label label5;
    }
}
