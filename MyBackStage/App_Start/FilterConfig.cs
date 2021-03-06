﻿using MyBackStage.Controllers.Filter;
using System.Web;
using System.Web.Mvc;

namespace MyBackStage
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionHandleAttribute());
        }
    }
}
