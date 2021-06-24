using Microsoft.AspNet.SignalR;
using PS_322_WiredBrain_SignalR.Helpers;
using PS_322_WiredBrain_SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

// 06/24/2021 04:01 am - SSN - [20210624-0400] - [001] - M03-02 - Creating a hub

namespace PS_322_WiredBrain_SignalR.Hubs
{

    public class CoffeeHub : Hub<ICoffeeHub>
    {
        

        public const string GROUP_NAME_ALL_UPDATES_RECEIVERS = "ReceiveOrderUpdate";
        public const string QUERY_STRING_GROUP_NAME_ALL_UPDATES = "allUpdates";

        private static readonly OrderChecker _orderChecker = new OrderChecker(new Random());


        public async Task GetUpdateForOrder(Order order)
        {
           IPrincipal currentClient =  Context.User;


            await Clients.Others.NewOrder(order);

            UpdateInfo result;

            do
            {
                result = _orderChecker.GetUpdate(order);
                await Task.Delay(700);

                if (!result.New)
                    continue;

                await Clients.Caller.ReceiveOrderUpdate(result);

                await Clients.Group(GROUP_NAME_ALL_UPDATES_RECEIVERS).ReceiveOrderUpdate(result);



            } while (!result.Finished);

            await Clients.Caller.Finished(order);

        }

        public override Task OnConnected()
        {
            // The connectionId of the calling client

            string connectionId1 = Context.ConnectionId;
            string connectionId2 = Context.ConnectionId;

            // Clients.Client(Context.ConnectionId).NewOrder();

            Clients.AllExcept(connectionId1, connectionId2);

            string groupName = "SomeGroupName";
            Groups.Add(connectionId1, groupName);
            Groups.Remove(connectionId1, groupName);

            // A group is greated when a client is added.  It is removed when last client is removed.
            // Groups are per hub.

            if (Context.QueryString["group"] == QUERY_STRING_GROUP_NAME_ALL_UPDATES)
            {
                Groups.Add(connectionId1, GROUP_NAME_ALL_UPDATES_RECEIVERS);
            }



            return base.OnConnected();
        }
    }
}