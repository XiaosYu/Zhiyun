using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Zhiyun.TrainTask.Models.Optimizers
{
    public class SGDOptimizer: Optimizer
    {
        [Settable]
        [JsonPropertyName("momentum")]
        public float Momentum { get; set; }
    }
}
