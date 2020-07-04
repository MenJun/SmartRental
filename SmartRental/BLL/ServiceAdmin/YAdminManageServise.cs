using SmartRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRental.BLL.ServiceAdmin
{
    public class YAdminManageServise
    {

        public static List<Order> GetStudentByPaging(int pageindex, int pagesize, out int pagecount)
        {
            return DAL.MapperAdmin.YAdminManagerMan.Select(pageindex, pagesize, out pagecount);
        }
        public static List<Order> GetStudentByPaging1(int pageindex, int pagesize, out int pagecount, string a, string b )
        {
            return DAL.MapperAdmin.YAdminManagerMan.Select1(pageindex, pagesize, out pagecount, a, b);
        }
    }
}