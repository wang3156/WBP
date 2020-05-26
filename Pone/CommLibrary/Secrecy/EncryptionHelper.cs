using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommLibrary.Secrecy
{
    public class EncryptionHelper
    {
        #region DES加密

        const string DESKey = "DESKey";

        ///<summary>
        /// 使用默认密钥加密
        ///</summary>
        ///<param name="strText"></param>
        ///<returns></returns>
        public static string DESEncrypt(string strText)
        {
            try
            {
                return DESEncrypt(strText, DESKey);
            }
            catch
            {
                return "";
            }
        }

        ///<summary>
        /// 使用给定密钥加密
        ///</summary>
        ///<param name="strText"></param>
        ///<param name="sKey">密钥</param>
        ///<returns></returns>
        public static string DESEncrypt(string strText, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(strText);
            des.Key = ASCIIEncoding.ASCII.GetBytes(MD5Helper.GenerateMD5(sKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(MD5Helper.GenerateMD5(sKey).Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        #endregion

        #region DES解密

        ///<summary>
        /// 使用默认密钥解密
        ///</summary>
        ///<param name="strText"></param>
        ///<returns></returns>
        public static string DESDecrypt(string strText)
        {
            try
            {
                return DESDecrypt(strText, DESKey);
            }
            catch
            {
                return "";
            }
        }

        ///<summary>
        /// 使用给定密钥解密
        ///</summary>
        ///<param name="strText"></param>
        ///<param name="sKey"></param>
        ///<returns></returns>
        public static string DESDecrypt(string strText, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len = strText.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(strText.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(MD5Helper.GenerateMD5(sKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(MD5Helper.GenerateMD5(sKey).Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion


        #region AES方法
        const string AESIV = "1234567812345678";
        const string AESKEY = "AESKEYAESKEYAESKEYAESKEY";

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="strText">明文</param>
        /// <returns>密文</returns>
        public static string AESEncrypt(string strText)
        {
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(strText);
            RijndaelManaged rm = new RijndaelManaged();
            rm.Padding = PaddingMode.PKCS7;
            rm.Mode = CipherMode.CBC;
            rm.BlockSize = 128;
            rm.KeySize = 128;
            rm.Key = ASCIIEncoding.ASCII.GetBytes(AESKEY);
            rm.IV = ASCIIEncoding.ASCII.GetBytes(AESIV);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, rm.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="strText">密文</param>
        /// <returns>明文</returns>
        public static string AESDecrypt(string strText)
        {

            int len;
            len = strText.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(strText.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            RijndaelManaged rm = new RijndaelManaged();
            rm.Padding = PaddingMode.PKCS7;
            rm.Mode = CipherMode.CBC;
            rm.BlockSize = 128;
            rm.KeySize = 128;
            rm.Key = ASCIIEncoding.ASCII.GetBytes(AESKEY);
            rm.IV = ASCIIEncoding.ASCII.GetBytes(AESIV);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, rm.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
        #endregion
    }
}
