using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

// 06/24/2021 05:23 am - SSN - [20210624-0520] - [001] - M03-07 - Persistent connections

namespace PS_322_WiredBrain_SignalR.Connections
{
    public class CoffeeConnection : PersistentConnection
    {

        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            // 06/24/2021 05:29 am - SSN - [20210624-0520] - [003] - M03-07 - Persistent connections

            // Not implemented nore tested.

            ConnectionMessage cm = new ConnectionMessage { };
            Connection.Send(cm);

            string[] excludedConnectionIDs = { };

            object someObject = new { Item1 = "", Item2 = 22 };

            Connection.Broadcast(someObject, excludedConnectionIDs);


            return base.OnReceived(request, connectionId, data);
        }

        protected override Task OnConnected(IRequest request, string connectionId)
        {
            return base.OnConnected(request, connectionId);
        }


        // 06/24/2021 05:39 am - SSN - [20210624-0533] - [002] - M03-08 - Authentication and autherization
        protected override bool AuthorizeRequest(IRequest request)
        {
            IPrincipal currentClient = request.User;

            return base.AuthorizeRequest(request);
        }



 
    }

}