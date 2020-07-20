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
            Session.Abandon();
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
            Session.Timeout=720;
            var decryptionResult = LoginService.Decryption(userMessage.UserPhone, userMessage.UserPwd);
            if (int.Parse(decryptionResult.ToString()) == 1) //超级管理员
            {
                Session["SuperAdmin"] = userMessage.UserPhone;
                return Json(new { succeed =200, roestr = "/Hotel/Index" }, JsonRequestBehavior.AllowGet);
            }
            else if (int.Parse(decryptionResult.ToString()) == 0) //密码错误
            {

                return Json(new { succeed = 404, roestr = "密码错误" }, JsonRequestBehavior.AllowGet);
            }
            else if (int.Parse(decryptionResult.ToString()) == -1) // 普通管理员
            {
                SmartRentalSystemEntities db = new SmartRentalSystemEntities();
              
                if (BLL.LoginService.DecryptionHotelID(userMessage.UserPhone) == null)
                {
                     return Json(new { succeed = 200, roestr = "/GHotelHome/Main" }, JsonRequestBehavior.AllowGet);
                }
          
                
                
                Session["GeneralManager"] = userMessage.UserPhone;
                Session["HotelID"] = BLL.LoginService.DecryptionHotelID(userMessage.UserPhone);
                return Json(new { succeed = 200, roestr = "/GHotelHome/MainIndex" }, JsonRequestBehavior.AllowGet);
            }
            else //不是管理员
            {
                return Json(new { succeed = 404, roestr = "没有权限" }, JsonRequestBehavior.AllowGet);
            }

        }



  


    }
}