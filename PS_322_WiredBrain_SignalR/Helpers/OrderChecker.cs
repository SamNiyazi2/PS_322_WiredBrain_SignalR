using PS_322_WiredBrain_SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// 06/24/2021 04:14 am - SSN - [20210624-0400] - [004] - M03-02 - Creating a hub

namespace PS_322_WiredBrain_SignalR.Helpers
{
    public class OrderChecker
    {

        private readonly Random random;

        private readonly string[] Status = { "Grinding beans", "Steaming milk, Taking a sip (quality control)", "on transit to counter", "Picked up" };

        private readonly Dictionary<int, int> StatusTracker = new Dictionary<int, int>();


        public OrderChecker(Random random)
        {
            this.random = random;
        }


        private int GetNextStatusIndex ( int OrderNo )
        {
            if (!StatusTracker.ContainsKey(OrderNo))
                StatusTracker.Add(OrderNo, -1);

            StatusTracker[OrderNo]++;

            return StatusTracker[OrderNo];

        }


        public UpdateInfo GetUpdate(Order order)
        {
            if (random.Next(1, 5) != 4)
                return new UpdateInfo { New = false };

            var index = GetNextStatusIndex(order.Id);

            if (Status.Length <= index)
                return new UpdateInfo { New = false };

            var result = new UpdateInfo
            {
                OrderId = order.Id,
                New = true,
                Update = Status[index],
                Finished = Status.Length - 1 == index
            };

            return result;

        }

    }
}