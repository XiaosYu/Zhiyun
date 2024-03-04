using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zhiyun.Nodes;
using Zhiyun.Utilities.Extensions;

namespace Zhiyun.Winform.Components
{
    public partial class TrainWizardModuleMessagePage : WizardPage
    {
        public TrainWizardModuleMessagePage(Control control)
        {
            InitializeComponent();
            Parent = control;
        }

        public string ModulePath => textBox1.Text;

        public override bool CanMoveNextPage() => !string.IsNullOrEmpty(ModulePath);

        private void TrainWizardModuleMessagePage_Load(object sender, EventArgs e)
        {
            PreviewNodeEditor.Visible = false;
            LabelMessage.Visible = false;
            PreviewNodeEditor.Initialize();
        }

        private async void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists(ModulePath))
            {
                var text = await File.ReadAllTextAsync(ModulePath);
                var moduleMessage = text.ToObject<ModuleMessage>();
                if (moduleMessage != null)
                {
                    PreviewNodeEditor.Visible = true;
                    PreviewNodeEditor.Nodes.Clear();
                    PreviewNodeEditor.LoadCanvas(moduleMessage.Graphs.FromBase64String());

                    var inputDimension = moduleMessage.Monolithic.InputDimension;
                    var outputDimension = moduleMessage.Monolithic.OutputDimension;
                    LabelMessage.Text = $"输入张量形状:{inputDimension}\t输出张量形状:{outputDimension}";
                    return;
                }
            }
            PreviewNodeEditor.Visible = false;
            Notification.Error("模型打开错误", "请输入正确的模型项目文件");
        }
    }
}
