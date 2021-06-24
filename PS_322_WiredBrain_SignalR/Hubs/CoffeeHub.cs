using Microsoft.AspNet.SignalR;
using PS_322_WiredBrain_SignalR.Helpers;
using PS_322_WiredBrain_SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

// 06/24/2021 04:01 am - SSN - [20210624-0400] - [001] - M03-02 - Creating a hub

namespace PS_322_WiredBrain_SignalR.Hubs
{
    public class CoffeeHub:Hub
    {
        private static readonly OrderChecker _orderChecker = new OrderChecker(new Random());

        
        public async Task  GetUpdateForOrder ( Order order)
        {

            await Clients.Others.NewOrder(order);

            UpdateInfo result;

            do
            {
                result = _orderChecker.GetUpdate(order);
                await Task.Delay(700);

                if (!result.New)
                    continue;

                await Clients.Caller.ReceivOrderUpdate(result);


            } while (!result.Finished);

            await Clients.Caller.Finished(order);

        }
    }
}