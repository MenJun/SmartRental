﻿using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRental.common
{
    /// <summary>
    /// Roomphoto 的摘要说明
    /// </summary>
    public class Roomphoto : IHttpHandler
    {

        public static string[] imgname;
        public static int nums;
    
        public void ProcessRequest(HttpContext context)
        {
           
            context.Response.ContentType = "text/plain";

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
            if(b=="" || c =="" || d == "" || e == "" || f == "" || g == "" || h ==null)
            {

            }
            else { 
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
    }
}