using SmartRental.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SmartRental.DAL.MapperAPI
{
    public class UserLoginMapper
    {
        private static SmartRentalSystemEntities db = new SmartRentalSystemEntities();
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user">用户注册信息</param>
        internal static bool Registr(UserMessage user)
        {
            db.UserMessage.Add(user);
           return db.SaveChanges() > 0 ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        internal static bool Updata(UserMessage user)
        {
            db.Entry(user).State = EntityState.Modified;
            return db.SaveChanges() >0;
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="userPhone">账号</param>
        internal static UserMessage getAccount(string userPhone)
        {
           return db.UserMessage.Where(t => t.UserPhone == userPhone).FirstOrDefault();
        }
    }
}