﻿using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using SmartRental.BLL.ServiceAPI;
using SmartRental.DAL.MapperAPI;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Aop.Api.Response;
using SmartRental.Models;

namespace SmartRental.Controllers.API
{
    [EnableCors(headers: "*", methods: "*", origins: "*")]
    public class PaymentController : ApiController
    {
        //沙箱环境，请求支付链接的地址           
        const string URL = "https://openapi.alipaydev.com/gateway.do";
        //支付宝正式环境 
        //https://openapi.alipaydev.com/gateway.do   
        //APPID即创建应用后生成,沙箱环境中的AppId
        const string APPID = "2016092900626596";
        //这里是我们之前用秘钥工具生成的商户私钥
        const string APP_PRIVATE_KEY = "MIIEowIBAAKCAQEAmtXAIVZKHtlVDjLyFsGqmAkPCkN6k7iSfNJ0BWhJdAJ3Fm4+TtVkQDePNde0CwYIDaCbqn//Hc3yLrG+kogeO2ShpK/eR6Q3O3DEe5sjYpm2c3Xv/yeEMTuqN7hfNRl0OHJwRH1Q3zFWmeOlxb7Tc+DrzJ7vkrRZC7xQPETt69vaIbXguLXsbkB3nzcbThRnsJ+gnxpkp9rQulAReO2fP7auzNp/MDqWeTYccDIGZ8cciguVOcA09wnrlLW/qln2DmEaGt9s9A6XjchoPudInl7OVAkVq4oPAz6gLhvhM9hxmnNuEl6Pe+zBmH51ozD/2L2DE1GOyIDTGq5iIuIoHwIDAQABAoIBADFGBLaXN09J6n9yRwhm6VsoxtiFUOThv/xpHPL7lSSOBEfnHX0I+7ZT+AcydeFsMfPkQKWpxg4+E+w4NGV9W+GazLYUIbC62bjY0i5j2IbwU9e6mH2inbbtPeYtjl7fktTD1mZ1Wt8sXFrszzx21ePymBeuHJ+bNRs++mmzpeOAvNUwAOI/Ek1gBhv8nbwu47G7zLTMGLqsz/9li/xkqgoQA2D84+C8+Z9q/0TKtrhRgbUimqoyaQlZtqO7NLv7BZ/63l6DlJ7qKrRpTHtH2xyth47521JPmQqjbSsedWAsTgAeIwH1Y3pv4EMB+yi8EvwKFtvN1dYS5Ujg1v3EMcECgYEAysDkmGTMWEbKJo1PyzpaSZgAeN9PoSkysqax3/Yl9OChSqJPYlBSXm0qgJSHUtNgg4Unt9BoJQQFaQ1QHSnUE5P6qmvY73t1f/RwcMW4kTPQ2VKi7hZylyL7UKXVBBpeZkzo3/sxMDWYCXjlkmeZ6HFEWpWS61beNKyaWiOpLgUCgYEAw39MJp5YrpzgnxZjHkgCZGXPletIlabZelSOcLncTmBVvef2Ye+01TTv1cwHdGbVokQsoaYQ1GVJDTbzbP6cc0g3ICCEcEgzonec4+A8e9pkqYjkzP5SvPq4qJ1xYIEN2nHSyi+fm1+f57ax803EHkbPPdNpxqYQQ40hrqeXctMCgYBfIplghFN5zGWIJ2BcjJnW1FLMZIP5q9oVB7CI1PfEai5kVbqH+AZeNjzuLkM88t/jdnRHhKuJStS7ETsZJApV0WaZ1pbo7/YXPvwPfkb3IugJQQQgUTodfpWLpvRHw5OtzsKqbjQLWepn72lRA/msnpEjK/HPKWiEMMUQLJFyfQKBgHQFYj4xD1RhifUgJibceX735SCGCAe4g6zT7cz8oMBq77fKqDArQFigGA38DhiIxxSL/SjaE3bZvWb4S7NNz6+jFgQdCDxSvUIYVqXueDbQv5jVw2PdM2EwMtLuzVCGhqAzmPOhG8nsEExF4ooQhumpAGkixM291D0izvCGSZ09AoGBAI11UBYyZHH+WeDY0ZW8ZnblfjrBnJRSv8MAoYNMHBIrvqdytI2ZEEH1JLtammeJPxMeBGcurMilfMxt4WDSlkxdagbwDK2ppTmP6I+3py5piarWkqknHdtO0IS9W/1snZPbjgc4bt/PPo4c4M8FXtXt6q5klS/e9u90BzdosbFh"; const string FORMAT = "json";
        //支持GBK和UTF-8
        const string CHARSET = "UTF-8";
        //支付宝公钥
        const string ALIPAY_PUBLIC_KEY = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDIgHnOn7LLILlKETd6BFRJ0GqgS2Y3mn1wMQmyh9zEyWlz5p1zrahRahbXAfCfSqshSNfqOmAQzSHRVjCqjsAw1jyqrXaPdKBmr90DIpIxmIyKXv4GGAkPyJ/6FTFY99uhpiq0qadD/uSzQsefWo0aTvP/65zi3eof7TcZ32oWpwIDAQAB";
 Models.Order order = new Models.Order();

        IAopClient client = new DefaultAopClient(URL, APPID, APP_PRIVATE_KEY, FORMAT, "2.0", "RSA2", ALIPAY_PUBLIC_KEY, CHARSET, false);
        //实例化具体API对应的request类,类名称和接口名称对应,当前调用接口名称如：
        AlipayTradePrecreateRequest request = new AlipayTradePrecreateRequest();//创建API对应的request类,请求返回二维码
        AlipayTradePagePayRequest requestPagePay = new AlipayTradePagePayRequest();//请求返回支付宝支付网页
        AlipayTradePagePayModel model = new AlipayTradePagePayModel();
          [System.Web.Http.HttpPost]
        public IHttpActionResult Payment(dynamic data)
        {
            var avl = "";
            order.UserID = data.data.UserId;//用户ID
            order.HotelID = data.data.hotelId;//酒店ID
            order.RoomID = data.data.RoomID;//房间ID
            order.ArrivalDate = data.data.minDate;//入住时间
            order.LeaveTime = data.data.maxDate;//离开时间
            order.ActualPrice = data.data.Pey;//实付金额
            order.ClientPhone = data.data.cellphone;//号码
            foreach (var item in data.data.contert)
            {
                avl += item+",";
            }
            order.ClientName = avl;
            order.Ordercount = data.data.numbers;//房间数量
            order.OutTradeNo = DateTime.Now.ToString("yyyyMMddHHmmss"); //订单号
            order.OrderState = "待支付";
            PaymentService.Payment(order);
            //Alipay
           
            //model.Body = "一般般";
            //model.GoodsDetail = data;
            model.Subject = data.data.fang;
            model.TotalAmount =data.data.Pey;
            model.OutTradeNo = order.OutTradeNo;
            model.StoreId = "William001";
              model.ProductCode = "FAST_INSTANT_TRADE_PAY";
            requestPagePay.SetBizModel(model);
            requestPagePay.SetReturnUrl("http://47.106.255.80:8080/Snail/#/OrderDetailss");
            var response = client.SdkExecute(requestPagePay);//Execute(request);
            if (!response.IsError)
            {
                var res = new
                {
                    success = true,
                    out_trade_no = response.OutTradeNo,
                    // qr_code = response.QrCode,    //二维码字符串
                    pay_url = URL + "?" + response.Body
                };
                return Ok(res);
            }
            else
            {
                var res = new
                {
                    success = false,
                };
                return Ok(res);
                
            }

          
        }
        /// <summary>
        /// 支付完成
        /// </summary>
        /// <param name="out_trade_no">商家号</param>
        /// <param name="trade_no">订单号</param>
        /// <param name="timestamp">下单时间</param>
        /// <param name="total_amount">价钱</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public IHttpActionResult Payments(dynamic data)
        {
           // string out_trade_no, string trade_no, string timestamp, string total_amount
            order.OutTradeNo = data.data.out_trade_no;
            order.OrderNumber = data.data.trade_no;
            order.Ordertime =DateTime.Parse(data.data.timestamp.Value);
            order.ActualPrice = decimal.Parse(data.data.total_amount.Value);
            PaymentService.CompletePayment(order);
            //requestPagePay.SetReturnUrl("");
            return Ok(order);
            
        }
        
        /// <summary>
        /// 订单支付成功显示
        /// </summary>
        /// <param name="order">订单号</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult Order(string order, int UserId)
        {

          return  Ok(PaymentService.Order(order, UserId));
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult dingw()
        {
            StreamReader sr = null;
            Stream s = null;
            string strUrl = "https://api.ipify.org"; //获得IP的网址了
            Uri uri = new Uri(strUrl);
            WebRequest wr = WebRequest.Create(uri);
            s = wr.GetResponse().GetResponseStream();
            sr = new StreamReader(s, Encoding.Default);
            string all = sr.ReadToEnd(); //读取网站的数据

            string url = "https://restapi.amap.com/v3/ip?ip=106.17.192.121&output=xml&key=1779729a3e64011a6993f6cda397764d";
            return Ok();
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult kkk(dynamic data)
        {

            IAopClient client = new DefaultAopClient(URL, APPID, APP_PRIVATE_KEY, FORMAT, "2.0", "RSA2", ALIPAY_PUBLIC_KEY, CHARSET, false);
            // 商户订单号，和交易号不能同时为空
            string out_trade_no = "20200709104925";

            // 支付宝交易号，和商户订单号不能同时为空
            string trade_no = "2020070922001450210500981173";

            // 退款金额，不能大于订单总金额
            string refund_amount = "110.00";

            // 退款原因
            string refund_reason = "我就是想退款";

            // 退款单号，同一笔多次退款需要保证唯一，部分退款该参数必填。
            string out_request_no = "16541";

            AlipayTradeRefundModel model = new AlipayTradeRefundModel();
            model.OutTradeNo = data.data.OutTradeNo;
            model.TradeNo = data.data.TradeNo;
            model.RefundAmount = data.data.RefundAmount;
            model.RefundReason = data.data.RefundReason;
            model.OutRequestNo = DateTime.Now.ToString("yyyyMMddHHmmss");

            AlipayTradeRefundRequest request = new AlipayTradeRefundRequest();
            request.SetBizModel(model);

            AlipayTradeRefundResponse response = null;
        
            response = client.SdkExecute(request);
            using (SmartRentalSystemEntities db =new SmartRentalSystemEntities())
            {
                var ode = db.Order.Where(t => t.OrderNumber == model.OutTradeNo).ToList().FirstOrDefault();
                ode.OrderState = "退款";
                db.SaveChanges();
            }
            //Execute(request);
            return Ok(response.Body);
        }

     
    }
}
