using Zhiyun.TrainTask.Models.Criterions;
using Zhiyun.TrainTask.Models.Optimizers;

namespace Zhiyun.Winform.Components
{
    public partial class TrainWizardTrainOptionsPage : WizardPage
    {
        public TrainWizardTrainOptionsPage(Control control)
        {
            InitializeComponent();
            Parent = control;
            CbTrainDevice.SelectedIndexChanged += CbTrainDevice_SelectedIndexChanged;
        }

        private string[] DeviceMessages = [
            "使用计算机CPU设备进行训练。",
            "CUDA是由NVIDIA开发的并行计算平台和应用程序编程接口，具有强大的性能和广泛的支持。通过使用CUDA，Pytorch可以利用GPU的强大计算能力，加速深度学习模型的训练过程。",
            "HIP是由AMD开发的用于异构计算的编程模型和工具集，类似于CUDA通过使用HIP，Pytorch可以在支持AMD显卡的设备上进行加速，提高深度学习模型的训练速度。",
            "OpenCL是一个开放的异构计算框架，允许利用不同类型的硬件进行并行计算。",
            "使用苹果的MPS作为PyTorch的后端，可以实现加速GPU训练。它提供了在Mac上设置和运行操作的脚本和功能，通过针对每个Metal GPU系列的独特特性进行微调的内核来优化计算性能。"
        ];

        public Optimizer? Optimizer => CbOptimizer.SelectedItem as Optimizer;
        public Criterion? Criterion => CbCriterion.SelectedItem as Criterion;
        public int BatchSize => (int)numericUpDown1.Value;
        public string Device => CbTrainDevice.SelectedText;

        public override bool CanMoveNextPage()
            => Optimizer != null && Criterion != null;

        private void TrainWizardTrainOptionsPage_Load(object sender, EventArgs e)
        {
            CbOptimizer.Items.AddRange(Optimizer.LoadOptimizers().ToArray());
            CbCriterion.Items.AddRange(Criterion.LoadCriterions().ToArray());
            CbOptimizer.SelectedIndex = 0;
            CbCriterion.SelectedIndex = 0;
            CbTrainDevice.SelectedIndex = 0;
            numericUpDown1.Value = 16;
        }


        private void OnComboxValueChanged(object sender, EventArgs e)
        {
            /*
            var builder = new StringBuilder();
            if (CbOptimizer.SelectedItem is Optimizer optimizer)
                builder.AppendLine($"优化器名称:{optimizer.Name}");
            if (CbCriterion.SelectedItem is Criterion criterion)
                builder.AppendLine($"损失函数名称:{criterion.Name}");
            if (!string.IsNullOrEmpty(Device))
                builder.AppendLine($"训练设备:{Device}");
            builder.AppendLine($"BatchSize:{BatchSize}");
            TotalMessage.Text = builder.ToString();
            */
        }



        private void linkOptimizer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void CbTrainDevice_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var message = DeviceMessages[CbTrainDevice.SelectedIndex];
            DeviceMessage.Text = message;
        }
    }
}
