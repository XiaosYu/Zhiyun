using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Utilities.Extensions
{
    static public partial class Extension
    {
        /// <summary>
        /// 计算目标字节数组的MD5值
        /// </summary>
        /// <param name="data">需要计算的字节数组</param>
        /// <returns>MD5</returns>
        static public byte[] ToMD5(this byte[] data)
            => MD5.HashData(data);

        /// <summary>
        /// 顺序将对象数组映射到实体
        /// </summary>
        /// <typeparam name="TEntity">需要映射的实体,需要有默认构造器</typeparam>
        /// <param name="sources">需要映射的实体</param>
        /// <returns>映射实体</returns>
        static public TEntity MapEntity<TEntity>(this object[] sources) where TEntity: class, new()
        {
            var type = typeof(TEntity);
            var properties = type.GetProperties();

            var entity = new TEntity();
            foreach(var (property, source) in properties.Zip(sources)) 
            {
                if(property.CanWrite && property.PropertyType == source.GetType())
                {
                    property.SetValue(entity, source, null);
                }
                else
                {
                    try
                    {
                        var data = Convert.ChangeType(source, property.PropertyType);
                        property.SetValue(entity, data, null);
                    }catch 
                    {
                        continue;
                    }
                }
            }
            return entity;
           
        }

        /// <summary>
        /// 复制数组source中的元素rate份并返回结果
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">源数据</param>
        /// <param name="rate">复制倍率</param>
        /// <returns>结果</returns>
        static public T[] Clone<T>(this T[] source, int rate)
        {
            var result = new T[source.Length * rate];
            for(int i=0;i<rate;++i)
            {
                source.CopyTo(result, 0 + source.Length * i);
            }
            return result;
        }

        /// <summary>
        /// 将data中的数据以文件的形式写入path
        /// </summary>
        /// <param name="data">写入数据</param>
        /// <param name="path">写入路径</param>
        /// <returns>等待对象</returns>
        static public async Task WriteToFile(this byte[] data, string path)
        {
            var file = File.Create(path);
            await file.WriteAsync(data);
            await file.DisposeAsync();
        }

        static public string ToBase64String(this byte[] data)
            => Convert.ToBase64String(data);
        static public int IndexOf<T>(this T[] source, T value)where T:notnull
        {
            int index = 0;
            foreach (T item in source) 
            {
                if (item.Equals(value))
                    return index;
                else index++;
            }
            throw new ObjectNotFoundException();
        }
    }
}
