﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WhiteBoardApplication.Models
{
    public class MyErrorHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.Result = new JsonResult
            {
                Data = new { success = false, error = filterContext.Exception.ToString() },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}