using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public JsonResult AjaxMethod(string myscreenshot)
        {

            string sURLString = GenerateRandomString();
            HttpRequestBase  request = this.Request;
            string URL = request.Url.Scheme + "://" + request.Url.Authority + VirtualPathUtility.ToAbsolute("~/") + "?id=" + sURLString;
            
            LinkInformation link = new LinkInformation
            {
                LinkID = sURLString,
                LinkContent = myscreenshot
            };
            using (var ctx = new ColWhiteBoardContext())
            {
                try
                {
                    var linkInfoInDb = ctx.LinkInformations
                  .Where(c => c.LinkID == URL) // or whatever your key is
                  .SingleOrDefault();
                    if (linkInfoInDb == null)
                        ctx.LinkInformations.Add(link);

                    ctx.SaveChanges();

                }
                catch (Exception Ex)
                {

                    throw;
                }

            }

            return Json(URL);
        }

      
        public string GenerateRandomString() {
            string URL = "";
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            List<char> characters = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '-', '_' };

            // Create one instance of the Random  
            Random rand = new Random();
            // run the loop till I get a string of 10 characters  
            for (int i = 0; i < 11; i++)
            {
                // Get random numbers, to get either a character or a number...  
                int random = rand.Next(0, 3);
                if (random == 1)
                {
                    // use a number  
                    random = rand.Next(0, numbers.Count);
                    URL += numbers[random].ToString();
                }
                else
                {
                    // Use a character  
                    random = rand.Next(0, characters.Count);
                    URL += characters[random].ToString();
                }
            }

            return URL;
        }

      
    }
}