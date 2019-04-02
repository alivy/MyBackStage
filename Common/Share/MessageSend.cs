using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;


namespace Common
{
    /// <summary>
    /// 短信发送
    /// </summary>
    public class MessageSend
    {
        public static string sendmsg(string tel, string nr, int type = 2)
        {
            //String str = HttpUtility.UrlEncode(nr, System.Text.Encoding.GetEncoding("utf-8"));
            //String strUrl = "http://119.145.9.12/sendSMS.action?enterpriseID=10008&loginName=admin&password=95c21d9ad5058270ae4534ca22f38c1a&smsId=&subPort=&content=" + nr + "&mobiles=" + tel + "&subPort=";
            //WebRequest wRequest = WebRequest.Create(strUrl);
            //WebResponse wResponse = wRequest.GetResponse();
            //Stream stream = wResponse.GetResponseStream();
            //StreamReader reader = new StreamReader(stream, System.Text.Encoding.Default);
            //string r = reader.ReadToEnd();
            //wResponse.Close();
            //Logger.WirteMessageLog(nr+"["+tel+"]");

            //return Post("http://119.145.9.12/sendSMS.action", "enterpriseID=15794&loginName=admin&password=207fa7cc137eeda04095c8317a9ad272&mobiles=" + tel + "&content=" + nr + "");

            string str = string.Empty;


            string regExpSymbol = @"^[！|!|。|.|]";                   //需过滤的末尾符号正则

            //if (nr.Substring(nr.Length - 1, nr.Length) == "。" || nr.Substring(nr.Length - 1, nr.Length) == ".")
            if (nr.Length > 1 && System.Text.RegularExpressions.Regex.IsMatch(nr.Substring(nr.Length - 1, 1), regExpSymbol))
            {
                nr = nr.Substring(0, nr.Length - 1);
            }

            str = LaiPost("http://120.24.161.220:8800/SMS/Send", "account=54CEC78EE932477FBE042214477FE21F&token=bd1a125defd94dfd8085e1bbe8515e87&tempid=384&mobile=" + tel + "&type=0&content=【钱富通】" + nr + "。");

            //var reslut=JsonConvert.DeserializeObject<VM_SendResult>
            return str;
        }



        /// <summary>
        /// LaiPost提交
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="postvalues">参数值(enterpriseID=1111&loginName=admin&password=admin&mobiles=13800000000&content=验证码)</param>
        /// <returns></returns>
        public static string LaiPost(string url, string postvalues)
        {
            string stringResponse = string.Empty;
            try
            {
                Console.WriteLine("post--url:" + url);
                Console.WriteLine("post--data:" + postvalues);
                byte[] data = Encoding.GetEncoding("UTF-8").GetBytes(postvalues);
                WebRequest webRequest = WebRequest.Create(url);
                HttpWebRequest httpRequest = webRequest as HttpWebRequest;
                HttpWebResponse response;
                // httpRequest.UserAgent = "Mozilla/4.0(compatible;MSIE 7.0;Windows NT 5.2;.NET CLR 1.1.4322;.NET CLR 2.0.50727)";
                httpRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                // httpRequest.Accept = "*/*";
                httpRequest.Method = "POST";
                httpRequest.ContentLength = data.Length;
                httpRequest.KeepAlive = false;
                httpRequest.AllowAutoRedirect = false;
                httpRequest.Timeout = 5000;
                httpRequest.ReadWriteTimeout = 5000;
                httpRequest.ServicePoint.Expect100Continue = false;//关闭Expect:100-Continue握手                 
                httpRequest.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                using (Stream requestStream = httpRequest.GetRequestStream())
                {
                    requestStream.Write(data, 0, data.Length);
                    response = (HttpWebResponse)httpRequest.GetResponse();
                    requestStream.Flush();
                    requestStream.Close();
                }
                //读取服务器返回信息
                using (Stream responseStream = response.GetResponseStream())
                {

                    using (StreamReader responseReader = new StreamReader(responseStream, Encoding.GetEncoding("UTF-8")))
                    {
                        stringResponse = responseReader.ReadToEnd();
                        // responseStream.Close();
                        response.Close();
                        httpRequest.Abort();

                    }
                }
            }
            catch (Exception ex)
            {
                stringResponse = ex.Message;
            }
            return stringResponse;
        }




        /// <summary>
        /// post提交
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="postvalues">参数值(enterpriseID=1111&loginName=admin&password=admin&mobiles=13800000000&content=验证码)</param>
        /// <returns></returns>
        public static string Post(string url, string postvalues)
        {
            string stringResponse = string.Empty;
            try
            {
                Console.WriteLine("post--url:" + url);
                Console.WriteLine("post--data:" + postvalues);
                byte[] data = Encoding.GetEncoding("UTF-8").GetBytes(postvalues);
                WebRequest webRequest = WebRequest.Create(url);
                HttpWebRequest httpRequest = webRequest as HttpWebRequest;
                HttpWebResponse response;
                // httpRequest.UserAgent = "Mozilla/4.0(compatible;MSIE 7.0;Windows NT 5.2;.NET CLR 1.1.4322;.NET CLR 2.0.50727)";
                httpRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                // httpRequest.Accept = "*/*";
                httpRequest.Method = "POST";
                httpRequest.ContentLength = data.Length;
                httpRequest.KeepAlive = false;
                httpRequest.AllowAutoRedirect = false;
                httpRequest.Timeout = 5000;
                httpRequest.ReadWriteTimeout = 5000;
                httpRequest.ServicePoint.Expect100Continue = false;//关闭Expect:100-Continue握手                 
                httpRequest.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                using (Stream requestStream = httpRequest.GetRequestStream())
                {
                    requestStream.Write(data, 0, data.Length);
                    response = (HttpWebResponse)httpRequest.GetResponse();
                    requestStream.Flush();
                    requestStream.Close();
                }
                //读取服务器返回信息
                using (Stream responseStream = response.GetResponseStream())
                {

                    using (StreamReader responseReader = new StreamReader(responseStream, Encoding.GetEncoding("UTF-8")))
                    {
                        stringResponse = responseReader.ReadToEnd();
                        // responseStream.Close();
                        response.Close();
                        httpRequest.Abort();

                    }
                }
            }
            catch (Exception ex)
            {
                stringResponse = ex.Message;
            }
            return stringResponse;
        }

    }
}
