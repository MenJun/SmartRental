using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SmartRental.Models;

namespace SmartRental.DAL.MapperAPI
{
    public class PersonalCenterMapper
    {
        public static UserMessage Personal(int UserId)
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                var aa = db.UserMessage.Where(t => t.UserID == UserId).ToList().FirstOrDefault();
                return aa;
            }
        }

        internal static object Updata(UserMessage user)
        {
           
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                var aa = db.UserMessage.Where(t=>t.UserID == user.UserID).ToList().FirstOrDefault();
                aa.UserName = user.UserName;
                aa.Birthday = user.Birthday;
                aa.sex = user.sex;
                db.Entry(aa).State = EntityState.Modified;
                return db.SaveChanges();

            }
          
        }
    }
}