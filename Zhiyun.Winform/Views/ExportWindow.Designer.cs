namespace Zhiyun.Winform.Views
{
    partial class ExportWindow
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
            VerfiyMessageList = new ListBox();
            ExportPanel = new Panel();
            ExportButton = new Button();
            ModuleBox = new ComboBox();
            label2 = new Label();
            textBox1 = new TextBox();
            label1 = new Label();
            PropertyGridView = new DataGridView();
            panel1 = new Panel();
            ExportPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PropertyGridView).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // VerfiyMessageList
            // 
            VerfiyMessageList.BackColor = Color.FromArgb(34, 34, 34);
            VerfiyMessageList.BorderStyle = BorderStyle.FixedSingle;
            VerfiyMessageList.ForeColor = SystemColors.ControlLightLight;
            VerfiyMessageList.FormattingEnabled = true;
            VerfiyMessageList.Location = new Point(12, 12);
            VerfiyMessageList.Name = "VerfiyMessageList";
            VerfiyMessageList.Size = new Size(244, 662);
            VerfiyMessageList.TabIndex = 0;
            // 
            // ExportPanel
            // 
            ExportPanel.Controls.Add(ExportButton);
            ExportPanel.Controls.Add(ModuleBox);
            ExportPanel.Controls.Add(label2);
            ExportPanel.Controls.Add(textBox1);
            ExportPanel.Controls.Add(label1);
            ExportPanel.Location = new Point(265, 12);
            ExportPanel.Name = "ExportPanel";
            ExportPanel.Size = new Size(878, 124);
            ExportPanel.TabIndex = 1;
            // 
            // ExportButton
            // 
            ExportButton.Location = new Point(297, 74);
            ExportButton.Name = "ExportButton";
            ExportButton.Size = new Size(136, 39);
            ExportButton.TabIndex = 4;
            ExportButton.Text = "导出";
            ExportButton.UseVisualStyleBackColor = true;
            ExportButton.Click += ExportButton_Click;
            // 
            // ModuleBox
            // 
            ModuleBox.BackColor = Color.FromArgb(34, 34, 34);
            ModuleBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ModuleBox.ForeColor = SystemColors.ControlLightLight;
            ModuleBox.FormattingEnabled = true;
            ModuleBox.Items.AddRange(new object[] { "Net", "Module" });
            ModuleBox.Location = new Point(78, 80);
            ModuleBox.Name = "ModuleBox";
            ModuleBox.Size = new Size(201, 28);
            ModuleBox.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(3, 87);
            label2.Name = "label2";
            label2.Size = new Size(69, 20);
            label2.TabIndex = 2;
            label2.Text = "模块类型";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(34, 34, 34);
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.ForeColor = SystemColors.ControlLightLight;
            textBox1.Location = new Point(78, 30);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(201, 27);
            textBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(3, 32);
            label1.Name = "label1";
            label1.Size = new Size(69, 20);
            label1.TabIndex = 0;
            label1.Text = "模块名称";
            // 
            // PropertyGridView
            // 
            PropertyGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PropertyGridView.Dock = DockStyle.Fill;
            PropertyGridView.Location = new Point(0, 0);
            PropertyGridView.Name = "PropertyGridView";
            PropertyGridView.RowHeadersWidth = 51;
            PropertyGridView.Size = new Size(878, 532);
            PropertyGridView.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.Controls.Add(PropertyGridView);
            panel1.Location = new Point(265, 142);
            panel1.Name = "panel1";
            panel1.Size = new Size(878, 532);
            panel1.TabIndex = 2;
            // 
            // ExportWindow
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 34, 34);
            ClientSize = new Size(1155, 692);
            Controls.Add(panel1);
            Controls.Add(ExportPanel);
            Controls.Add(VerfiyMessageList);
            Name = "ExportWindow";
            Text = "导出网络结构";
            Load += ExportWindow_Load;
            ExportPanel.ResumeLayout(false);
            ExportPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PropertyGridView).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListBox VerfiyMessageList;
        private Panel ExportPanel;
        private TextBox textBox1;
        private Label label1;
        private Button ExportButton;
        private ComboBox ModuleBox;
        private Label label2;
        private DataGridView PropertyGridView;
        private Panel panel1;
    }
}