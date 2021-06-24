using PS_322_WiredBrain_SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 06/24/2021 04:38 am - SSN - [20210624-0438] - [001] - M03-03 - Hub explained

namespace PS_322_WiredBrain_SignalR.Hubs
{
    public interface ICoffeeHub
    {
        Task NewOrder(Order order);

        Task ReceiveOrderUpdate(UpdateInfo info);

        Task Finished(Order order);

    }
}
