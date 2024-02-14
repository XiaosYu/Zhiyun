using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Utilities.Extensions
{
    public static class JTokenExtension
    {
        public static TResult? AsObject<TResult>(this JToken token) where TResult : class
        {
            if (typeof(TResult).Equals(typeof(string)))
                return token.ToObject<string>()?.Trim('\"') as TResult;
            else return token.ToObject<TResult>();
        }
    }
}
