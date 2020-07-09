using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SmartRental.Models;

namespace SmartRental.DAL.MapperAdmin
{
    public class HotelMapper
    {
        public static SmartRentalSystemEntities db = new SmartRentalSystemEntities();
        /// <summary>
        /// 查询所有酒店
        /// </summary>
        /// <returns></returns>
        public static List<HotelManag> GetHotel()
        {
            return db.HotelManag.OrderByDescending(d=>d.Hoteltration_time).ToList();
        }
         public static bool UpdataAudit(HotelManag hotel)
        {
     
           var re= db.HotelManag.Find(hotel.HotelID);
            re.HotelRatify = hotel.HotelRatify;
            if (re.HotelRatify==true)
            {
                var userurel = db.UserMessage.Where(s => s.HotelID == hotel.HotelID).FirstOrDefault();
                var usme = db.UserMessage.Find(userurel.UserID);
                usme.UserGrade = "管理员";
                user_roles user_Roles = new user_roles();
                user_Roles.UserPhone = usme.UserPhone;
                user_Roles.user_roles1 = usme.UserGrade;
                db.user_roles.Add(user_Roles);

            }
            else
            {
                var userurel = db.UserMessage.Where(s => s.HotelID == hotel.HotelID).FirstOrDefault();
                var usme = db.UserMessage.Find(userurel.UserID);
                usme.UserGrade = "用户";
                var usrolfirst = db.user_roles.Where(s => s.UserPhone == usme.UserPhone).FirstOrDefault();
                var usrol = db.user_roles.Find(usrolfirst.user_rolesID);
                db.user_roles.Remove(usrol);
               
            }
            return  db.SaveChanges() > 0;
        }
        public static List<HotelManag>  SelectHotel(string hotelname)
        {

            var re = db.HotelManag.OrderByDescending(d => d.Hoteltration_time).Where(s=>s.HotelName.Contains(hotelname)).ToList();
          
            return re;
        }
    }
}