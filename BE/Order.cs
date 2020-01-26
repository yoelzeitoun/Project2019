using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Order
    {
        public long HostingUnitKey { get; set; }
        public long GuestRequestKey { get; set; }
        public long OrderKey { get; set; }
        public Status_order status_Order { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime OrderDate { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
