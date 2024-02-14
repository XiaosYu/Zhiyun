namespace Zhiyun.Nodes
{
    public class Dimension
    {
        public const int Batch = -10;
        public Dimension(params int[] dimension) => Dimensions = [.. dimension];
        public Dimension() { }
        public Dimension(List<int> dimension) => Dimensions = [.. dimension];
        public static Dimension Create(params int[] dimension)
        {
            var dimensions = new Dimension();
            dimensions.Dimensions = [Batch, .. dimension];
            return dimensions;
        }
        public static Dimension Empty => Create();

        public int this[int index]
        {
            get => Dimensions[index];
            set => Dimensions[index] = value;
        }

        public Dimension Clone() => new(Dimensions);

        [JsonPropertyName(nameof(Dimensions))]
        public List<int> Dimensions { get; set; }

        [JsonIgnore]
        public int DimensionsCount => Dimensions.Count;
        [JsonIgnore]
        public bool IsVector => Dimensions.Count == 2;
        [JsonIgnore]
        public bool IsImage => Dimensions.Count == 4;
        [JsonIgnore]
        public bool OnlyBatch => Dimensions.Count == 1;
        [JsonIgnore]
        public Dimension DimensionWithoutBatch => new(Dimensions.Except([Batch]).ToList());
        [JsonIgnore]
        public int ValueCount => Dimensions.Where(s=>s != Batch).Aggregate((product, next) => product * next);

        public Dimension SetDimension(int dimension, int value)
        {
            Dimensions[dimension] = value;
            return this;
        }

        public override string ToString()
        {
            return ToString('×');
        }

        public string ToString(char c = '×')
        {
            return string.Join(c, Dimensions.GetRange(1, Dimensions.Count - 1));
        }


        public static implicit operator string(Dimension dimension) => dimension.ToString();

       
            
    }
}
