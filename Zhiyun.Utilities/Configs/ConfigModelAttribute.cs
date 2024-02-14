#region 文件说明
/*----------------------------------------------------------------
// 文件名称：ConfigAttribute
// 创 建 者：暗夜游侠
// 创建时间：2023/2/11 0:38:04
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Utilities.Configs
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigModelAttribute : Attribute
    {
        public string Path { get; set; } = "";
        public string Name { get; set; } = "";
        public ConfigModelAttribute(string path = "", string name = "")
        {
            Path = path;
            Name = name;
        }
        public ConfigModelAttribute() { }
    }
}