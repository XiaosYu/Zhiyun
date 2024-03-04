using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zhiyun.TrainTask.Models.Optimizers;

namespace Zhiyun.TrainTask.Models.Criterions
{
    public class Criterion
    {
        public string Name { set; get; }

        public static List<Criterion> LoadCriterions()
        {
            return [
                new Criterion() { Name = "MSELoss" },
                new Criterion() { Name = "CrossEnrtyLoss" }
            ];
        }

        public override string ToString() => Name;
    }
}
