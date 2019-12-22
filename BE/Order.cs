using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Order
    {
        public long HostingUnitKey = 10000000;
        public long GuestRequestKey = 10000000;
        public long OrderKey = 10000000;
        public Status_order status_Order { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime OrderDate { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
