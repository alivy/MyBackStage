using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace Common
{



    /// <summary>
    /// DES����/�����ࡣ
    /// </summary>
    public class DESEncrypt
    {
        static PaddingMode[] padding = {
            PaddingMode.PKCS7,
            PaddingMode.ANSIX923,
            PaddingMode.ISO10126,
            PaddingMode.None,
            PaddingMode.Zeros
        };


        /// <summary>
        /// �Զ���ֵ
        /// </summary>
        //static string UL = System.Configuration.ConfigurationManager.AppSettings["EncryptString"];
        static string UL = "hao123cz";



        #region ========����========

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="Text">Ҫ���ܵ��ַ���</param>
        /// <returns>���ؼ��ܺ���ַ���</returns>
        public static string EncryptEncoding(string Text, string sKey = null)
        {
            if (sKey == null)
            {
                return Encrypt(Text, UL);
            }
            else
            {
                return Encrypt(Text, sKey);
            }
        }


        /// <summary>
        /// ���ַ�������DES����(IOSͨ�ã���׿�÷�)
        /// </summary>
        /// <param name="sourceString">�����ܵ��ַ���</param>
        /// <returns>���ܺ��BASE64������ַ���</returns>
        public static string Encrypt(string sourceString, string sKey)
        {
            byte[] btKey = Encoding.UTF8.GetBytes(sKey);
            byte[] btIV = Encoding.UTF8.GetBytes(sKey);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Encoding.UTF8.GetBytes(sourceString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// ����(IOSͨ�ã���׿APP�÷�)
        /// </summary>
        /// <param name="Message">�����ı�</param>
        /// <param name="key">��Կ</param>
        /// <param name="Mode">Ĭ��ѡECB</param>
        /// <param name="pad">����0</param>
        /// <returns></returns>
        public static string APPEncrypt(string Message, string key, CipherMode Mode, int pad)
        {
            try
            {

                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] keyIV = Encoding.UTF8.GetBytes(key);
                byte[] inputByteArray = Encoding.UTF8.GetBytes(Message);

                DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();

                // java Ĭ�ϵ���ECBģʽ��PKCS5padding��c#Ĭ�ϵ�CBCģʽ��PKCS7padding ������������Ĭ��ʹ��ECB��ʽ
                desProvider.Mode = Mode;
                desProvider.Padding = padding[pad];
                MemoryStream memStream = new MemoryStream();
                CryptoStream crypStream = new CryptoStream(memStream, desProvider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);

                crypStream.Write(inputByteArray, 0, inputByteArray.Length);
                crypStream.FlushFinalBlock();
                return Convert.ToBase64String(memStream.ToArray());
            }
            catch
            {
                return "����ʧ��";
            }
        }



        ///// <summary> 
        ///// ��������  ����C#
        ///// </summary> 
        ///// <param name="Text"></param> 
        ///// <param name="sKey"></param> 
        ///// <returns></returns> 
        //public static string Encrypt(string Text, string sKey)
        //{
        //    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //    byte[] inputByteArray;
        //    inputByteArray = Encoding.Default.GetBytes(Text);


        //    des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey,"md5").Substring(0, 8));
        //    des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
        //    System.IO.MemoryStream ms = new System.IO.MemoryStream();
        //    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        //    cs.Write(inputByteArray, 0, inputByteArray.Length);
        //    cs.FlushFinalBlock();
        //    StringBuilder ret = new StringBuilder();
        //    foreach (byte b in ms.ToArray())
        //    {
        //        ret.AppendFormat("{0:X2}", b);
        //    }
        //    return ret.ToString();
        //}






        #endregion



        #region ========����========


        /// <summary>
        /// ����(IOSͨ�ã���׿�÷�)
        /// </summary>
        /// <param name="pToDecrypt">Ҫ���ܵ���Base64</param>
        /// <param name="sKey">��Կ���ұ���Ϊ8λ</param>
        /// <returns>�ѽ��ܵ��ַ���</returns>
        public static string Decrypt(string pToDecrypt, string sKey)
        {
            try
            {
                //ת�������ַ�
                pToDecrypt = pToDecrypt.Replace("-", "+");
                pToDecrypt = pToDecrypt.Replace("_", "/");
                pToDecrypt = pToDecrypt.Replace("~", "=");
                byte[] inputByteArray = Convert.FromBase64String(pToDecrypt);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                    des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                    }
                    string str = Encoding.UTF8.GetString(ms.ToArray());
                    ms.Close();
                    return str;
                }
            }
            catch (Exception e)
            {
                return "";
            }
        }
       

        /// <summary>
        /// ����(IOSͨ�ã���׿APP�÷�)
        /// </summary>
        /// <param name="Message">�����ı�</param>
        /// <param name="key">��Կ</param>
        /// <param name="Mode">Ĭ��ѡECB</param>
        /// <param name="pad">����0</param>
        /// <returns></returns>
        public static string APPDecrypt(string Message, string key, CipherMode Mode, int pad)
        {
            try
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] keyIV = Encoding.UTF8.GetBytes(key);
                byte[] inputByteArray = Convert.FromBase64String(Message);

                DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();

                // java Ĭ�ϵ���ECBģʽ��PKCS5padding��c#Ĭ�ϵ�CBCģʽ��PKCS7padding ������������Ĭ��ʹ��ECB��ʽ
                desProvider.Mode = Mode;
                desProvider.Padding = padding[pad];
                MemoryStream memStream = new MemoryStream();
                CryptoStream crypStream = new CryptoStream(memStream, desProvider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);

                crypStream.Write(inputByteArray, 0, inputByteArray.Length);
                crypStream.FlushFinalBlock();
                return Encoding.Default.GetString(memStream.ToArray());
            }
            catch
            {
                return "����ʧ��";
            }
        }

        /// <summary>
        /// Des����
        /// </summary>
        /// <param name="source">Դ�ַ���</param>
        /// <param name="key">des��Կ�����ȱ���8λ</param>
        /// <param name="iv">��Կ����</param>
        /// <returns>���ܺ���ַ���</returns>
        public static string DecryptDes(string source, string key)
        {
            using (DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider())
            {
                byte[] rgbKeys = GetDesKey(key),
                    rgbIvs = null,
                    inputByteArray = Convert.FromBase64String(source);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, desProvider.CreateDecryptor(rgbKeys, rgbIvs), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                        cryptoStream.FlushFinalBlock();
                        return Encoding.UTF8.GetString(memoryStream.ToArray());
                    }
                }
            }
        }

        static byte[] GetDesKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "Des��Կ����Ϊ��");
            }
            if (key.Length > 8)
            {
                key = key.Substring(0, 8);
            }
            if (key.Length < 8)
            {
                // ����8��ȫ
                key = key.PadRight(8, '0');
            }
            return Encoding.UTF8.GetBytes(key);
        }


        public string DesDecrypt(string pToDecrypt, string sKey)
        {
            MemoryStream ms = new MemoryStream();

            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
                for (int x = 0; x < pToDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();

            }
            catch
            {

            }

            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }  


        /// <summary>
        /// ����
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string DecryptDecoding(string Text, string sKey = null)
        {
            //if (sKey == null)
            //{
            //    return APPDecrypt(Text, UL, CipherMode.ECB, 0);
            //}
            //else
            //{
            //    return APPDecrypt(Text, sKey, CipherMode.ECB,0);
            //}
            if (sKey == null)
            {
                return Decrypt(Text, UL);
            }
            else
            {
                return Decrypt(Text, sKey);
            }
        }




        ///// <summary> 
        ///// ��������  ����C#
        ///// </summary> 
        ///// <param name="Text"></param> 
        ///// <param name="sKey"></param> 
        ///// <returns></returns> 
        //public static string Decrypt(string Text, string sKey)
        //{
        //    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //    int len;
        //    len = Text.Length / 2;
        //    byte[] inputByteArray = new byte[len];
        //    int x, i;
        //    for (x = 0; x < len; x++)
        //    {
        //        i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
        //        inputByteArray[x] = (byte)i;
        //    }
        //    des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
        //    des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
        //    System.IO.MemoryStream ms = new System.IO.MemoryStream();
        //    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
        //    cs.Write(inputByteArray, 0, inputByteArray.Length);
        //    cs.FlushFinalBlock();
        //    return Encoding.Default.GetString(ms.ToArray());
        //}




        #endregion
    }
}
