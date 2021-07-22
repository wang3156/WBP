using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommLibrary.Extension
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 检查string的集合中是否包含指定字符v 
        /// <!--三个符号合为一个则为保留符. >↑<   -->
        /// </summary>
        /// <param name="list">集合本身</param>
        /// <param name="word">需要检查的值</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public static bool Contains(this IEnumerable<string> list, string word, bool ignoreCase)
        {
            string data = "";
            IEnumerable<string> colist = list;
            if (ignoreCase)
            {
                data = string.Join(">↑<", list).ToUpper();
                colist = data.Split(new string[] { ">↑<" }, StringSplitOptions.None);
                word = word.ToUpper();
            }
            return colist.Contains(word);
        }

        /// <summary>
        /// 检查string的集合中是否包含指定字符v 
        /// <!--三个符号合为一个则为保留符. >↑<-->
        /// </summary>
        /// <param name="str">字串自己</param>
        /// <param name="word">需要检查的值</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public static bool Contains(this string str, string word, bool ignoreCase)
        {
            if (ignoreCase)
            {
                str = str.ToUpper();
                word = word.ToUpper();
            }
            return str.Contains(word);
        }

        /// <summary>
        /// 使用GZip压缩字符串 返回一个压缩后的base64字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns>压缩后的base64字符串</returns>
        public static string CompressString(this string str)
        {
            var compressBeforeByte = Encoding.GetEncoding("UTF-8").GetBytes(str);
            var compressAfterByte = Compress(compressBeforeByte);
            string compressString = Convert.ToBase64String(compressAfterByte);
            return compressString;
        }
        /// <summary>
        /// 解压以上面方式压缩过的字符串
        /// </summary>
        /// <param name="str">压缩生成的base64的字符串</param>
        /// <returns></returns>
        public static string DecompressString(this string str)
        {
            var compressBeforeByte = Convert.FromBase64String(str);
            var compressAfterByte = Decompress(compressBeforeByte);
            string compressString = Encoding.GetEncoding("UTF-8").GetString(compressAfterByte);
            return compressString;
        }

        /// <summary>
        /// Compress
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] Compress(byte[] data)
        {
            try
            {
                var ms = new MemoryStream();
                var zip = new GZipStream(ms, CompressionMode.Compress, true);
                zip.Write(data, 0, data.Length);
                zip.Close();
                var buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);
                ms.Close();
                return buffer;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Decompress
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] Decompress(byte[] data)
        {
            try
            {
                var ms = new MemoryStream(data);
                var zip = new GZipStream(ms, CompressionMode.Decompress, true);
                var msreader = new MemoryStream();
                var buffer = new byte[0x1000];
                while (true)
                {
                    var reader = zip.Read(buffer, 0, buffer.Length);
                    if (reader <= 0)
                    {
                        break;
                    }
                    msreader.Write(buffer, 0, reader);
                }
                zip.Close();
                ms.Close();
                msreader.Position = 0;
                buffer = msreader.ToArray();
                msreader.Close();
                return buffer;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
