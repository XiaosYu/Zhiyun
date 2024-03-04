#nullable disable

using System.Reflection;
using System.Text.Json.Serialization;
using Zhiyun.Utilities.Extensions;

namespace Zhiyun.TrainTask.Models.Optimizers
{
    public abstract class Optimizer
    {
        public string Name { get; set; }

        protected Optimizer() 
        {
            Name = this.GetClassName();
        }

        [Settable]
        [JsonPropertyName("lr")]
        public float LearningRate { get; set; }


        public static List<Optimizer> LoadOptimizers()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(s => s.IsSubclassOf(typeof(Optimizer))).Select(s => Activator.CreateInstance(s)! as Optimizer).ToList();
            return types;
        }

        public override string ToString() => Name;
    }
}
