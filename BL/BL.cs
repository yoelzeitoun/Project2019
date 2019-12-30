using System;
using DAL;
using BE;
using DS;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public class BL : IBL
    {
        IDal d;
        Host h;
        public void AddGuestRequest(GuestRequest guestRequest)
        {
            // check if the release date is at least one day after entry day
            if (DateTime.Compare(guestRequest.EntryDate, guestRequest.ReleaseDate) >= 0)
                throw new DuplicateWaitObjectException("Error in the interval date");

            d.AddGuestRequest(guestRequest);
        }

        public void AddHostingUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }

        public void AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public bool DeleteHostingUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }

        public GuestRequest GetGuestRequest(long guestRequestKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GuestRequest> GetGuestRequestList(Func<GuestRequest, bool> predicat = null)
        {
            throw new NotImplementedException();
        }

        public HostingUnit GetHostingUnit(long hostingUnitKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HostingUnit> GetHostingUnitList(Func<HostingUnit, bool> predicat = null)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(long orderKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrderList(Func<Order, bool> predicat = null)
        {
            throw new NotImplementedException();
        }

        public void UpdateGuestRequest(GuestRequest guestRequest)
        {
            throw new NotImplementedException();
        }

        public void UpdateHostingUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order order)
        {
            // verify if the Host accepted the Collection Clearance
            if (order.status_Order == Status_order.Email_sent && !d.FindHost(order).CollectionClearance)
                throw new Exception("Please accept the Collection Clearance!");
            
            HostingUnit hosting1 = DataSource.hostingUnitList.FirstOrDefault(t => t.HostingUnitKey == order.HostingUnitKey);
            GuestRequest gs1 = DataSource.guestRequestList.FirstOrDefault(t => t.GuestRequestKey == order.GuestRequestKey);
            if (!CheckDatesAvailable(hosting1, gs1))
            {
                throw new Exception("the dates you chose are not available!");
            }
            if (!(order.status_Order == Status_order.Closed_for_customer_response))
                order.status_Order = Status_order.In_progress;
            d.UpdateOrder(order);
        }

        public bool CheckDatesAvailable(HostingUnit hu, GuestRequest gs)
        {
            for (var date = gs.EntryDate; date < gs.ReleaseDate; date = date.AddDays(1))
            {
                if (hu.Diary[date.Month, date.Day])
                    return false;
            }
            return true;
        }
    }
}