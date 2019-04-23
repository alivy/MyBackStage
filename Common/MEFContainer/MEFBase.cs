using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MEFBase
    {
        /// <summary>
        /// 实现注入
        /// 此方法重点优化
        /// </summary>
        public static void Compose<T>(T t)
        {
            ////获取bin文件程序集元素
            string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var catalog = new DirectoryCatalog(path);
            //获取当前执行的程序集中所有的标有特性标签的代码段  
            //var catalog = new AssemblyCatalog(Assembly.GetEntryAssembly());
            // var ss =   (from s in typeof(Export).Assembly.GetExportedTypes() select t).ToDictionary();
            //将所有Export特性标签存放进组件容器中（其实是一个数组里面）  
            CompositionContainer container = new CompositionContainer(catalog);
            //找到所传入对象中所有拥有Import特性标签的属性，并在组件容器的数组中找到与这些属性匹配的Export特性标签所标注的类，然后进行实例化并给这些属性赋值。  
            //简而言之，就是找到与Import对应的Export所标注的类，并用这个类的实例来给Import所标注的属性赋值，用于解耦。  
            container.ComposeParts(t);
        }


        public static void InitControllers<T>(T t)
        {
            var catalog = new AssemblyCatalog(Assembly.GetEntryAssembly());
            var ss = (from s in typeof(Export).Assembly.GetExportedTypes() select t).ToList();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        }

    }


    public class PlugerBase<T>
    {
        public T GetClass(string className)
        {
            if (Names.Contains(className))
            {
                var plug = DoPluginList.Where(i => i.Metadata.Mark == className).Select(p => p.Value).FirstOrDefault();
                return (T)plug;
            }
            else
            {
                return default(T);
            }
        }
        public string[] Names
        {
            get
            {
                List<string> name = new List<string>();
                foreach (var item in DoPluginList)
                {
                    name.Add(item.Metadata.Mark);
                }
                return name.ToArray();
            }
        }
        /// <summary>
        /// 插件列表
        /// </summary>
        [ImportMany]
        private List<Lazy<T, IPluginMark>> DoPluginList = new List<Lazy<T, IPluginMark>>();
        public PlugerBase(string subFolderName = "Plug")
        {
            Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + subFolderName);
            var catelog = new AggregateCatalog();
            AssemblyCatalog assemblyCataLog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            catelog.Catalogs.Add(new DirectoryCatalog(subFolderName));
            var container = new CompositionContainer(catelog);
            container.ComposeParts(this);
        }
    }


    public interface IPlugin
    {
        string Name { get; set; }
        void SayHello();
    }
    public interface IPluginMark
    {
        string Mark { get; }
    }


    public enum AssemblyType
    {
        ApplicationBase = 1,

        GetEntryAssembly = 2

    }
}
