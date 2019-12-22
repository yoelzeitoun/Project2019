using System;
using System.Collections;
using System.Collections.Generic;
using BE;

namespace DS
{
    public class DataSource : IEnumerable
    {
        public static List<GuestRequest> gsList = new List<GuestRequest>()
        {
            new GuestRequest()
            {

            }

        };
        public static List<HostingUnit> huList = new List<HostingUnit>()
        {
            new HostingUnit()
            {

            }
        };
        public static List<Order> orderList = new List<Order>()
        {
            new Order()
            {

            }
        };

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
