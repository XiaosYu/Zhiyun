namespace Zhiyun.Winform.Components
{
    partial class TrainWizardBaseMessagePage
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            tbName = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            comboBox1 = new ComboBox();
            TemplateMessage = new RichTextBox();
            SuspendLayout();
            // 
            // tbName
            // 
            tbName.Location = new Point(80, 78);
            tbName.Name = "tbName";
            tbName.Size = new Size(377, 27);
            tbName.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(80, 55);
            label1.Name = "label1";
            label1.Size = new Size(69, 20);
            label1.TabIndex = 1;
            label1.Text = "项目名称";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(80, 136);
            label2.Name = "label2";
            label2.Size = new Size(84, 20);
            label2.TabIndex = 2;
            label2.Text = "项目文件夹";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(80, 159);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(377, 27);
            textBox1.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(80, 213);
            label3.Name = "label3";
            label3.Size = new Size(69, 20);
            label3.TabIndex = 4;
            label3.Text = "项目模板";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(80, 236);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(377, 28);
            comboBox1.TabIndex = 5;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // TemplateMessage
            // 
            TemplateMessage.BorderStyle = BorderStyle.None;
            TemplateMessage.Location = new Point(80, 270);
            TemplateMessage.Name = "TemplateMessage";
            TemplateMessage.Size = new Size(377, 102);
            TemplateMessage.TabIndex = 6;
            TemplateMessage.Text = "";
            // 
            // TrainWizardBaseMessagePage
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            Controls.Add(TemplateMessage);
            Controls.Add(comboBox1);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tbName);
            Name = "TrainWizardBaseMessagePage";
            Size = new Size(999, 511);
            Load += TrainWizardBaseMessagePage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbName;
        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private Label label3;
        private ComboBox comboBox1;
        private RichTextBox TemplateMessage;
    }
}
