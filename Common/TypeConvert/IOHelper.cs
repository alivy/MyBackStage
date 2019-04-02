using System;
using System.Xml;
using System.Web;
using System.Collections;
using System.Text;
using System.Data;
using System.IO;
using System.Xml.Serialization;

namespace Common
{
   /**//// <summary>
   /// ������XPATH���ʽ����ȡ��Ӧ�ڵ�
    /// ����xpath���Բμ�:
    /// </summary>
     public class IOHelper
     {
         /// <summary>
         /// ����ļ�����·��
         /// </summary>
         /// <returns></returns>
         public static string GetMapPath(string path)
         {
             if (HttpContext.Current != null)
             {
                 return HttpContext.Current.Server.MapPath(path);
             }
             else
             {
                 return System.Web.Hosting.HostingEnvironment.MapPath(path);
             }
         }

         #region  ���л�

         /// <summary>
         /// XML���л�
         /// </summary>
         /// <param name="obj">���ж���</param>
         /// <param name="filePath">XML�ļ�·��</param>
         /// <returns>�Ƿ�ɹ�</returns>
         public static bool SerializeToXml(object obj, string filePath)
         {
             bool result = false;

             FileStream fs = null;
             try
             {
                 fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                 XmlSerializer serializer = new XmlSerializer(obj.GetType());
                 serializer.Serialize(fs, obj);
                 result = true;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 if (fs != null)
                     fs.Close();
             }
             return result;

         }

         /// <summary>
         /// XML�����л�
         /// </summary>
         /// <param name="type">Ŀ������(Type����)</param>
         /// <param name="filePath">XML�ļ�·��</param>
         /// <returns>���ж���</returns>
         public static object DeserializeFromXML(Type type, string filePath)
         {
             FileStream fs = null;
             try
             {
                 fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                 XmlSerializer serializer = new XmlSerializer(type);
                 return serializer.Deserialize(fs);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 if (fs != null)
                     fs.Close();
             }
         }

         #endregion
 
     }
 
 }

