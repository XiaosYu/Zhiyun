using System.Drawing;
using System.Reflection;
using Zhiyun.Utilities.Extensions;

namespace Zhiyun.Nodes
{
    public abstract class NodeBase : STNode
    {
        [STNodeProperty("节点名称", "")]
        public string Name { get; set; }

        public string Id { get; set; }

        private readonly Dictionary<string, STNodeControl> controls = [];

        public NodeBase()
        {
            Name = $"{GetType().Name}_{Random.Shared.RandString(6)}";
            Id = Random.Shared.RandDigit(6);
            var attribute = GetType().GetCustomAttribute<NameAttribute>();
            Title = attribute != null ? attribute.Title : GetType().Name;

            OnInitializePort();
            OnInitializeProperty();
            OnBindingPort();

            Flush();
        }

        protected virtual void OnInitializePort() { }
        protected virtual void OnInitializeProperty() { }

        protected virtual bool CanForward() => true;

        protected virtual void OnBindingPort()
        {
            foreach (STNodeOption option in InputOptions)
                option.DataTransfer += new STNodeOptionEventHandler((object sender, STNodeOptionEventArgs args) =>
                {
                    if (args.Status == ConnectionStatus.Connected && args.TargetOption.Data != null && args.TargetOption.Data is ConnectionData data)
                    {
                        data.OptionIndex = InputOptions.IndexOf(option);
                        OnReceivedMessage(data);
                        Flush();

                    }
                });
        }

        public List<NodeBase> ChildNodes { get; } = [];

        public void AddInPort(string name, bool single) => InputOptions.Add(name, typeof(ConnectionData), single);
        public void AddOutPort(string name, bool single) => OutputOptions.Add(name, typeof(ConnectionData), single);

        public virtual NodeData GetNodeData()
        {
            return new NodeData
            {
                Name = Name,
                Type = GetType().Name,
                Parameters = new ParameterDataCollection(GetType()
                                .GetProperties()
                                .Where(s => s.GetCustomAttribute<PropertyAttribute>() != null)
                                .Select(s =>
                                {
                                    var @default = s.GetCustomAttribute<PropertyAttribute>()!.Default;
                                    return new ParameterData()
                                    {
                                        Id = $"{Random.Shared.RandLower(1)}{Random.Shared.RandString(5)}",
                                        Settable = @default,
                                        Name = s.Name,
                                        Type = s.PropertyType.Name,
                                        Value = s.GetValue(this),
                                        ParentID = Id,
                                        ParentType = GetType().Name
                                    };
                                })
                                .ToList()),
                Id = Id,
                Connected = ChildNodes.Select(s => s.Id).ToList(),
                LearnableParameters = GetType().GetProperty("ParametersNumber") != null ? (int)GetType().GetProperty("ParametersNumber")!.GetValue(this)! : 0
            };
        }

        public virtual void OnReceivedMessage(ConnectionData data)
        {

        }

        public virtual ConnectionData OnSendMessage() => new();
        public virtual void OnFlushComponent() { }

        public virtual void Flush()
        {
            OnFlushComponent();
            if(CanForward())
                foreach (STNodeOption option in OutputOptions)
                    option.TransferData(OnSendMessage());
        }

        protected virtual void AddTextBlockControl(string name, string initialText)
        {
            if (AutoSize)
                AutoSize = false;

            var control = new STNodeControl();
            controls.Add(name, control);
            Controls.Add(control);
            UpdateText(name, initialText);
        }

        protected virtual void UpdateText(string name, string text)
        {
            static float CalculateTextLength(string t) => t.ToArray().Sum(s => char.IsAscii(s) ? 0.5f : 1);

            var lineCount = text.Count(s => s == '\n');
            var maxWordPerLine = text.Split('\n').Select(CalculateTextLength).Max();
            var width = (int)maxWordPerLine * 20;
            var height = lineCount * 60;

            if(controls.TryGetValue(name, out var node))
            {
                var inOptions = InputOptions.Count;
                var outOptions = OutputOptions.Count;
                var maxOptionsCount = inOptions > outOptions ? inOptions : outOptions;

                Size = new Size(width + 20, height + 20 + maxOptionsCount * 35);
                node.Text = text;
                node.Location = new Point(10, 25 * maxOptionsCount);
                node.Size = new Size(width, height);

            }
                
        }



        protected override void OnSaveNode(Dictionary<string, byte[]> dic)
        {
            dic.Add("Id", Id.ToBytes());
            base.OnSaveNode(dic);
        }

        protected override void OnLoadNode(Dictionary<string, byte[]> dic)
        {
            var id = Encoding.UTF8.GetString(dic["Id"]);
            Id = id;
            base.OnLoadNode(dic);
        }

    }

}
