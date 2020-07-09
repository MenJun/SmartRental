using System;
using SmartRental.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartRental.Helpers;

namespace SmartRental.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IdentifyingCode()
        {
            return View();
        }

        public ActionResult CreateVerifyCode()
        {
            VerifyCode vc = new VerifyCode();
            byte[] result = vc.GetVerifyCode();
            return File(result, "image/jpeg jpeg jpg jpe");
        }

        public string CheckVerifyCode(string thecode)
        {
            string result = "";
            string sessioncode = Session["verifyCode"].ToString();
            string thecodeX = thecode.ToLower();
            string sessioncodeX = sessioncode.ToLower();
            if (thecodeX == sessioncodeX)
            {
                result = "ok";
            }
            else
            {
                result = "no";
            }
            return result;
        }
    }
}