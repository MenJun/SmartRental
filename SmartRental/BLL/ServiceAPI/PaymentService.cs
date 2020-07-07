using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartRental.DAL.MapperAPI;
using SmartRental.Models;

namespace SmartRental.BLL.ServiceAPI
{
    public class PaymentService
    {
        public static bool Payment(Order order)
        {
            return PaymentMapper.insertOrder(order) > 0;
        }

        internal static bool CompletePayment(Order order)
        {
            return PaymentMapper.insertCompletePayment(order) > 0;
        }
        internal static Order Order(string order, int UsertID)
        {
            return PaymentMapper.GetOrder(order, UsertID);
        }
    }
}