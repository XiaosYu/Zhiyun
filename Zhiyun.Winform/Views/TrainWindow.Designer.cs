namespace Zhiyun.Winform.Views
{
    partial class TrainWindow
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
            pythonEnvirments = new TextBox();
            panel1 = new Panel();
            comboBox1 = new ComboBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pythonEnvirments
            // 
            pythonEnvirments.BackColor = SystemColors.GrayText;
            pythonEnvirments.BorderStyle = BorderStyle.FixedSingle;
            pythonEnvirments.ForeColor = SystemColors.ControlLightLight;
            pythonEnvirments.Location = new Point(34, 20);
            pythonEnvirments.Name = "pythonEnvirments";
            pythonEnvirments.Size = new Size(355, 27);
            pythonEnvirments.TabIndex = 0;
            pythonEnvirments.Text = "点击设置Python解释器位置";
            // 
            // panel1
            // 
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(pythonEnvirments);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(465, 432);
            panel1.TabIndex = 1;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(113, 67);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(276, 28);
            comboBox1.TabIndex = 1;
            // 
            // TrainWindow
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 34, 34);
            ClientSize = new Size(1032, 450);
            Controls.Add(panel1);
            Name = "TrainWindow";
            StartPosition = FormStartPosition.CenterParent;
            Text = "训练模型(本地)";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox pythonEnvirments;
        private Panel panel1;
        private ComboBox comboBox1;
    }
}