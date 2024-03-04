using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zhiyun.Winform.Components
{
    public partial class TrainWizardSelectTrainPlateformPage : WizardPage
    {
        public TrainWizardSelectTrainPlateformPage(Control parent)
        {
            InitializeComponent();
            Parent = parent;
        }

        public string PlateformType => (SelectedPlateform.SelectedItem as string)!;
        public string PlateformConnectionString => textBox1.Text;

        public override bool CanMoveNextPage()
            => !string.IsNullOrEmpty(PlateformType) && !string.IsNullOrEmpty(PlateformConnectionString);

        private string[] DisplayMessage => [
            "使用本地CPU或者GPU进行训练，需要Python环境且有PyTorch工具包支持\n" +
            "torch>=2.0,torchvision,torchtext,numpy,pandas",
            "使用远程的主机进行训练，远程主机也必须提供所需要Python环境且有PyTorch工具包支持",
            "使用Zhiyun.Cloud提供的云环境进行训练，无任何环境要求即可训练以及部署",
            "使用其他服务商提供的云环境进行训练（必须部署Zhiyun的接口）"
            ];

        private void SelectedPlateform_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowMessage.Text = DisplayMessage[SelectedPlateform.SelectedIndex];
            LabelText.Text = SelectedPlateform.SelectedIndex switch
            {
                0 => "Python解释器地址",
                1 => "远程Python解释器地址",
                2 => "Zhiyun.Cloud云服务连接字符串",
                3 => "其他服务商连接字符串",
                _ => throw new Exception("未知错误")
            };
        }

        private void TrainWizardSelectTrainPlateformPage_Load(object sender, EventArgs e)
        {
            SelectedPlateform.Items.AddRange(["LocalPythonEnvironment", "RemotePythonEnvironment", "www.zhiyun.cloud", "其他服务提供商"]);
            SelectedPlateform.SelectedIndex = 0;
        }
    }
}
