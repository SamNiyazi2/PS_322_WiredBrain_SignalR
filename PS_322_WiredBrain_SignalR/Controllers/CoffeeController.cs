using Microsoft.AspNet.SignalR;
using PS_322_WiredBrain_SignalR.Hubs;
using PS_322_WiredBrain_SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// 06/24/2021 04:57 am - SSN - [20210624-0456] - [001] - M03-05 - Using hubs anywhere


namespace PS_322_WiredBrain_SignalR.Controllers
{
    public class CoffeeController : ApiController
    {
        // 09/26/2022 05:51 pm - SSN - Add lock
        private readonly object lockObject = new object();

        private static int OrderId;

        [HttpPost]
        public int OrderCoffee(Order order)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<CoffeeHub>();

            // 09/26/2022 05:49 pm - SSN 

            lock (lockObject)
            {
                OrderId++;
                order.Id = OrderId;
                hubContext.Clients.All.NewOrder(order);
            }

            return order.Id;

        }
    }
}