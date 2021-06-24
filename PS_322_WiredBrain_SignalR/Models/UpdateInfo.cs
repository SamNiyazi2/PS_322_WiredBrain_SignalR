using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// 06/24/2021 04:03 am - SSN - [20210624-0400] - [003] - M03-02 - Creating a hub

namespace PS_322_WiredBrain_SignalR.Models
{
    public class UpdateInfo
    {
        public int OrderId { get; set; }
        public bool New { get; set; }
        public string Update { get; set; }
        public bool Finished { get; set; }

    }
}