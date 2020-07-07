using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartRental.DAL.MapperAPI;
using SmartRental.Models;

namespace SmartRental.BLL.ServiceAPI
{
    public class PersonalCenterService
    {
        public static UserMessage getPersonal(int UserId)
        {
            return PersonalCenterMapper.Personal(UserId);
        }

        internal static object UpdataPersonal(dynamic data)
        {
            UserMessage user = new UserMessage();
            user.UserID = data.data.usrtId;
            user.UserName = data.data.usrt;
            user.Birthday = DateTime.Parse(data.data.Birthdays.Value);
            user.UserPhone = data.data.Photo;
            user.sex = bool.Parse(data.data.Sex.Value);
            user.HeadPhoto = data.data.HeadPhoto;
            return PersonalCenterMapper.Updata(user); ;
        }
    }
}