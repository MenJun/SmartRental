using SmartRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRental.DAL.MapperAdmin
{
    public class GHotelManagerMan
    {
        public static int AddHotelManager(HotelManag manager, string[] photo)
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                HotelPhoto hotel = new HotelPhoto();
                for (int i = 0; i < photo.Length; i++)
                {

                    if (i == 0)
                        hotel.Hotelphoto1 = photo[i].ToString();
                    if (i == 1)
                        hotel.Hotelphoto2 = photo[i].ToString();
                    if (i == 2)
                        hotel.Hotelphoto3 = photo[i].ToString();
                    if (i == 3)
                        hotel.Hotelphoto4 = photo[i].ToString();
                    if (i == 4)
                        hotel.Hotelphoto5 = photo[i].ToString();
                    if (i == 5)
                        hotel.Hotelphoto6 = photo[i].ToString();
                    if (i == 6)
                        hotel.Hotelphoto7 = photo[i].ToString();

                }
              var bb=db.HotelPhoto.Add(hotel);
                db.SaveChanges();
                int photoID = bb.HotelPhotoID;
                manager.HotelPhotoID = photoID;
                db.HotelManag.Add(manager);

                return db.SaveChanges();


            }
          



        }
    }
}