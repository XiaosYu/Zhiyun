using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zhiyun.Nodes;
using Zhiyun.Nodes.Modules;
using Zhiyun.Nodes.Structures;
using Zhiyun.Utilities.Extensions;
using Zhiyun.Winform.Components;
using Zhiyun.Winform.Models;
using Zhiyun.Winform.Services;

namespace Zhiyun.Winform.Views
{
    public partial class NetworkWindow : Form
    {
        public NetworkWindow()
        {
            InitializeComponent();
        }

        private Project CurrentProject { get; set; } = new() { ProjectName = "未命名项目", LastModified = DateTime.Now, FileNames = [] };
        private string? CurrentRoot { get; set; }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            NodeEditor.Initialize();
            NodeTreeView.Initialize();

            NodeEditor.ActiveChanged += (s, ea) => NodePropertyGrid.SetNode(NodeEditor.ActiveNode);
            NodeEditor.OptionConnected += (s, ea) => NodeEditor.ShowAlert(ea.Status.ToString(), Color.White, ea.Status == ConnectionStatus.Connected ? Color.FromArgb(125, Color.Green) : Color.FromArgb(125, Color.Red));
            NodeEditor.CanvasScaled += (s, ea) => NodeEditor.ShowAlert(NodeEditor.CanvasScale.ToString("F2"), Color.White, Color.FromArgb(125, Color.Yellow));

            NodeEditor.OptionConnected += NodeEditor_OptionConnected;
            NodeEditor.NodeAdded += NodeEditor_NodeAdded;
            
        }

        private void NodeEditor_NodeAdded(object sender, STNodeEditorEventArgs e)
        {
            if (NodeEditor.Nodes.ToArray().Count(s => s is Output) == 2 && e.Node is Output)
            {
                Notification.Error("添加节点错误", "已经有一个输出节点，请勿重复添加");
                NodeEditor.Nodes.Remove(e.Node);
                return;
            }
            else if (NodeEditor.Nodes.ToArray().Count(s => s is Input) == 2 && e.Node is Input)
            {
                Notification.Error("添加节点错误", "已经有一个输入节点，请勿重复添加");
                NodeEditor.Nodes.Remove(e.Node);
                return;
            }

            if (e.Node is CustomModule node)
            {
                var contextMenuStrip = new CustomModuleContextMenuStrip(node);
                node.ContextMenuStrip = contextMenuStrip;
            }
            else e.Node.ContextMenuStrip = new NodeContextMenuStrip();

            if(e.Node.ContextMenuStrip is NodeContextMenuStrip strip)
            {
                strip.OnClickDelete = () =>
                {
                    NodeEditor.Nodes.Remove(e.Node);
                    NodeEditor.Nodes.ToArray().Foreach(s => (s as NodeBase)!.ChildNodes.Remove((e.Node as NodeBase)!));
                };
            }
          

          
        }

        private void NodeEditor_OptionConnected(object sender, STNodeEditorOptionEventArgs e)
        {
            if (e.CurrentOption.Owner is NodeBase current && e.TargetOption.Owner is NodeBase target)
            {
                if (!e.CurrentOption.IsInput)
                {
                    if (e.Status == ConnectionStatus.Connected)
                        current.ChildNodes.Add(target);
                    else if (e.Status == ConnectionStatus.DisConnected)
                        current.ChildNodes.Remove(target);
                }
                else
                {
                    if (e.Status == ConnectionStatus.Connected)
                        target.ChildNodes.Add(current);
                    else if (e.Status == ConnectionStatus.DisConnected)
                        target.ChildNodes.Remove(current);
                }
            }

        }

        private async void AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var dialog = new FolderBrowserDialog();
            dialog.Description = "请选择项目保存目录";
            if (dialog.ShowDialog() == DialogResult.OK && dialog.SelectedPath != default)
            {
                var project = new ProjectService();
                var projectName = ProjectName.Text;
                var result = await project.SaveProjectAsync($"{dialog.SelectedPath}/{projectName}", async (path) =>
                {

                    NodeEditor.SaveCanvas($"{path}/canvas.zyf");

                    var totalNodes = NodeEditor.Nodes.ToArray().Where(s => s is NodeBase).Select(s => (s as NodeBase)!.GetNodeData());
                    var monolithicNode = new MonolithicNode()
                    {
                        Nodes = totalNodes.ToList()
                    };

                    await File.WriteAllTextAsync($"{path}/network.zyn", monolithicNode.ToJson());

                    CurrentProject.FileNames = [
                        $"{projectName}.zypj",
                        $"canvas.zyf",
                        $"network.zyn"
                    ];

                    CurrentProject.ProjectName = projectName;
                    CurrentRoot = path;

                    await File.WriteAllTextAsync($"{path}/{projectName}.zypj", CurrentProject.ToJson());
                });
                if (result)
                    Notification.Inform("保存成功", "项目保存成功");

            }
        }

        private async void OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            dialog.Filter = "项目文件 (.zypj)|*.zypj";
            if (dialog.ShowDialog() == DialogResult.OK && dialog.FileName != default)
            {
                var project = new ProjectService();
                CurrentRoot = Path.GetDirectoryName(dialog.FileName);
                var result = await project.LoadProjectAsync(dialog.FileName, async (project) =>
                {
                    CurrentProject = project;
                    NodeEditor.Nodes.Clear();
                    NodeEditor.LoadCanvas($"{CurrentRoot}/{project.FileNames.First(s => s.Contains(".zyf"))}");
                    ProjectName.Text = project.ProjectName;
                    await Task.CompletedTask;
                });
            }
        }

        private void NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NodeEditor.Nodes.Count != 0)
            {
                var check = Notification.Confirm("文件未保存", "当前结构尚未保存，是否保存?");
                if (check == DialogResult.Yes)
                {
                    AToolStripMenuItem_Click(sender, e);
                    CurrentRoot = null;
                }
                NodeEditor.Nodes.Clear();
                ProjectName.Text = "未命名项目";
                CurrentProject = new() { ProjectName = "未命名项目", LastModified = DateTime.Now, FileNames = [] };
            }
        }

        private async void SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentProject.FileNames.Count != 0 && !string.IsNullOrEmpty(CurrentRoot))
            {
                var flowChatPath = CurrentProject.FileNames.First(s => s.Contains(".zyf"));
                var networkPath = CurrentProject.FileNames.First(s => s.Contains(".zyn"));
                var projectPath = CurrentProject.FileNames.First(s => s.Contains(".zypj"));
                var project = new ProjectService();
                await project.SaveProjectAsync(async () =>
                {
                    NodeEditor.SaveCanvas($"{CurrentRoot}/{flowChatPath}");
                    var firstNode = NodeEditor.Nodes.ToArray().First(s => s is Input) as Input;
                    var node = firstNode!.GetNodeData();

                    await File.WriteAllTextAsync($"{CurrentRoot}/{networkPath}", node.ToJson());
                    CurrentProject.LastModified = DateTime.Now;

                    await File.WriteAllTextAsync($"{CurrentRoot}/{projectPath}", CurrentProject.ToJson());
                });
            }
            else
            {
                var check = Notification.Confirm("文件未保存", "当前结构尚未保存，是否保存?");
                if (check == DialogResult.Yes)
                {
                    AToolStripMenuItem_Click(sender, e);
                }
            }
        }

        private void EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SToolStripMenuItem_Click(sender, e);
            var stream = new MemoryStream();
            NodeEditor.SaveCanvas(stream);
            var bytes = stream.ToArray();
            var window = new ExportWindow(new ModuleMessage()
            {
                Graphs = bytes.ToBase64String(),
                Name = ProjectName.Text,
                Monolithic = new MonolithicNode()
                {
                    Nodes = NodeEditor.Nodes.ToArray().Where(s => s is NodeBase).Select(s => (s as NodeBase)!.GetNodeData()).ToList()
                }
            });
            window.ShowDialog();
        }

        
    }
}
