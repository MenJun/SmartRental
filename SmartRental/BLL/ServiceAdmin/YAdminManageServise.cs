using SmartRental.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows;

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
        /// <summary>
                /// 对数据库的备份和恢复操作，Sql语句实现
                /// </summary>
                /// <param name="cmdText">实现备份或恢复的Sql语句</param>
                /// <param name="isBak">该操作是否为备份操作，是为true否，为false</param>
        public static void BakReductSql(string cmdText, bool isBak)
        {
            SqlCommand cmdBakRst = new SqlCommand();
            SqlConnection conn = new SqlConnection("Data Source=47.115.31.38;Initial Catalog=SmartSystem;uid=lgm;pwd=qw7878.00;");
            try
            {
                conn.Open();
                cmdBakRst.Connection = conn;
                cmdBakRst.CommandType = CommandType.Text;
                if (!isBak)     //如果是恢复操作
                {
                    string setOffline = "Alter database ExamDB Set Offline With rollback immediate ";
                    string setOnline = " Alter database ExamDB Set Online With Rollback immediate";
                    cmdBakRst.CommandText = setOffline + cmdText + setOnline;
                }
                else
                {
                    cmdBakRst.CommandText = cmdText;
                }
                cmdBakRst.ExecuteNonQuery();
                if (!isBak)
                {
                    MessageBox.Show("恭喜你，数据成功恢复为所选文档的状态！", "系统消息");
                }
                else
                {
                    MessageBox.Show("恭喜，你已经成功备份当前数据！", "系统消息");
                }
            }
            catch (SqlException sexc)
            {
                MessageBox.Show("失败，可能是对数据库操作失败，原因：" + sexc, "数据库错误消息");
            }
            catch (Exception ex)
            {
                MessageBox.Show("对不起，操作失败，可能原因：" + ex, "系统消息");
            }
            finally
            {
                cmdBakRst.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }
    }
}