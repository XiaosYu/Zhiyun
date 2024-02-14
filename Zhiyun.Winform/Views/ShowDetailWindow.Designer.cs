namespace Zhiyun.Winform.Views
{
    partial class ShowDetailWindow
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
            NodeEditor = new STNodeEditor();
            NodePropertyGrid = new STNodePropertyGrid();
            SuspendLayout();
            // 
            // NodeEditor
            // 
            NodeEditor.AllowDrop = true;
            NodeEditor.BackColor = Color.FromArgb(34, 34, 34);
            NodeEditor.Curvature = 0.3F;
            NodeEditor.Location = new Point(230, 12);
            NodeEditor.LocationBackColor = Color.FromArgb(120, 0, 0, 0);
            NodeEditor.MarkBackColor = Color.FromArgb(180, 0, 0, 0);
            NodeEditor.MarkForeColor = Color.FromArgb(180, 0, 0, 0);
            NodeEditor.MinimumSize = new Size(100, 100);
            NodeEditor.Name = "NodeEditor";
            NodeEditor.Size = new Size(558, 426);
            NodeEditor.TabIndex = 0;
            NodeEditor.Text = "stNodeEditor1";
            // 
            // NodePropertyGrid
            // 
            NodePropertyGrid.BackColor = Color.FromArgb(35, 35, 35);
            NodePropertyGrid.DescriptionColor = Color.FromArgb(200, 184, 134, 11);
            NodePropertyGrid.ErrorColor = Color.FromArgb(200, 165, 42, 42);
            NodePropertyGrid.ForeColor = Color.White;
            NodePropertyGrid.ItemHoverColor = Color.FromArgb(50, 125, 125, 125);
            NodePropertyGrid.ItemValueBackColor = Color.FromArgb(80, 80, 80);
            NodePropertyGrid.Location = new Point(12, 12);
            NodePropertyGrid.MinimumSize = new Size(120, 50);
            NodePropertyGrid.Name = "NodePropertyGrid";
            NodePropertyGrid.ShowTitle = true;
            NodePropertyGrid.Size = new Size(212, 426);
            NodePropertyGrid.TabIndex = 1;
            NodePropertyGrid.Text = "节点属性";
            NodePropertyGrid.TitleColor = Color.FromArgb(127, 0, 0, 0);
            // 
            // ShowDetailWindow
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 34, 34);
            ClientSize = new Size(800, 450);
            Controls.Add(NodePropertyGrid);
            Controls.Add(NodeEditor);
            Name = "ShowDetailWindow";
            Text = "模块详细";
            Load += ShowDetailWindow_Load;
            ResumeLayout(false);
        }

        #endregion

        private STNodeEditor NodeEditor;
        private STNodePropertyGrid NodePropertyGrid;
    }
}