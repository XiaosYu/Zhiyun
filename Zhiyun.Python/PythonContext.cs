using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Python
{
    public class PythonContext()
    {
        public T Invoke<T>(Func<PyModule, T> context)
        {
            T result;
            using (Py.GIL())
            {
                var scope = Py.CreateScope();
                result = context.Invoke(scope);
            }
            return result;
        }
        public void Invoke(Action<PyModule> context)
        {
            using (Py.GIL())
            {
                var scope = Py.CreateScope();
                context.Invoke(scope);
            }
        }
    }
}
