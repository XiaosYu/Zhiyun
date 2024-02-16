using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zhiyun.Nodes;
using Zhiyun.Utilities.Extensions;
using Zhiyun.Winform.Models;
using Zhiyun.Winform.Services;

namespace Zhiyun.Winform.Views
{
    public partial class ExportWindow : Form
    {
        public ExportWindow(ModuleMessage moduleMessage)
        {
            InitializeComponent();
            ModuleMessage = moduleMessage;
        }

        private ModuleMessage ModuleMessage { get; }
        private ParameterDataCollection ParameterDataCollection { get; set; } = new();

        private async void ExportWindow_Load(object sender, EventArgs e)
        {
            var totalNodes = ModuleMessage.Monolithic.Nodes;
            VerfiyMessageList.Items.Add($"载入节点数量:{totalNodes.Count}");
            await Task.Delay(500);

            var inputNode = totalNodes.First(s => s.Type.Contains("Input"));
            if (inputNode["OutDim"] is Dimension featuresDim)
            {
                VerfiyMessageList.Items.Add($"输入张量形状:[batch_size,{featuresDim.ToString(',')}]");
                await Task.Delay(500);
            }

            var outputNode = totalNodes.First(s => s.Type.Contains("Output"));
            if (outputNode["FeaturesDim"] is Dimension outDim)
            {
                VerfiyMessageList.Items.Add($"输出张量形状:[batch_size,{outDim.ToString(',')}]");
                await Task.Delay(500);
            }

            var totalParameters = totalNodes.Sum(s => s.LearnableParameters);

            VerfiyMessageList.Items.Add($"参数总量:{totalParameters}");
            await Task.Delay(500);

            VerfiyMessageList.Items.Add($"网络结构检查成功，支持导出");

            ModuleMessage.Monolithic.Nodes.ForEach(s => ParameterDataCollection.AddRange(s.Parameters));

            PropertyGridView.DataSource = ParameterDataCollection;
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            ParameterDataCollection
                .GroupBy(s => s.ParentID)
                .Foreach(s => ModuleMessage.Monolithic[s.Key].Parameters = new ParameterDataCollection(s));

            if (!Path.Exists("Models"))
                Directory.CreateDirectory("Models");

            File.WriteAllText(Path.Combine("Models", "data.json"), ModuleMessage.ToJson());
        }

       
    }
}
