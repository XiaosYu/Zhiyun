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
using Zhiyun.Utilities.Extensions;

namespace Zhiyun.Nodes.Modules
{
    public abstract class CustomModule : Module
    {
        public CustomModule()
        {
            TitleColor = Color.FromArgb(200, Color.Pink);
            ModuleMessage.Monolithic.SettableParameters.ForEach(s =>
            {
                var a = this;
                var property = GetType().GetRuntimeProperty(s.Name);
                
                
                property?.SetValue(this, s.Type switch
                {
                    "Int32" => s.Value.ToString()!.ToInt32(),
                    "Boolean" => s.Value.ToString()!.ToBoolean(),
                    "String" => s.Value.ToString()!,
                    _ => throw new Exception("模块解析出错")
                });
            });

        }

        public ModuleMessage ModuleMessage => ModuleMessageText.ToObject<ModuleMessage>()!;
        public abstract string ModuleMessageText { get; }
        public override void OnFlushComponent()
        {
            SetInPortText(ModuleMessage.Monolithic.InputDimension);
            SetOutPortText(ModuleMessage.Monolithic.OutputDimension);
        }
        public override int ParametersNumber => ModuleMessage.Monolithic.LearnableParameters;
        protected override List<Dimension> GetInputDimensions() => [ModuleMessage.Monolithic.InputDimension];
        protected override List<Dimension> GetOutputDimensions() => [ModuleMessage.Monolithic.OutputDimension];
        protected override Dimension OutputDim => GetOutputDimensions()[0];

        protected override void OnReceivedMessagePart(ConnectionData data)
        {
            Flush();
        }

        public override ConnectionData OnSendMessage() => new() { Dimension = OutputDim.Clone() };

        public static Type CreateCustomModuleType(ModuleMessage module)
        {
            static Type ConvertType(string typeName)
                => typeName switch
                {
                    "Int32" => typeof(int),
                    "Boolean" => typeof(bool),
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
                FieldBuilder fieldBuilder = typeBuilder.DefineField(settableParameter.Name.ToLower(), parameterType, FieldAttributes.Public);
                PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(settableParameter.Name, PropertyAttributes.None, parameterType, null);

                ConstructorInfo attributeCtor = typeof(STNodePropertyAttribute).GetConstructor([typeof(string), typeof(string)])!;
                attributeBuilder = new(attributeCtor, [settableParameter.Name, settableParameter.Description]);
                propertyBuilder.SetCustomAttribute(attributeBuilder);

                ConstructorInfo propertyCtor = typeof(PropertyAttribute).GetConstructor([])!;
                attributeBuilder = new(propertyCtor, []);
                propertyBuilder.SetCustomAttribute(attributeBuilder);

                MethodBuilder getterBuilder = typeBuilder.DefineMethod($"get_{settableParameter.Name}", MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, parameterType, Type.EmptyTypes);
                ILGenerator getterIL = getterBuilder.GetILGenerator();
                getterIL.Emit(OpCodes.Ldarg_0);
                getterIL.Emit(OpCodes.Ldfld, fieldBuilder);
                getterIL.Emit(OpCodes.Ret);
                propertyBuilder.SetGetMethod(getterBuilder);

                MethodBuilder setterBuilder = typeBuilder.DefineMethod($"set_{settableParameter.Name}", MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, null, new Type[] { parameterType });
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
