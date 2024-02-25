using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Newtonsoft.Json.Linq;

namespace Zhiyun.Utilities.Extensions
{
	static public partial class Extension
	{
		static JsonSerializerOptions CommonSerializerOptions = new JsonSerializerOptions()
		{
			Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
		};

        /// <summary>
        /// 将数据对象解析为Json字符串
        /// </summary>
        /// <param name="obj">需要解析的对象</param>
        /// <returns>返回字符串</returns>
        static public string ToJson(this object obj)
			=> JsonSerializer.Serialize(obj, CommonSerializerOptions);

		/// <summary>
		/// 复制源对象的成员值到目标对象中
		/// </summary>
		/// <param name="source">源对象</param>
		/// <param name="target">目标对象</param>
		static public void CopyProperties(this object source, object target)
		{
			var sourceType = source.GetType();
			var targetType = target.GetType();
			var targetProperties = targetType.GetProperties();
			foreach(var type in sourceType.GetProperties()) 
			{
				var property = targetProperties.FirstOrDefault(s => s.Name.IsSimilar(type.Name) && s.PropertyType == type.PropertyType && s.CanWrite && type.CanRead);
				if (property != null) 
				{
					property.SetValue(target, type.GetValue(source));
				}
				
			}
		}



        /// <summary>
        /// 复制源对象的成员值到目标对象中
        /// </summary>
        /// <typeparam name="TEntity">目标类</typeparam>
        /// <param name="source">源对象</param>
        /// <returns>目标对象</returns>
        static public TEntity ToNewEntity<TEntity>(this object source) where TEntity : class, new()
		{
			var entity = new TEntity();
            CopyProperties(source, entity);
			return entity;
        }

		/// <summary>
		/// 将source中所有可读对象转换为数组,建议数据类使用
		/// </summary>
		/// <param name="source">数据类对象</param>
		/// <returns>数据数组</returns>
		static public object[] MapObject(this object source)
		{
			var type = source.GetType();
			List<object> list = new List<object>();
			foreach(var property in type.GetProperties()) 
			{
				if(property.CanRead)
				{
					var data = property.GetValue(source);
					if(data != null)
					{
                        list.Add(data);
                    }       
				}
			}
			return list.ToArray();
		}

        static public string ToHtmlUriParameters(this object data, bool isLower=true, params string[] exceptName)
        {
			var type = data.GetType();
			var properties = type.GetProperties();
			var selectProperties = properties.Where(s => !exceptName.Contains(s.Name));
			var keypairs = selectProperties.Select(s => $"{(isLower ? s.Name.ToLower() : s.Name)}={s.GetValue(data) ?? ""}");
			var result = keypairs.Synthesize("&");
			return result;
        }

		static public object? InvokeMethod(this object obj, string functionName, object?[]? args)
		{
			var function = obj.GetType().GetMethod(functionName);
			if (function != null)
			{
				return function.Invoke(obj, args);
			}
			else return null;
		}

		static public void SetProperty(this object obj, string propertyName, object value) 
		{
			var property = obj.GetType().GetProperty(propertyName);
			property?.SetValue(obj, value);
		}

		static public object? GetProperty(this object obj, string propertyName)
		{
            var property = obj.GetType().GetProperty(propertyName);
			return property?.GetValue(obj);
        }

		static public void SetField(this object obj, string fieldName, object value)
		{
			var field = obj.GetType().GetField(fieldName);
			field?.SetValue(obj, value);
		}
    }
}
