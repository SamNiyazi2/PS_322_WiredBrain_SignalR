using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// 06/24/2021 04:02 am - SSN - [20210624-0400] - [002] - M03-02 - Creating a hub

namespace PS_322_WiredBrain_SignalR.Models
{

    public class Order
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public string Size { get; set; }
    }

}