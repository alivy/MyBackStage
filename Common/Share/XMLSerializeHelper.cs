using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Common
{
    public static class XMLSerializeHelper
    {
        /// <summary>
        /// XML的编码方式,默认是UTF-8
        /// </summary>
        public static Encoding xmlEncode;
        /// <summary>
        /// 静态的构造函数,初始化此类
        /// </summary>
        static XMLSerializeHelper()
        {
            xmlEncode = Encoding.UTF8;
        }

        /// <summary>
        /// 将对象序列化为XML写入到流中
        /// </summary>
        /// <param name="stream">要写入的流</param>
        /// <param name="obj">被写入的对象</param>
        static void XmlSerializeInternal(Stream stream, Object obj)
        {
            //创建序列化对象,只有public的class才可以进行xml序列化
            XmlSerializer serialize = new XmlSerializer(obj.GetType());
            //创建写入对象
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;/*表示缩进*/
            settings.IndentChars = " ";/*表示缩进的距离为一个Tab*/
            settings.NewLineChars = "\r\n";/*换行符,Window下换行符*/
            settings.Encoding = xmlEncode;/*编码方式*/
            //开始正式写入
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                serialize.Serialize(writer, obj);
            }
        }

        /// <summary>
        /// 将对象序列化为xml并返回xml字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>序列化后的xml字符串</returns>
        public static String XmlSerializeFromString(Object obj)
        {
            //创建内存流对象
            using (MemoryStream stream = new MemoryStream())
            {
                //将对象以xml形式写入到内存流中
                XmlSerializeInternal(stream, obj);
                stream.Position = 0;/*设置流的的位置在开始处,以便我们可以从头读取,和stream.Seek(0, SeekOrigin.Begin);一样*/
                using (StreamReader reader = new StreamReader(stream, xmlEncode))
                {
                    return reader.ReadToEnd();/*代码很花哨,几乎都是语法糖*/
                }
            }
        }

        /// <summary>
        /// 将对象按照xml序列化写入到文件中
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="obj">写入的对象</param>
        public static void XmlSerializeToFile(String path, Object obj)
        {
            //创建文件写入流对象
            using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                //写入将对象转成xml流的对象
                XmlSerializeInternal(file, obj);
            }
        }

        /// <summary>
        /// 将xml字符串转成对象
        /// </summary>
        /// <typeparam name="T">要转换的对象</typeparam>
        /// <param name="xmlStr">xml字符串</param>
        /// <returns>返回转换的对象</returns>
        public static T XmlDeserializeFromObject<T>(String xmlStr)
        {
            //以前刚学泛型时,就是感觉泛型好流弊,就是不明白这个词是什么意思.后来觉得模板更加贴切
            XmlSerializer serialize = new XmlSerializer(typeof(T));
            //创建内存流并进行转换
            using (MemoryStream stream = new MemoryStream(xmlEncode.GetBytes(xmlStr)))
            {
                //采用StreamReader是为了编码
                using (StreamReader reader = new StreamReader(stream, xmlEncode))
                {
                    return (T)serialize.Deserialize(reader);
                }
            }
        }

        /// <summary>
        /// 将xml文件序列化为对象
        /// </summary>
        /// <typeparam name="T">泛型,待指定的类型</typeparam>
        /// <param name="path">文件绝对全路径字符串</param>
        /// <returns>返回对象</returns>
        public static T XmlDeserializeFromFile<T>(String path)
        {
            //创建xml反序列化对象
            XmlSerializer serialize = new XmlSerializer(typeof(T));
            //获取xml文件流,并反序列化转成对象并返回
            using (StreamReader reader = new StreamReader(path, xmlEncode))
            {
                return (T)serialize.Deserialize(reader);
            }
        }
    }
}