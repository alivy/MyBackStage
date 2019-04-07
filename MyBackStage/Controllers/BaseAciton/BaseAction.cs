using MyBackStage.Controllers.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBackStage.Controllers
{
    [HttpsToHttp]
    public class BaseAction : Controller
    {
        internal RequestActionResult<T> RequestAction<T>(T t) where T : RequestResult
        {
            return new RequestActionResult<T>(t);
        }
    }
}