using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhiyun.FlowChart.NodeEditor;
using Zhiyun.Nodes;
using Zhiyun.Nodes.Modules;
using Zhiyun.Utilities.Extensions;

namespace Zhiyun.Winform
{
    public static class Extensions
    {
        public static void LoadNodes(this STNodeEditor editor) 
        {
            editor.LoadAssembly(Application.ExecutablePath.Replace("Zhiyun.Winform.exe", "Zhiyun.Nodes.dll"));
            var type = CustomModule.CreateCustomModuleType(File.ReadAllText("Models/data.json").ToObject<ModuleMessage>());
            editor.LoadType(type);
        }

        public static void LoadNodes(this STNodeTreeView view)
        {
            view.LoadAssembly(Application.ExecutablePath.Replace("Zhiyun.Winform.exe", "Zhiyun.Nodes.dll"));
            var type = CustomModule.CreateCustomModuleType(File.ReadAllText("Models/data.json").ToObject<ModuleMessage>());
            view.LoadType(type);
        }
    }
}
