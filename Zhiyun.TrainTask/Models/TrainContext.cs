using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhiyun.Python;

namespace Zhiyun.TrainTask.Models
{
    public class TrainContext(PythonContext context)
    {
        private PythonContext Context { get; set; } = context;


        public Task RunAsync(TrainTaskOptions options) => Task.Run(() => { });
    }
}
