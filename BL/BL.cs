using System;
using DAL;
using BE;
using System.Collections.Generic;

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
            if (order.status_Order== Status_order.Email_sent && !d.FindHost(order).CollectionClearance)
                throw new Exception("Please accept the Collection Clearance!");
            
            d.UpdateOrder(order);
        }
    }
}