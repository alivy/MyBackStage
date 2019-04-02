using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
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
            //获取bin文件程序集元素
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

    }

    public enum AssemblyType
    {
        ApplicationBase = 1,

        GetEntryAssembly = 2

    }
}
