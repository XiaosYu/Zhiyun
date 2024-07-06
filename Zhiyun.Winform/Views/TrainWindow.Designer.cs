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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrainWindow));
            richTextBox1 = new RichTextBox();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            panel2 = new Panel();
            pictureBox3 = new PictureBox();
            label3 = new Label();
            panel3 = new Panel();
            pictureBox4 = new PictureBox();
            label4 = new Label();
            panel4 = new Panel();
            label5 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Location = new Point(12, 35);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(277, 389);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // panel1
            // 
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(295, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(370, 203);
            panel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.figure;
            pictureBox1.Location = new Point(24, 23);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(320, 180);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(134, 0);
            label1.Name = "label1";
            label1.Size = new Size(84, 20);
            label1.TabIndex = 0;
            label1.Text = "训练损失图";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.eval1;
            pictureBox2.Location = new Point(24, 23);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(320, 180);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(134, 0);
            label2.Name = "label2";
            label2.Size = new Size(84, 20);
            label2.TabIndex = 0;
            label2.Text = "验证损失图";
            // 
            // panel2
            // 
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(672, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(370, 203);
            panel2.TabIndex = 2;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.a_eval;
            pictureBox3.Location = new Point(24, 23);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(320, 180);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 1;
            pictureBox3.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(134, 0);
            label3.Name = "label3";
            label3.Size = new Size(84, 20);
            label3.TabIndex = 0;
            label3.Text = "训练效果图";
            // 
            // panel3
            // 
            panel3.Controls.Add(pictureBox3);
            panel3.Controls.Add(label3);
            panel3.Location = new Point(295, 221);
            panel3.Name = "panel3";
            panel3.Size = new Size(370, 203);
            panel3.TabIndex = 3;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.b_eval;
            pictureBox4.Location = new Point(24, 23);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(320, 180);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 1;
            pictureBox4.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.ControlLightLight;
            label4.Location = new Point(134, 0);
            label4.Name = "label4";
            label4.Size = new Size(84, 20);
            label4.TabIndex = 0;
            label4.Text = "验证效果图";
            // 
            // panel4
            // 
            panel4.Controls.Add(pictureBox4);
            panel4.Controls.Add(label4);
            panel4.Location = new Point(672, 221);
            panel4.Name = "panel4";
            panel4.Size = new Size(370, 203);
            panel4.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = SystemColors.ControlLightLight;
            label5.Location = new Point(93, 9);
            label5.Name = "label5";
            label5.Size = new Size(69, 20);
            label5.TabIndex = 2;
            label5.Text = "训练日志";
            // 
            // TrainWindow
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 34, 34);
            ClientSize = new Size(1054, 433);
            Controls.Add(label5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(richTextBox1);
            Name = "TrainWindow";
            StartPosition = FormStartPosition.CenterParent;
            Text = "训练模型(本地)";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox1;
        private Panel panel1;
        private PictureBox pictureBox1;
        private Label label1;
        private PictureBox pictureBox2;
        private Label label2;
        private Panel panel2;
        private PictureBox pictureBox3;
        private Label label3;
        private Panel panel3;
        private PictureBox pictureBox4;
        private Label label4;
        private Panel panel4;
        private Label label5;
    }
}