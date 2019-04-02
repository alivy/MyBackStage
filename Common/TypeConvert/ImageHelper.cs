/*************************************************************************************
* 代码:Bert
* 时间:2015.12.22
* 说明:图片帮助类
* 其他:
* 修改人：
* 修改时间：
* 修改说明：
************************************************************************************/
using System;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;
using System.IO;
using System.Drawing;

namespace Common
{
    public class ImageHelper
    {
        
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ImageHelper() { }

        /// <summary>
        /// 下载网络图片
        /// </summary>
        /// <param name="url">网络图片地址</param>
        /// <param name="dirPath">保存地址</param>
        /// <param name="fileName">图片名称(存在相同名称则覆盖原图片)</param>
        /// <returns>True:下载成功;falsh:下载失败</returns>
        public static bool DownImage(string url, string dirPath, string fileName)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);//图片src内容
                WebResponse response = request.GetResponse();
                //文件流获取图片操作
                Stream reader = response.GetResponseStream();
                string path = dirPath + fileName + ".png";        //图片路径命名 

                //不存在则创建目录
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(dirPath));
                }

                FileStream writer = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                byte[] buff = new byte[512];
                int c = 0;                                           //实际读取的字节数   
                while ((c = reader.Read(buff, 0, buff.Length)) > 0)
                {
                    writer.Write(buff, 0, c);
                }
                //释放资源
                writer.Close();
                writer.Dispose();
                reader.Close();
                reader.Dispose();
                response.Close();
                //下载成功
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}