using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SmartRental.Controllers.API
{
    [EnableCors(headers: "http://localhost:8081", methods: "*", origins: "*")]
    public class PaymentController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Payment(decimal name)
        {
            return Ok(name);
        }
    }
}
