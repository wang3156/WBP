using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommLibrary.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// 检查string的集合中是否包含指定字符v 
        ///    >↑<  三个符号合为一个则为保留符.
        /// </summary>
        /// <param name="list">集合本身</param>
        /// <param name="v">需要检查的值</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public static bool Contains(this IEnumerable<string> list, string v, bool ignoreCase)
        {
            string data = "";
            IEnumerable<string> colist = list;
            if (ignoreCase)
            {
                data = string.Join(">↑<", list).ToUpper();
                colist = data.Split(new string[] { ">↑<" }, StringSplitOptions.None);
                v = v.ToUpper();
            }
            return colist.Contains(v);
        }
    }
}
