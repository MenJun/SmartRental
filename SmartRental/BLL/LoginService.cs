using SmartRental.DAL;
using SmartRental.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRental.BLL
{
    public class LoginService
    {

        /// <summary>
        /// 后台登陆判断超级管理员。普通管理员
        /// </summary>
        /// <param name="userPhone">账号</param>
        /// <param name="userPwd">密码</param>
        /// <returns></returns>
        internal static object Decryption(string userPhone, string userPwd)
        {
            var user = LoginMapper.GetLogin(userPhone);
            string UserPwd = TrickLock.WeiYiJieMiGuid(user.UserPwd);

            var Rples = LoginMapper.Rples(userPhone);
            if (Rples == null)
            {
                return 3;
            }else if (Rples.user_roles1 == "超级管理员")//超级管理员
            {
                if (UserPwd == userPwd)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else if (Rples.user_roles1 == "管理员")//普通管理员
            {
                if (UserPwd == userPwd)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else //用户
            {
                return 3;
            }


           

            }
        internal static object DecryptionHotelID(string userPhone)
          {
                var user = LoginMapper.GetLogin(userPhone);
            return user.HotelID;
           }
    }
}