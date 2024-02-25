using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zhiyun.Utilities.Extensions;

namespace Zhiyun.Nodes.Services
{
    public class NodeTypeService: IEnumerable<Type>
    {
        private static NodeTypeService? shared;
        public static NodeTypeService Shared 
        {
            get
            {
                shared ??= new();
                return shared;
            }
        }

        public NodeTypeService()
        {
            //载入基本类型
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes().Where(s=>s.IsSubclassOf(typeof(NodeBase)));
            LoadedTypes.AddRange(types);
#if DEBUG
          
            //载入导出模型
            var modules = Directory.GetFiles("Models");
            foreach (var module in modules) 
            {
                var moduleMessage = File.ReadAllText(module).ToObject<ModuleMessage>();
                if (moduleMessage != null)
                {
                    var type = CustomModule.CreateCustomModuleType(moduleMessage);
                    LoadedTypes.Add(type);
                }
            }
      
#endif
        }

        public List<Type> LoadedTypes { get; } = [];

        public IEnumerator<Type> GetEnumerator() => LoadedTypes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => LoadedTypes.GetEnumerator();
    }
}
