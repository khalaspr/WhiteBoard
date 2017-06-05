using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiteBoardApplication.Models;

namespace WhiteBoardApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            using (var ctx = new ColWhiteBoardContext())
            {
                if (!string.IsNullOrEmpty(id)) 
                { 
                   LinkInformation obj =  ctx.LinkInformations.First(a => a.LinkID == id);
                   ViewBag.myscreenshot =  obj.LinkContent;
                }

            }
            return View();
        }

        [HttpPost]
        [MyErrorHandler]
        public JsonResult saveScreenShot(string myscreenshot) 
        {
            string URL = string.Empty;
            using (var ctx = new ColWhiteBoardContext())
            {
                try
                {
                    string sURLString = Utility.GenerateRandomString();
                    HttpRequestBase request = this.Request;
                    URL = request.Url.Scheme + "://" + request.Url.Authority + VirtualPathUtility.ToAbsolute("~/") + "?id=" + sURLString;

                    LinkInformation link = new LinkInformation
                    {
                        LinkID = sURLString,
                        LinkContent = myscreenshot
                    };
                    ctx.LinkInformations.Add(link);
                    ctx.SaveChanges();

                }
                catch (Exception Ex)
                {
                    ErrorLog errObj = new ErrorLog();
                    errObj.ErrorMessage =  Ex.Message;
                    ctx.ErrorLogs.Add(errObj);
                    ctx.SaveChanges();
                    Utility.WriteLogError(Ex.Message);
                    throw new Exception("Message: "+Ex.Message);
                }

            }
            return Json(new { success = true, urlString = URL });   
        }
    }
}