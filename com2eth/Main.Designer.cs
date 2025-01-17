namespace com2eth
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            app_menubar = new MenuStrip();
            文件ToolStripMenuItem1 = new ToolStripMenuItem();
            ToolStripMenuItem_import = new ToolStripMenuItem();
            ToolStripMenuItem_export = new ToolStripMenuItem();
            pn_left = new Panel();
            bundles_tree = new TreeView();
            panel4 = new Panel();
            app_msg = new RichTextBox();
            app_splitContainer = new SplitContainer();
            m_spc_center = new SplitContainer();
            m_pn_center = new Panel();
            pn_top = new FlowLayoutPanel();
            tools_btn_new = new Button();
            tools_btn_del = new Button();
            app_menubar.SuspendLayout();
            pn_left.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)app_splitContainer).BeginInit();
            app_splitContainer.Panel1.SuspendLayout();
            app_splitContainer.Panel2.SuspendLayout();
            app_splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_spc_center).BeginInit();
            m_spc_center.Panel1.SuspendLayout();
            m_spc_center.Panel2.SuspendLayout();
            m_spc_center.SuspendLayout();
            pn_top.SuspendLayout();
            SuspendLayout();
            // 
            // app_menubar
            // 
            app_menubar.Font = new Font("Microsoft YaHei UI", 12F);
            app_menubar.GripStyle = ToolStripGripStyle.Visible;
            app_menubar.Items.AddRange(new ToolStripItem[] { 文件ToolStripMenuItem1 });
            app_menubar.Location = new Point(0, 0);
            app_menubar.Name = "app_menubar";
            app_menubar.Padding = new Padding(9, 2, 0, 2);
            app_menubar.Size = new Size(1008, 29);
            app_menubar.TabIndex = 1;
            app_menubar.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem1
            // 
            文件ToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { ToolStripMenuItem_import, ToolStripMenuItem_export });
            文件ToolStripMenuItem1.Name = "文件ToolStripMenuItem1";
            文件ToolStripMenuItem1.Size = new Size(54, 25);
            文件ToolStripMenuItem1.Text = "文件";
            // 
            // ToolStripMenuItem_import
            // 
            ToolStripMenuItem_import.Name = "ToolStripMenuItem_import";
            ToolStripMenuItem_import.Size = new Size(112, 26);
            ToolStripMenuItem_import.Text = "导入";
            ToolStripMenuItem_import.Click += ToolStripMenuItem_import_Click;
            // 
            // ToolStripMenuItem_export
            // 
            ToolStripMenuItem_export.Name = "ToolStripMenuItem_export";
            ToolStripMenuItem_export.Size = new Size(112, 26);
            ToolStripMenuItem_export.Text = "导出";
            ToolStripMenuItem_export.Click += ToolStripMenuItem_export_Click;
            // 
            // pn_left
            // 
            pn_left.Controls.Add(bundles_tree);
            pn_left.Dock = DockStyle.Fill;
            pn_left.Location = new Point(3, 3);
            pn_left.Margin = new Padding(4);
            pn_left.Name = "pn_left";
            pn_left.Size = new Size(170, 524);
            pn_left.TabIndex = 2;
            // 
            // bundles_tree
            // 
            bundles_tree.Dock = DockStyle.Fill;
            bundles_tree.Location = new Point(0, 0);
            bundles_tree.Name = "bundles_tree";
            bundles_tree.Size = new Size(170, 524);
            bundles_tree.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.Location = new Point(107, 548);
            panel4.Margin = new Padding(4);
            panel4.Name = "panel4";
            panel4.Size = new Size(234, 72);
            panel4.TabIndex = 2;
            // 
            // app_msg
            // 
            app_msg.Dock = DockStyle.Fill;
            app_msg.Location = new Point(3, 3);
            app_msg.Name = "app_msg";
            app_msg.Size = new Size(816, 173);
            app_msg.TabIndex = 0;
            app_msg.Text = "";
            app_msg.TextChanged += app_msg_TextChanged;
            // 
            // app_splitContainer
            // 
            app_splitContainer.Dock = DockStyle.Fill;
            app_splitContainer.Location = new Point(0, 69);
            app_splitContainer.Name = "app_splitContainer";
            // 
            // app_splitContainer.Panel1
            // 
            app_splitContainer.Panel1.Controls.Add(pn_left);
            app_splitContainer.Panel1.Padding = new Padding(3);
            // 
            // app_splitContainer.Panel2
            // 
            app_splitContainer.Panel2.Controls.Add(m_spc_center);
            app_splitContainer.Panel2.Padding = new Padding(3);
            app_splitContainer.Size = new Size(1008, 530);
            app_splitContainer.SplitterDistance = 176;
            app_splitContainer.TabIndex = 3;
            // 
            // m_spc_center
            // 
            m_spc_center.Dock = DockStyle.Fill;
            m_spc_center.Location = new Point(3, 3);
            m_spc_center.Name = "m_spc_center";
            m_spc_center.Orientation = Orientation.Horizontal;
            // 
            // m_spc_center.Panel1
            // 
            m_spc_center.Panel1.Controls.Add(m_pn_center);
            m_spc_center.Panel1.Padding = new Padding(3);
            // 
            // m_spc_center.Panel2
            // 
            m_spc_center.Panel2.Controls.Add(app_msg);
            m_spc_center.Panel2.Padding = new Padding(3);
            m_spc_center.Size = new Size(822, 524);
            m_spc_center.SplitterDistance = 341;
            m_spc_center.TabIndex = 3;
            // 
            // m_pn_center
            // 
            m_pn_center.Dock = DockStyle.Fill;
            m_pn_center.Location = new Point(3, 3);
            m_pn_center.Name = "m_pn_center";
            m_pn_center.Size = new Size(816, 335);
            m_pn_center.TabIndex = 0;
            // 
            // pn_top
            // 
            pn_top.BorderStyle = BorderStyle.FixedSingle;
            pn_top.Controls.Add(tools_btn_new);
            pn_top.Controls.Add(tools_btn_del);
            pn_top.Dock = DockStyle.Top;
            pn_top.Location = new Point(0, 29);
            pn_top.Name = "pn_top";
            pn_top.Padding = new Padding(10, 3, 3, 3);
            pn_top.Size = new Size(1008, 40);
            pn_top.TabIndex = 4;
            // 
            // tools_btn_new
            // 
            tools_btn_new.Location = new Point(18, 6);
            tools_btn_new.Margin = new Padding(8, 3, 3, 3);
            tools_btn_new.Name = "tools_btn_new";
            tools_btn_new.Size = new Size(60, 30);
            tools_btn_new.TabIndex = 1;
            tools_btn_new.Text = "新建";
            tools_btn_new.UseVisualStyleBackColor = true;
            tools_btn_new.Click += tools_btn_new_Click;
            // 
            // tools_btn_del
            // 
            tools_btn_del.Location = new Point(84, 6);
            tools_btn_del.Name = "tools_btn_del";
            tools_btn_del.Size = new Size(60, 30);
            tools_btn_del.TabIndex = 0;
            tools_btn_del.Text = "删除";
            tools_btn_del.UseVisualStyleBackColor = true;
            tools_btn_del.Click += tools_btn_del_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(10F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 599);
            Controls.Add(app_splitContainer);
            Controls.Add(pn_top);
            Controls.Add(panel4);
            Controls.Add(app_menubar);
            Font = new Font("Microsoft YaHei UI", 12F);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = app_menubar;
            Margin = new Padding(4);
            Name = "Main";
            FormClosing += Main_FormClosing;
            Load += Main_Load;
            app_menubar.ResumeLayout(false);
            app_menubar.PerformLayout();
            pn_left.ResumeLayout(false);
            app_splitContainer.Panel1.ResumeLayout(false);
            app_splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)app_splitContainer).EndInit();
            app_splitContainer.ResumeLayout(false);
            m_spc_center.Panel1.ResumeLayout(false);
            m_spc_center.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)m_spc_center).EndInit();
            m_spc_center.ResumeLayout(false);
            pn_top.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip app_menubar;
        private ToolStripMenuItem 文件ToolStripMenuItem1;
        private ToolStripMenuItem ToolStripMenuItem_import;
        private ToolStripMenuItem ToolStripMenuItem_export;
        private Panel pn_left;
        private Panel panel4;
        private SplitContainer app_splitContainer;
        private RichTextBox app_msg;
        private TreeView bundles_tree;
        private FlowLayoutPanel pn_top;
        private Button tools_btn_del;
        private Button tools_btn_new;
        private SplitContainer m_spc_center;
        private Panel m_pn_center;
    }
}