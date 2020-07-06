using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Web;

namespace SmartRental.common
{
    /// <summary>
    /// Upload 的摘要说明
    /// </summary>
    public class Upload : IHttpHandler
    {

        public static string[] imgname;
        public static int nums;
 
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var b = context.Request.Form["HotelBoss"];
            var cc = context.Request.Form["RoomName"];
            if (b!=null&&b!=""&&cc==null)
            HotelPhoto(context);
            if(cc!=null&&cc!=""&&b==null)
            {
                RoomPhoto(context);
            }
            context.Response.Close();
        }
      
       
        public static string[] ImgName()
        {
            string[] imgName = imgname;
            imgname = null;

            return imgName;

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 添加酒店图片方法
        /// </summary>
        /// <param name="context"></param>
        public void HotelPhoto(HttpContext context)
        {


            var num = context.Request.Files.Count;
            imgname = new string[num];
            nums = num;
            var b = context.Request.Form["HotelBoss"];
            var c = context.Request.Form["HotelName"];
            var d = context.Request.Form["HotelPhone"];
            var e = context.Request.Form["HotelOpentime"];
            var f = context.Request.Form["city"];
            var g = context.Request.Form["AddressDetails"];
            var h = context.Request.Form["HotelFacility"];
            var l = context.Request.Form["HotelRule"];
            var j = context.Request.Form["HotelIntro"];

            if (b == "" || c == "" || d == "" || e == "" || f == "" || g == "" || h == null || j == "" || l == "")
            {

            }
            else
            {
                for (int i = 0; i < num; i++)
                {
                    HttpPostedFile file = context.Request.Files[i];
                    if (file.FileName != "")
                    {
                        //上传的文件保存到目录(为了保证文件名不重复，加个Guid)
                        string guid = Guid.NewGuid().ToString();
                        string path = "~/images/HotelPhoto/" + guid + file.FileName;
                        file.SaveAs(context.Request.MapPath(path));//必须得是相对路径
                        imgname[i] = "/images/HotelPhoto/" + guid + file.FileName;
                    }
                    else
                    {
                        imgname[i] = "";
                    }

                    continue;
                }
            }
        }
        /// <summary>
        /// 酒店房间图片添加
        /// </summary>
        /// <param name="context"></param>
        public void RoomPhoto(HttpContext context)
        {
            var num = context.Request.Files.Count;
            imgname = new string[num];
            nums = num;
            var b = context.Request.Form["RoomName"];
            var c = context.Request.Form["RoomLayout"];
            var d = context.Request.Form["RoomCount"];
            var e = context.Request.Form["RoomPrice"];
            var f = context.Request.Form["PrimeCost"];
            var g = context.Request.Form["RoomName"];
            var h = context.Request.Form["RoomFacility"];
            if (b == "" || c == "" || d == "" || e == "" || f == "" || g == "" || h == null)
            {

            }
            else
            {
                for (int i = 0; i < num; i++)
                {
                    HttpPostedFile file = context.Request.Files[i];

                    if (file.FileName != "")
                    {
                        //上传的文件保存到目录(为了保证文件名不重复，加个Guid)
                        string guid = Guid.NewGuid().ToString();
                        string path = "~/images/RoomPhoto/" + guid + file.FileName;
                        file.SaveAs(context.Request.MapPath(path));//必须得是相对路径
                        imgname[i] = "/images/RoomPhoto/" + guid + file.FileName;
                    }
                    else
                    {
                        imgname[i] = "";
                    }

                    continue;
                }
            }

        }
    }
}