using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Common
{
    public static class HtmlHelperExtends
    {
        public static HtmlString EsTextBox(this HtmlHelper helper,string name,object value)
        {
            return helper.TextBox(name, value, new { @class = "easyui-validatebox", data_options = "required:true,missingMessage:'该字段是必须的'" });
        }

    }
}
