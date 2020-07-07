using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SmartRental.Models;

namespace SmartRental.DAL.MapperAPI
{
    public class PaymentMapper
    {
        public static int insertOrder(Order order)
        {
            using(SmartRentalSystemEntities db =new SmartRentalSystemEntities())
            {
                db.Order.Add(order);
                return db.SaveChanges();
            }
        }

        internal static int insertCompletePayment(Order order)
        {
            using(SmartRentalSystemEntities db =new SmartRentalSystemEntities())
            {
                var orders = db.Order.Where(t => t.OutTradeNo == order.OutTradeNo).ToList().FirstOrDefault();
                orders.Ordertime = order.Ordertime;
                orders.ActualPrice = order.ActualPrice;
                orders.OrderNumber = order.OrderNumber;
                orders.OrderState = "已支付";
                db.Entry(orders).State = EntityState.Modified;
                  db.SaveChanges();

                var Room = db.RoomMessage.Where(t => t.RoomID == orders.RoomID).ToList().FirstOrDefault();
                Room.RoomCount = Room.RoomCount - orders.Ordercount;
                db.Entry(Room).State = EntityState.Modified;
                return db.SaveChanges();
                
            }
        }

        internal static Order GetOrder(string order, int UsertID)
        {
            using (SmartRentalSystemEntities db = new SmartRentalSystemEntities())
            {
                return db.Order.Include("UserMessage").Include("HotelManag").Include("RoomMessage").Where(t => t.OrderNumber == order && t.UserID == UsertID).ToList().FirstOrDefault();
                
            }
        }
    }
}