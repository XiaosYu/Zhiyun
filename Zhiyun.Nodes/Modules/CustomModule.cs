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
using Zhiyun.Nodes.Interfaces;
using Zhiyun.Utilities.Extensions;

namespace Zhiyun.Nodes.Modules
{
    public abstract class CustomModule : Module
    {
        public CustomModule()
        {
            TitleColor = Color.FromArgb(200, Color.Pink);
        }

        public ICustomModuleContextStripLinker? ContextStripLinker { get; set; }

        public ModuleMessage ModuleMessage => ModuleMessageText.ToObject<ModuleMessage>()!;


        public abstract string ModuleMessageText { get; }


        public override void OnFlushComponent()
        {
            GetType()
                .GetProperties()
                .Where(s => s.GetCustomAttribute<PropertyAttribute>() != null)
                .Foreach(s =>
                {
                    var value = s.GetValue(this)!;
                    var propertyId = s.Name;
                    var parameter = ModuleMessage.Monolithic.SettableParameters.First(s=>s.Id == propertyId);

                    var targetNode = ContextStripLinker?.FindNode(s => s.Id == parameter.ParentID);
                    targetNode?.SetProperty(parameter.Name, value);
                });

            SetInPortText(InputDim);
            SetOutPortText(OutputDim);
        }
        
        public override int ParametersNumber => ContextStripLinker!.Modification.LearnableParameters;

        protected override Dimension CalculateOutputDim()
        {
            if(ContextStripLinker != null)
            {
                var outNode = ContextStripLinker.FindNode(s => s is Output);
                if (outNode != null && outNode is Output output)
                    return output.OutDim;
            }
            return Dimension.Empty;
        }

        protected override void OnInitializeProperty()
        {
            ModuleMessage.Monolithic.SettableParameters.ForEach(s =>
            {
                var a = this;
                var property = GetType().GetRuntimeProperty(s.Id);


                property?.SetValue(this, s.Type switch
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
            //当接收到新的输入进行重新计算输入与输出维度
            if(ContextStripLinker != null)
            {
                var inputNode = ContextStripLinker.FindNode(s => s.GetType().Name.Contains("Input"));
                if(inputNode is Input input)
                {
                    input.Modify(InputDim);
                    var outNode = ContextStripLinker.FindNode(s => s.GetType().Name.Contains("Output"));
                    if (outNode is Output output)
                    {
                        //更新参数，使参数对其
                        GetType()
                            .GetProperties()
                            .Where(s => s.GetCustomAttribute<PropertyAttribute>() != null)
                            .Foreach(s =>
                            {
                                var value = s.GetValue(this)!;
                                var propertyId = s.Name;
                                var parameter = ModuleMessage.Monolithic.SettableParameters.First(s => s.Id == propertyId);

                                var targetNode = ContextStripLinker?.FindNode(s => s.Id == parameter.ParentID);
                                var pv = targetNode?.GetProperty(parameter.Name);

                                s.SetValue(this, pv);
                            });
                    }
                }
            }
            Flush();
        }





        public static Type CreateCustomModuleType(ModuleMessage module)
        {
            static Type ConvertType(string typeName)
                => typeName switch
                {
                    "Int32" => typeof(int),
                    "Boolean" => typeof(bool),
                    "String" => typeof(string),
                    _ => (Type)Type.Missing,
                };

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
                var parameterType = ConvertType(settableParameter.Type)!;
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
