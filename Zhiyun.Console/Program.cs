using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;
using Zhiyun.Nodes;
using Zhiyun.Utilities.Extensions;
var node = File.ReadAllText("data.json").ToObject<ModuleMessage>();
Console.WriteLine(node.Monolithic.Nodes[0].Parameters[0].Value.GetType().Name);
Console.WriteLine(node);