namespace Zhiyun.Winform.Components
{
    partial class TrainWizardModuleMessagePage
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
            label1 = new Label();
            textBox1 = new TextBox();
            PreviewNodeEditor = new STNodeEditor();
            LabelMessage = new Label();
            linkLabel1 = new LinkLabel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(52, 28);
            label1.Name = "label1";
            label1.Size = new Size(99, 20);
            label1.TabIndex = 0;
            label1.Text = "模型项目文件";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(52, 51);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(373, 27);
            textBox1.TabIndex = 1;
            // 
            // PreviewNodeEditor
            // 
            PreviewNodeEditor.AllowDrop = true;
            PreviewNodeEditor.BackColor = Color.FromArgb(34, 34, 34);
            PreviewNodeEditor.BorderColor = Color.White;
            PreviewNodeEditor.BorderThickness = 1;
            PreviewNodeEditor.Curvature = 0.3F;
            PreviewNodeEditor.GridColor = Color.DimGray;
            PreviewNodeEditor.Location = new Point(52, 84);
            PreviewNodeEditor.LocationBackColor = Color.FromArgb(120, 0, 0, 0);
            PreviewNodeEditor.MarkBackColor = Color.FromArgb(180, 0, 0, 0);
            PreviewNodeEditor.MarkForeColor = Color.FromArgb(180, 0, 0, 0);
            PreviewNodeEditor.MinimumSize = new Size(100, 100);
            PreviewNodeEditor.Name = "PreviewNodeEditor";
            PreviewNodeEditor.Radians = 20;
            PreviewNodeEditor.Size = new Size(448, 204);
            PreviewNodeEditor.TabIndex = 3;
            PreviewNodeEditor.Text = "stNodeEditor1";
            // 
            // LabelMessage
            // 
            LabelMessage.AutoSize = true;
            LabelMessage.Location = new Point(52, 300);
            LabelMessage.Name = "LabelMessage";
            LabelMessage.Size = new Size(53, 20);
            LabelMessage.TabIndex = 4;
            LabelMessage.Text = "label2";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(431, 54);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(69, 20);
            linkLabel1.TabIndex = 5;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "检测模型";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // TrainWizardModuleMessagePage
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(linkLabel1);
            Controls.Add(LabelMessage);
            Controls.Add(PreviewNodeEditor);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "TrainWizardModuleMessagePage";
            Size = new Size(1284, 405);
            Load += TrainWizardModuleMessagePage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private STNodeEditor PreviewNodeEditor;
        private Label LabelMessage;
        private LinkLabel linkLabel1;
    }
}
