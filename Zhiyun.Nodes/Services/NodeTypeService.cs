using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            var types = assembly.GetTypes();
            LoadedTypes.AddRange(types);
        }

        public List<Type> LoadedTypes { get; } = [];

        public IEnumerator<Type> GetEnumerator() => LoadedTypes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => LoadedTypes.GetEnumerator();
    }
}
