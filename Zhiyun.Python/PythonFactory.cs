using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Python
{
    public class PythonFactory
    {
        private static PythonFactory instance;
        public static PythonFactory Instance => instance;

        public static void Initialize(string pythonDll, string pythonHome)
        {
            if (instance != null) return;
            Runtime.PythonDLL = pythonDll;
            PythonEngine.PythonHome = pythonHome;
            PythonEngine.Initialize();
            PythonEngine.BeginAllowThreads();
            instance = new PythonFactory();
        }

        public PythonContext Create() => new();
        public Task<PythonContext> CreateAsync() => Task.FromResult(Create());
    }
}
