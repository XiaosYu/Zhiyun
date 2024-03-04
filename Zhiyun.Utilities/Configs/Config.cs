#region 文件说明
/*----------------------------------------------------------------
// 文件名称：ConfigContext
// 创 建 者：暗夜游侠
// 创建时间：2023/2/11 0:41:51
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhiyun.Utilities.Utility.Extensions;

namespace Zhiyun.Utilities.Configs
{
    public static class Config
    {
        public static T LoadConfig<T>(string? path = null) where T : class, new()
        {
            var type = typeof(T);
            var attributes = Attribute.GetCustomAttributes(type);
            if (attributes.FirstOrDefault(s => s is ConfigModelAttribute) is ConfigModelAttribute attribute)
            {
                //获取位置
                if (path == null)
                {
                    if (attribute.Path == "")
                    {
                        path = type.Name.Replace("Setting", "").Replace("Config", "");
                    }
                    else
                    {
                        if (attribute.Name == "")
                        {
                            path = $"{attribute.Path}\\{type.Name.Replace("Setting", "").Replace("Config", "")}";
                        }
                        else
                        {
                            path = $"{attribute.Path}\\{attribute.Name}";
                        }
                    }
                    var text = File.ReadAllText(path.ToLower() + ".json");
                    var obj = text.ToObject<T>() ?? throw new NullReferenceException();
                    return obj;
                }
                else throw new FileNotFoundException();
            }
            else throw new TypeUnloadedException();
        }
        public async static Task<T> LoadConfigAsync<T>(string? path = null) where T : class, new()
        {
            var type = typeof(T);
            var attributes = Attribute.GetCustomAttributes(type);
            if (attributes.FirstOrDefault(s => s is ConfigModelAttribute) is ConfigModelAttribute attribute)
            {
                //获取位置
                if (path == null)
                {
                    if (attribute.Path == "")
                    {
                        path = type.Name.Replace("Setting", "").Replace("Config", "");
                    }
                    else
                    {
                        if (attribute.Name == "")
                        {
                            path = $"{attribute.Path}\\{type.Name.Replace("Setting", "").Replace("Config", "")}";
                        }
                        else
                        {
                            path = $"{attribute.Path}\\{attribute.Name}";
                        }
                    }
                    var text = await File.ReadAllTextAsync(path + ".json");
                    var obj = text.ToObject<T>() ?? throw new NullReferenceException();
                    return obj;
                }
                else throw new FileNotFoundException();
            }
            else throw new TypeUnloadedException();
        }
        public static void SaveConfig<T>(T config, string? path = null) where T : class, new()
        {
            var type = typeof(T);
            var attributes = Attribute.GetCustomAttributes(type);
            if (attributes.FirstOrDefault(s => s is ConfigModelAttribute) is ConfigModelAttribute attribute)
            {
                //获取位置
                if (path == null)
                {
                    if (attribute.Path == "")
                    {
                        path = type.Name.Replace("Setting", "").Replace("Config", "");
                    }
                    else
                    {
                        if (attribute.Name == "")
                        {
                            path = $"{attribute.Path}\\{type.Name.Replace("Setting", "").Replace("Config", "")}";
                        }
                        else
                        {
                            path = $"{attribute.Path}\\{attribute.Name}";
                        }
                    }
                    var text = config.ToJson();
                    File.WriteAllText(path + ".json", text);
                }
                else throw new FileNotFoundException();
            }
            else throw new TypeUnloadedException();
        }
        public static async Task SaveConfigAsync<T>(T config, string? path = null) where T : class, new()
        {
            var type = typeof(T);
            var attributes = Attribute.GetCustomAttributes(type);
            if (attributes.FirstOrDefault(s => s is ConfigModelAttribute) is ConfigModelAttribute attribute)
            {
                //获取位置
                if (path == null)
                {
                    if (attribute.Path == "")
                    {
                        path = type.Name.Replace("Setting", "").Replace("Config", "");
                    }
                    else
                    {
                        if (attribute.Name == "")
                        {
                            path = $"{attribute.Path}\\{type.Name.Replace("Setting", "").Replace("Config", "")}";
                        }
                        else
                        {
                            path = $"{attribute.Path}\\{attribute.Name}";
                        }
                    }
                    var text = config.ToJson();
                    await File.WriteAllTextAsync(path + ".json", text);
                }
                else throw new FileNotFoundException();
            }
            else throw new TypeUnloadedException();
        }
    }
}