using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookGallery.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ContentResult Index()
        {
            return new ContentResult()
            {
                Content ="hej igen fårn content result"
            };
        }
    }
}