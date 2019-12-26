using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace BL
{
    public interface IBL
    {
        void AddGuestRequest(GuestRequest guestRequest);
        void UpdateGuestRequest(GuestRequest guestRequest);
        GuestRequest GetGuestRequest(long guestRequestKey);

        void AddHostingUnit(HostingUnit hostingUnit);
        bool DeleteHostingUnit(HostingUnit hostingUnit);
        void UpdateHostingUnit(HostingUnit hostingUnit);
        HostingUnit GetHostingUnit(long hostingUnitKey);

        void AddOrder(Order order);
        void UpdateOrder(Order order);
        Order GetOrder(long orderKey);

        //returns copy of the Hosting Unit collection which answer a specific predicate or null
        IEnumerable<HostingUnit> GetHostingUnitList(Func<HostingUnit, bool> predicat = null);

        //returns copy of the Guest Request collection which answer a specific predicate or null
        IEnumerable<GuestRequest> GetGuestRequestList(Func<GuestRequest, bool> predicat = null);

        //returns copy of the order collection which answer a specific predicate or null
        IEnumerable<Order> GetOrderList(Func<Order, bool> predicat = null);

    }
}
