using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SmartRental.Controllers
{
   
    public class HomesController : ApiController
    {
        [HttpGet]
        public object Main1()
        {

            return Json("123");
        }

    }
}
