using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;


namespace SmartRental.Controllers
{
    //[EnableCors("*", " *", "*")]
    public class HomeController :Controller
    {
        public ActionResult Main()
        {
            return View();
        }

      
        public ActionResult Minor()
        {
            return View();
        }
    }
}
