#region 文件说明
/*----------------------------------------------------------------
// 文件名称：IntExtension
// 创 建 者：暗夜游侠
// 创建时间：2023/2/17 23:10:43
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

namespace Zhiyun.Utilities.Extensions
{
    public static partial class Extension
    {
        /// <summary>
        /// 对action进行for循环count次
        /// </summary>
        /// <param name="count">循环次数</param>
        /// <param name="action">目标函数</param>
        static public void ForEach(this int count, Action<int> action)
        {
            for (int i = 0; i < count; ++i)
            {
                action(i);
            }
        }


        static public string ToFileSize(this int size)
        {
            var tmp = (double)size;
            // size <= 1000 -> Byte 0-300Byte
            if (tmp <= 1000) return $"{tmp}byte";
            // size <= 800 * 1024 -> kB 0.78-800kB
            if (tmp <= 800 * 1024) return $"{(tmp / 1024):F2}kB";
            // size <= 800 * 1024 * 1024 -> MB 0.78-800Mb
            if (tmp <= 800 * 1024 * 1024) return $"{(tmp / 1024 / 1024):F2}MB";
            // size <= 2 * 1024 * 1024 * 1024 -> GB 0.78-2Gb
            if (tmp <= 2147483648) return $"{(tmp / 1024 / 1024 / 1024):F2}GB";
            else return "Error Size";
        }
    }
}