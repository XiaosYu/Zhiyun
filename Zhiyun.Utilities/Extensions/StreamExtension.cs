#region 文件说明
/*----------------------------------------------------------------
// 文件名称：StreamExtension
// 创 建 者：暗夜游侠
// 创建时间：2023/1/21 10:16:06
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

namespace Zhiyun.Utilities.Utility.Extensions
{
    public static partial class Extension
    {
        public static byte[] ReadBytes(this Stream stream)
        {
            if(stream.Length > 10 * 1024 * 1024)
            {
                throw new LargeFileException();
            }
            else
            {
                var buffer = new byte[stream.Length];
                stream.Read(buffer);
                return buffer;
            }
        }

        public static async Task<byte[]> ReadBytesAsync(this Stream stream)
        {
            if (stream.Length > 50 * 1024 * 1024)
            {
                throw new LargeFileException();
            }
            else
            {
                var buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer);
                return buffer;
            }
        }
    }
}