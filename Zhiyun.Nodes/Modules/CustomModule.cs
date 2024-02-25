using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Zhiyun.Nodes;
using Zhiyun.Nodes.Services;
using Zhiyun.Utilities.Extensions;

namespace Zhiyun.Nodes.Modules
{
    [STNode("网络模块/自定义模型")]
    public abstract class CustomModule : Module
    {
        public CustomModule()
        {
            TitleColor = Color.FromArgb(200, Color.Pink);
        }

        private NodeSandbox Sandbox;
        private List<ParameterData> SettableParameters;

        public byte[] GetBytes() => Sandbox.GetCanvasData();
        public STNodeEditor GetNodeEditor() => Sandbox.GetNodeEditor();

        public abstract string ModuleMessageText { get; }

        public void ShowDetailWindow() => Sandbox.ShowDialog();

        public override NodeData GetNodeData()
        {
            var parameter = new ParameterDataCollection(GetType()
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
                                .ToList())
            {
                new ParameterData()
                {
                    Id = $"{Random.Shared.RandLower(1)}{Random.Shared.RandString(5)}",
                    Settable = false,
                    Name = "Monolithic",
                    Type = "Monolithic",
                    Value = Sandbox.GetMonolithic(),
                    ParentID = Id,
                    ParentType = GetType().Name
                }
            };
            

            return new NodeData
            {
                Name = Name,
                Type = "CustomModule",
                Parameters = parameter,
                Id = Id,
                Connected = ChildNodes.Select(s => s.Id).ToList(),
                LearnableParameters = GetType().GetProperty("ParametersNumber") != null ? (int)GetType().GetProperty("ParametersNumber")!.GetValue(this)! : 0
            };
        }

        public override void OnFlushComponent()
        {
            GetType()
                .GetProperties()
                .Where(s => s.GetCustomAttribute<PropertyAttribute>() != null)
                .Foreach(s =>
                {
                    var value = s.GetValue(this)!;
                    var propertyId = s.Name;
                    var parameter = SettableParameters.First(s=>s.Id == propertyId);

                    Sandbox.SetNodeProperty(parameter.ParentID, parameter.Name, value);
                });

            SetInPortText(InputDim);
            SetOutPortText(OutputDim);
        }

        public override int ParametersNumber => (int)Sandbox.GetParametersNumber();

        protected override Dimension CalculateOutputDim()
        {
            if (InputDim.OnlyBatch) return Dimension.Empty;
            return Sandbox.Input(InputDim);
        }

        protected override void OnInitializeProperty()
        {
            var moduleMessage = ModuleMessageText.ToObject<ModuleMessage>()!;
            Sandbox = new(moduleMessage.Graphs.FromBase64String());
            SettableParameters = moduleMessage.Monolithic.SettableParameters;

            SettableParameters.ForEach(s =>
            {
                var field = GetType().GetField(s.Id.ToLower());
                field?.SetValue(this, s.Type switch
                {
                    "Int32" => s.Value.ToString()!.ToInt32(),
                    "Boolean" => s.Value.ToString()!.ToBoolean(),
                    "String" => s.Value.ToString()!,
                    _ => throw new Exception($"模块解析出错,在初始化自定义模块参数的时候，无法解析将参数字符串{s.Type}转换为对应的Type类型")
                });
            });

        }

        protected override void OnReceivedMessagePart(ConnectionData data)
        {
            Sandbox.Input(data.Dimension);
            GetType()
               .GetProperties()
               .Where(s => s.GetCustomAttribute<PropertyAttribute>() != null)
               .Foreach(s =>
               {
                   var propertyName = s.Name;
                   var field = GetType().GetField(s.Name.ToLower());

                   var parameter = SettableParameters.First(s => s.Id == propertyName);

                   var nodeValue = Sandbox.GetNodeProperty(parameter.ParentID, parameter.Name);
                   field?.SetValue(this, nodeValue);
               });
        }

        public static Type CreateCustomModuleType(ModuleMessage module)
        {
            AssemblyName assemblyName = new("DynamicAssembly");
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicModule");

            TypeBuilder typeBuilder = moduleBuilder.DefineType(module.Name, TypeAttributes.Public, typeof(CustomModule));

            ConstructorInfo attributeInfo = typeof(NameAttribute).GetConstructor([typeof(string)])!;
            CustomAttributeBuilder attributeBuilder = new(attributeInfo, [module.Name]);
            typeBuilder.SetCustomAttribute(attributeBuilder);

            PropertyInfo parentProperty = typeof(CustomModule).GetProperty("ModuleMessageText")!;
            MethodBuilder getMethodBuilder = typeBuilder.DefineMethod("get_MyProperty", MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.SpecialName | MethodAttributes.HideBySig, typeof(string), Type.EmptyTypes);
            ILGenerator ilGenerator = getMethodBuilder.GetILGenerator();

            LocalBuilder localVar = ilGenerator.DeclareLocal(typeof(string)); 

            ilGenerator.Emit(OpCodes.Ldstr, module.ToJson()); 
            ilGenerator.Emit(OpCodes.Stloc, localVar); 
            ilGenerator.Emit(OpCodes.Ldloc, localVar); 
            ilGenerator.Emit(OpCodes.Ret);
            typeBuilder.DefineMethodOverride(getMethodBuilder, parentProperty.GetGetMethod()!);

            var monolithicNode = module.Monolithic;
            var settableParameters = monolithicNode.SettableParameters;
            foreach(var settableParameter in settableParameters)
            {
                var parameterType = settableParameter.Type.ToType();
                FieldBuilder fieldBuilder = typeBuilder.DefineField(settableParameter.Id.ToLower(), parameterType, FieldAttributes.Public);
                PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(settableParameter.Id, PropertyAttributes.None, parameterType, null);

                ConstructorInfo attributeCtor = typeof(STNodePropertyAttribute).GetConstructor([typeof(string), typeof(string)])!;
                attributeBuilder = new(attributeCtor, [settableParameter.Description, settableParameter.Description]);
                propertyBuilder.SetCustomAttribute(attributeBuilder);

                ConstructorInfo propertyCtor = typeof(PropertyAttribute).GetConstructor([])!;
                attributeBuilder = new(propertyCtor, []);
                propertyBuilder.SetCustomAttribute(attributeBuilder);

                MethodBuilder getterBuilder = typeBuilder.DefineMethod($"get_{settableParameter.Id}", MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, parameterType, Type.EmptyTypes);
                ILGenerator getterIL = getterBuilder.GetILGenerator();
                getterIL.Emit(OpCodes.Ldarg_0);
                getterIL.Emit(OpCodes.Ldfld, fieldBuilder);
                getterIL.Emit(OpCodes.Ret);
                propertyBuilder.SetGetMethod(getterBuilder);

                MethodBuilder setterBuilder = typeBuilder.DefineMethod($"set_{settableParameter.Id}", MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, null, new Type[] { parameterType });
                ILGenerator setterIL = setterBuilder.GetILGenerator();
                setterIL.Emit(OpCodes.Ldarg_0);
                setterIL.Emit(OpCodes.Ldarg_1);
                setterIL.Emit(OpCodes.Stfld, fieldBuilder);
                setterIL.Emit(OpCodes.Ldarg_0);
                setterIL.Emit(OpCodes.Call, typeof(CustomModule).GetMethod("Flush")!);
                setterIL.Emit(OpCodes.Ret);
                propertyBuilder.SetSetMethod(setterBuilder);
            }
            
            Type dynamicType = typeBuilder.CreateType();

            return dynamicType;

        }
    }



}
