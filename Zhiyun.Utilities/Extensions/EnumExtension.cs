using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Utilities.Extensions
{
    public static class EnumExtension
    {
        public static string ParseToString<TEnum>(this TEnum @enum) where TEnum : struct, Enum => Enum.GetName(@enum) ?? "UnKnown";
    }
}
