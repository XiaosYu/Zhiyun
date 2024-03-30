namespace Zhiyun.Winform.Views
{
    partial class TrainWizardWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrainWizardWindow));
            WizardPanel = new Panel();
            pictureBox1 = new PictureBox();
            BtLastStep = new Button();
            BtNextStep = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // WizardPanel
            // 
            WizardPanel.Location = new Point(240, 4);
            WizardPanel.Name = "WizardPanel";
            WizardPanel.Size = new Size(557, 407);
            WizardPanel.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(231, 445);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // BtLastStep
            // 
            BtLastStep.Location = new Point(603, 417);
            BtLastStep.Name = "BtLastStep";
            BtLastStep.Size = new Size(94, 29);
            BtLastStep.TabIndex = 2;
            BtLastStep.Text = "上一步(&B)";
            BtLastStep.UseVisualStyleBackColor = true;
            BtLastStep.Click += BtLastStep_Click;
            // 
            // BtNextStep
            // 
            BtNextStep.Location = new Point(703, 417);
            BtNextStep.Name = "BtNextStep";
            BtNextStep.Size = new Size(94, 29);
            BtNextStep.TabIndex = 3;
            BtNextStep.Text = "下一步(&N)";
            BtNextStep.UseVisualStyleBackColor = true;
            BtNextStep.Click += BtNextStep_Click;
            // 
            // TrainWizardWindow
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(800, 450);
            Controls.Add(BtNextStep);
            Controls.Add(BtLastStep);
            Controls.Add(pictureBox1);
            Controls.Add(WizardPanel);
            Name = "TrainWizardWindow";
            Text = "训练任务导航";
            Load += TrainWizardWindow_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel WizardPanel;
        private PictureBox pictureBox1;
        private Button BtLastStep;
        private Button BtNextStep;
    }
}