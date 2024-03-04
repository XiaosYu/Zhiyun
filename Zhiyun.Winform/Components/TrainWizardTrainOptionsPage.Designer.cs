namespace Zhiyun.Winform.Components
{
    partial class TrainWizardTrainOptionsPage
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
            CbOptimizer = new ComboBox();
            label2 = new Label();
            CbCriterion = new ComboBox();
            label3 = new Label();
            linkOptimizer = new LinkLabel();
            label1 = new Label();
            numericUpDown1 = new NumericUpDown();
            CbTrainDevice = new ComboBox();
            label4 = new Label();
            DeviceMessage = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // CbOptimizer
            // 
            CbOptimizer.DropDownStyle = ComboBoxStyle.DropDownList;
            CbOptimizer.FormattingEnabled = true;
            CbOptimizer.Location = new Point(69, 118);
            CbOptimizer.Name = "CbOptimizer";
            CbOptimizer.Size = new Size(297, 28);
            CbOptimizer.TabIndex = 2;
            CbOptimizer.SelectedIndexChanged += OnComboxValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(69, 95);
            label2.Name = "label2";
            label2.Size = new Size(84, 20);
            label2.TabIndex = 3;
            label2.Text = "优化器名称";
            // 
            // CbCriterion
            // 
            CbCriterion.DropDownStyle = ComboBoxStyle.DropDownList;
            CbCriterion.FormattingEnabled = true;
            CbCriterion.Location = new Point(69, 56);
            CbCriterion.Name = "CbCriterion";
            CbCriterion.Size = new Size(297, 28);
            CbCriterion.TabIndex = 4;
            CbCriterion.SelectedIndexChanged += OnComboxValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(69, 33);
            label3.Name = "label3";
            label3.Size = new Size(99, 20);
            label3.TabIndex = 5;
            label3.Text = "损失函数类型";
            // 
            // linkOptimizer
            // 
            linkOptimizer.AutoSize = true;
            linkOptimizer.Location = new Point(372, 121);
            linkOptimizer.Name = "linkOptimizer";
            linkOptimizer.Size = new Size(69, 20);
            linkOptimizer.TabIndex = 6;
            linkOptimizer.TabStop = true;
            linkOptimizer.Text = "编辑参数";
            linkOptimizer.LinkClicked += linkOptimizer_LinkClicked;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(69, 159);
            label1.Name = "label1";
            label1.Size = new Size(78, 20);
            label1.TabIndex = 8;
            label1.Text = "BatchSize";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(69, 182);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(297, 27);
            numericUpDown1.TabIndex = 9;
            // 
            // CbTrainDevice
            // 
            CbTrainDevice.DropDownStyle = ComboBoxStyle.DropDownList;
            CbTrainDevice.FormattingEnabled = true;
            CbTrainDevice.Items.AddRange(new object[] { "CPU", "CUDA", "HIP", "OpenCL", "MPS" });
            CbTrainDevice.Location = new Point(69, 251);
            CbTrainDevice.Name = "CbTrainDevice";
            CbTrainDevice.Size = new Size(297, 28);
            CbTrainDevice.TabIndex = 10;
            CbTrainDevice.SelectedIndexChanged += OnComboxValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(69, 228);
            label4.Name = "label4";
            label4.Size = new Size(69, 20);
            label4.TabIndex = 11;
            label4.Text = "训练设备";
            // 
            // DeviceMessage
            // 
            DeviceMessage.BorderStyle = BorderStyle.None;
            DeviceMessage.Location = new Point(69, 285);
            DeviceMessage.Name = "DeviceMessage";
            DeviceMessage.Size = new Size(297, 107);
            DeviceMessage.TabIndex = 12;
            DeviceMessage.Text = "";
            // 
            // TrainWizardTrainOptionsPage
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            Controls.Add(DeviceMessage);
            Controls.Add(label4);
            Controls.Add(CbTrainDevice);
            Controls.Add(numericUpDown1);
            Controls.Add(label1);
            Controls.Add(linkOptimizer);
            Controls.Add(label3);
            Controls.Add(CbCriterion);
            Controls.Add(label2);
            Controls.Add(CbOptimizer);
            Name = "TrainWizardTrainOptionsPage";
            Size = new Size(999, 511);
            Load += TrainWizardTrainOptionsPage_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox CbOptimizer;
        private Label label2;
        private ComboBox CbCriterion;
        private Label label3;
        private LinkLabel linkOptimizer;
        private Label label1;
        private NumericUpDown numericUpDown1;
        private ComboBox CbTrainDevice;
        private Label label4;
        private RichTextBox DeviceMessage;
    }
}
