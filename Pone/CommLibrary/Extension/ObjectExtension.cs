using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommLibrary.Extension
{
    /// <summary>
    /// 应用到Object的一些扩展
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 检查字符串的object是否为null或空
        /// </summary>
        /// <param name="t">需要检查的object字符串对象</param>
        /// <returns>为null或空则为true</returns>
        public static bool Str_IsNullOrWhiteSpace(this object t)
        {
            return string.IsNullOrWhiteSpace(t?.ToString());
        }
    }
}
