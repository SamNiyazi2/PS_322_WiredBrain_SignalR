using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using PS_322_WiredBrain_SignalR.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// 06/24/2021 04:49 am - SSN - [20210624-0449] - [001] - M03-04 - Configuring the ASP.NET project

[assembly: OwinStartup(typeof(PS_322_WiredBrain_SignalR.Startup))]

namespace PS_322_WiredBrain_SignalR
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();

            // 06/24/2021 05:25 am - SSN - [20210624-0520] - [002] - M03-07 - Persistent connections
            app.MapSignalR<CoffeeConnection>("/coffee");

            // 06/24/2021 05:35 am - SSN - [20210624-0533] - [001] - M03-08 - Authentication and autherization
            GlobalHost.HubPipeline.RequireAuthentication();

        }

    }

}