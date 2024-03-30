#nullable disable

using Zhiyun.TrainTask.Models.Criterions;
using Zhiyun.TrainTask.Models.Optimizers;

namespace Zhiyun.TrainTask.Models
{
    public class TrainTaskOptions
    {
        public string TaskID { set; get; }
        public string TaskType { set; get; }
        public Optimizer Optimizer { get; set; }

        public Criterion Criterion { get; set; }
        public string Device { get; set; }
        public int BatchSize { get; set; }
        public string DatasetType { get; set; }
        public string DatasetPath { get; set; }

        public string ModuleMessage { get; set; }


    }
}
