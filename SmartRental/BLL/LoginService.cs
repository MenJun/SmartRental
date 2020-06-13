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
            if (UserPwd == userPwd)
            {
                var Rples = LoginMapper.Rples(userPhone);
                if (Rples.user_roles1 == "超级管理员")//超级管理员
                {
                    return 1;
                }
                else if (Rples.user_roles1 == "管理员")//普通管理员
                {
                    return -1;
                }
                else //用户
                {
                    return 0;
                }

            }
            else
            {
                return null;
            }


        }
    }
}