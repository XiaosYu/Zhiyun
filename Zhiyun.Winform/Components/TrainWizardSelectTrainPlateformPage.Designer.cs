namespace Zhiyun.Winform.Components
{
    partial class TrainWizardSelectTrainPlateformPage
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
            ShowMessage = new RichTextBox();
            SelectedPlateform = new ComboBox();
            label1 = new Label();
            textBox1 = new TextBox();
            LabelText = new Label();
            SuspendLayout();
            // 
            // ShowMessage
            // 
            ShowMessage.BorderStyle = BorderStyle.None;
            ShowMessage.Location = new Point(73, 125);
            ShowMessage.Name = "ShowMessage";
            ShowMessage.Size = new Size(411, 92);
            ShowMessage.TabIndex = 5;
            ShowMessage.Text = "ShowMessage";
            // 
            // SelectedPlateform
            // 
            SelectedPlateform.DropDownStyle = ComboBoxStyle.DropDownList;
            SelectedPlateform.FormattingEnabled = true;
            SelectedPlateform.Location = new Point(73, 91);
            SelectedPlateform.Name = "SelectedPlateform";
            SelectedPlateform.Size = new Size(411, 28);
            SelectedPlateform.TabIndex = 4;
            SelectedPlateform.SelectedIndexChanged += SelectedPlateform_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(73, 68);
            label1.Name = "label1";
            label1.Size = new Size(144, 20);
            label1.TabIndex = 3;
            label1.Text = "请选择你的训练平台";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(73, 236);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(411, 27);
            textBox1.TabIndex = 6;
            // 
            // LabelText
            // 
            LabelText.AutoSize = true;
            LabelText.Location = new Point(73, 213);
            LabelText.Name = "LabelText";
            LabelText.Size = new Size(135, 20);
            LabelText.TabIndex = 7;
            LabelText.Text = "Python解释器地址";
            // 
            // TrainWizardSelectTrainPlateformPage
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            Controls.Add(LabelText);
            Controls.Add(textBox1);
            Controls.Add(ShowMessage);
            Controls.Add(SelectedPlateform);
            Controls.Add(label1);
            DoubleBuffered = true;
            Name = "TrainWizardSelectTrainPlateformPage";
            Size = new Size(999, 511);
            Load += TrainWizardSelectTrainPlateformPage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox ShowMessage;
        private ComboBox SelectedPlateform;
        private Label label1;
        private TextBox textBox1;
        private Label LabelText;
    }
}
