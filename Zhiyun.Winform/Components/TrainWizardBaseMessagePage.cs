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
    public partial class TrainWizardBaseMessagePage : WizardPage
    {
        public TrainWizardBaseMessagePage(Control control)
        {
            InitializeComponent();
            Parent = control;
        }

        public string PropjectType => comboBox1.Text;
        public string ProjectName => tbName.Text;
        public string ProjectDirectory => textBox1.Text;

        public override bool CanMoveNextPage()
            => !string.IsNullOrEmpty(ProjectName) && !string.IsNullOrEmpty(ProjectDirectory) && !string.IsNullOrEmpty(PropjectType);


        private readonly string[] ProjectTemplateMessage = [
            "对于向量数据进行特征回归",
            "对于向量数据进行特征分类",
            "对图片数据进行分类",
            "使用物理信息网络(PINN)算法,对ODE/PDF进行求解",
            "使用物理信息网络(PINN)算法,基于一定量的物理数据进行学习，得到黑盒模型",
            "使用物理信息网络(PINN)算法,基于一定量的物理数据下对含未知变量的ODE/PDF进行求解"
            ];

        private void TrainWizardBaseMessagePage_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange([
                "向量特征回归", 
                "向量特征分类", 
                "图片分类", 
                "PINN纯物理信息驱动", 
                "PINN纯数据驱动", 
                "PINN反向问题求解"
            ]);
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TemplateMessage.Text = ProjectTemplateMessage[comboBox1.SelectedIndex];
        }
    }
}
