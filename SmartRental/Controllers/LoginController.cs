using SmartRental.BLL;
using SmartRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SmartRental.Controllers
{
   
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="inputString">后台登陆</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetDecryptionString(UserMessage userMessage)
        {
            var decryptionResult = LoginService.Decryption(userMessage.UserPhone, userMessage.UserPwd);
            if (int.Parse(decryptionResult.ToString()) == 1) //超级管理员
            {
                Session["SuperAdmin"] = userMessage.UserPhone;
                return Json(new { roestr = "/Home/Main" }, JsonRequestBehavior.AllowGet);
            }
            else if (int.Parse(decryptionResult.ToString()) == 0) //密码错误
            {

                return Json(new { roestr = "你妈逼错误了" }, JsonRequestBehavior.AllowGet);
            }
            else if (int.Parse(decryptionResult.ToString()) == -1) // 普通管理员
            {
                Session["GeneralManager"] = userMessage.UserPhone;
                Session["HotelName"] = 10;
                return Json(new { roestr = "/Home/Main" }, JsonRequestBehavior.AllowGet);
            }
            else //不是管理员
            {
                return Json(new { roestr = "没有权限" }, JsonRequestBehavior.AllowGet);
            }

        }



  


    }
}