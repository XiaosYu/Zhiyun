namespace Zhiyun.Winform.Views
{
    partial class NetworkWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetworkWindow));
            NodeEditor = new STNodeEditor();
            NodePropertyGrid = new STNodePropertyGrid();
            NodeTreeView = new STNodeTreeView();
            MenuStrip = new MenuStrip();
            FToolStripMenuItem = new ToolStripMenuItem();
            NToolStripMenuItem = new ToolStripMenuItem();
            OToolStripMenuItem = new ToolStripMenuItem();
            SToolStripMenuItem = new ToolStripMenuItem();
            AToolStripMenuItem = new ToolStripMenuItem();
            XToolStripMenuItem = new ToolStripMenuItem();
            EToolStripMenuItem = new ToolStripMenuItem();
            ProjectName = new TextBox();
            MenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // NodeEditor
            // 
            NodeEditor.AllowDrop = true;
            NodeEditor.BackColor = Color.FromArgb(34, 34, 34);
            NodeEditor.Curvature = 0.3F;
            NodeEditor.Location = new Point(287, 45);
            NodeEditor.LocationBackColor = Color.FromArgb(120, 0, 0, 0);
            NodeEditor.MarkBackColor = Color.FromArgb(180, 0, 0, 0);
            NodeEditor.MarkForeColor = Color.FromArgb(180, 0, 0, 0);
            NodeEditor.MinimumSize = new Size(100, 100);
            NodeEditor.Name = "NodeEditor";
            NodeEditor.Size = new Size(1271, 774);
            NodeEditor.TabIndex = 0;
            NodeEditor.Text = "NodeEditor";
            // 
            // NodePropertyGrid
            // 
            NodePropertyGrid.BackColor = Color.FromArgb(35, 35, 35);
            NodePropertyGrid.DescriptionColor = Color.FromArgb(200, 184, 134, 11);
            NodePropertyGrid.ErrorColor = Color.FromArgb(200, 165, 42, 42);
            NodePropertyGrid.ForeColor = Color.White;
            NodePropertyGrid.ItemHoverColor = Color.FromArgb(50, 125, 125, 125);
            NodePropertyGrid.ItemValueBackColor = Color.FromArgb(80, 80, 80);
            NodePropertyGrid.Location = new Point(12, 465);
            NodePropertyGrid.MinimumSize = new Size(120, 50);
            NodePropertyGrid.Name = "NodePropertyGrid";
            NodePropertyGrid.ShowTitle = true;
            NodePropertyGrid.Size = new Size(269, 354);
            NodePropertyGrid.TabIndex = 1;
            NodePropertyGrid.Text = "NodePropertyGrid";
            NodePropertyGrid.TitleColor = Color.FromArgb(127, 0, 0, 0);
            // 
            // NodeTreeView
            // 
            NodeTreeView.AllowDrop = true;
            NodeTreeView.BackColor = Color.FromArgb(35, 35, 35);
            NodeTreeView.FolderCountColor = Color.FromArgb(40, 255, 255, 255);
            NodeTreeView.ForeColor = Color.FromArgb(220, 220, 220);
            NodeTreeView.ItemBackColor = Color.FromArgb(45, 45, 45);
            NodeTreeView.ItemHoverColor = Color.FromArgb(50, 125, 125, 125);
            NodeTreeView.Location = new Point(12, 83);
            NodeTreeView.MinimumSize = new Size(100, 60);
            NodeTreeView.Name = "NodeTreeView";
            NodeTreeView.ShowFolderCount = true;
            NodeTreeView.Size = new Size(269, 376);
            NodeTreeView.TabIndex = 2;
            NodeTreeView.Text = "NodeTreeView";
            NodeTreeView.TextBoxColor = Color.FromArgb(30, 30, 30);
            NodeTreeView.TitleColor = Color.FromArgb(60, 60, 60);
            // 
            // MenuStrip
            // 
            MenuStrip.BackColor = Color.FromArgb(34, 34, 34);
            MenuStrip.ImageScalingSize = new Size(20, 20);
            MenuStrip.Items.AddRange(new ToolStripItem[] { FToolStripMenuItem, EToolStripMenuItem });
            MenuStrip.Location = new Point(0, 0);
            MenuStrip.Name = "MenuStrip";
            MenuStrip.Size = new Size(1570, 28);
            MenuStrip.TabIndex = 3;
            MenuStrip.Text = "MenuStrip";
            // 
            // FToolStripMenuItem
            // 
            FToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { NToolStripMenuItem, OToolStripMenuItem, SToolStripMenuItem, AToolStripMenuItem, XToolStripMenuItem });
            FToolStripMenuItem.ForeColor = SystemColors.ButtonHighlight;
            FToolStripMenuItem.Name = "FToolStripMenuItem";
            FToolStripMenuItem.Size = new Size(71, 24);
            FToolStripMenuItem.Text = "文件(&F)";
            // 
            // NToolStripMenuItem
            // 
            NToolStripMenuItem.BackColor = Color.FromArgb(34, 34, 34);
            NToolStripMenuItem.ForeColor = SystemColors.ButtonHighlight;
            NToolStripMenuItem.Image = (Image)resources.GetObject("NToolStripMenuItem.Image");
            NToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            NToolStripMenuItem.Name = "NToolStripMenuItem";
            NToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            NToolStripMenuItem.Size = new Size(202, 26);
            NToolStripMenuItem.Text = "新建(&N)";
            NToolStripMenuItem.Click += NToolStripMenuItem_Click;
            // 
            // OToolStripMenuItem
            // 
            OToolStripMenuItem.BackColor = Color.FromArgb(34, 34, 34);
            OToolStripMenuItem.ForeColor = SystemColors.ButtonHighlight;
            OToolStripMenuItem.Image = (Image)resources.GetObject("OToolStripMenuItem.Image");
            OToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            OToolStripMenuItem.Name = "OToolStripMenuItem";
            OToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            OToolStripMenuItem.Size = new Size(202, 26);
            OToolStripMenuItem.Text = "打开(&O)";
            OToolStripMenuItem.Click += OToolStripMenuItem_Click;
            // 
            // SToolStripMenuItem
            // 
            SToolStripMenuItem.BackColor = Color.FromArgb(34, 34, 34);
            SToolStripMenuItem.ForeColor = SystemColors.ButtonHighlight;
            SToolStripMenuItem.Image = (Image)resources.GetObject("SToolStripMenuItem.Image");
            SToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            SToolStripMenuItem.Name = "SToolStripMenuItem";
            SToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            SToolStripMenuItem.Size = new Size(202, 26);
            SToolStripMenuItem.Text = "保存(&S)";
            SToolStripMenuItem.Click += SToolStripMenuItem_Click;
            // 
            // AToolStripMenuItem
            // 
            AToolStripMenuItem.BackColor = Color.FromArgb(34, 34, 34);
            AToolStripMenuItem.ForeColor = SystemColors.ButtonHighlight;
            AToolStripMenuItem.Name = "AToolStripMenuItem";
            AToolStripMenuItem.Size = new Size(202, 26);
            AToolStripMenuItem.Text = "另存为(&A)";
            AToolStripMenuItem.Click += AToolStripMenuItem_Click;
            // 
            // XToolStripMenuItem
            // 
            XToolStripMenuItem.BackColor = Color.FromArgb(34, 34, 34);
            XToolStripMenuItem.ForeColor = SystemColors.ButtonHighlight;
            XToolStripMenuItem.Name = "XToolStripMenuItem";
            XToolStripMenuItem.Size = new Size(202, 26);
            XToolStripMenuItem.Text = "退出(&X)";
            // 
            // EToolStripMenuItem
            // 
            EToolStripMenuItem.ForeColor = SystemColors.ButtonHighlight;
            EToolStripMenuItem.Name = "EToolStripMenuItem";
            EToolStripMenuItem.Size = new Size(71, 24);
            EToolStripMenuItem.Text = "导出(&E)";
            EToolStripMenuItem.Click += EToolStripMenuItem_Click;
            // 
            // ProjectName
            // 
            ProjectName.BackColor = Color.FromArgb(34, 34, 34);
            ProjectName.BorderStyle = BorderStyle.None;
            ProjectName.ForeColor = SystemColors.ControlLightLight;
            ProjectName.Location = new Point(12, 45);
            ProjectName.Name = "ProjectName";
            ProjectName.Size = new Size(269, 20);
            ProjectName.TabIndex = 5;
            ProjectName.Text = "未命名项目";
            // 
            // NetworkWindow
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 34, 34);
            ClientSize = new Size(1570, 831);
            Controls.Add(ProjectName);
            Controls.Add(NodeTreeView);
            Controls.Add(NodePropertyGrid);
            Controls.Add(NodeEditor);
            Controls.Add(MenuStrip);
            MainMenuStrip = MenuStrip;
            Name = "NetworkWindow";
            Text = "网络结构编辑器";
            Load += MainWindow_Load;
            MenuStrip.ResumeLayout(false);
            MenuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private STNodeEditor NodeEditor;
        private STNodePropertyGrid NodePropertyGrid;
        private STNodeTreeView NodeTreeView;
        private MenuStrip MenuStrip;
        private ToolStripMenuItem FToolStripMenuItem;
        private ToolStripMenuItem NToolStripMenuItem;
        private ToolStripMenuItem OToolStripMenuItem;
        private ToolStripMenuItem SToolStripMenuItem;
        private ToolStripMenuItem AToolStripMenuItem;
        private ToolStripMenuItem XToolStripMenuItem;
        private TextBox ProjectName;
        private ToolStripMenuItem EToolStripMenuItem;
    }
}