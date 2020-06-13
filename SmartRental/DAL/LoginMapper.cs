using SmartRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRental.DAL
{
    public class LoginMapper
    {
        private static SmartRentalSystemEntities db = new SmartRentalSystemEntities();
        /// <summary>
        /// 登陆查询
        /// </summary>
        /// <param name="userPhone">账号查询</param>
        /// <returns></returns>
        internal static UserMessage GetLogin(string userPhone)
        {
            var user = db.UserMessage.Where(t => t.UserPhone == userPhone).FirstOrDefault();
            return user;
        }
        /// <summary>
        /// 权限查询
        /// </summary>
        /// <param name="userPhone">通过号码查询权限</param>
        /// <returns></returns>
        internal static User_roles Rples(string userPhone)
        {
            return db.User_roles.Where(t => t.UserName == userPhone).FirstOrDefault();
        }
    }
}