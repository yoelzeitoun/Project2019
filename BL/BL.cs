using System;
using DAL;
using BE;
using System.Collections.Generic;

namespace BL
{
    public class BL : IBL
    {
        public void addGuestRequest(GuestRequest guestRequest)
        {
            // check if the release date is at least one day after entry day
            if (DateTime.Compare(guestRequest.EntryDate, guestRequest.ReleaseDate) >= 0) 
                throw new DuplicateWaitObjectException ("Error in the interval date"); 
        }

        public void addOrder(Order order)
        {

        }

        public void addUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }

        public List<BankBranch> allBanks()
        {
            throw new NotImplementedException();
        }

        public void allGuestRequests()
        {

        }

        public void allOrders()
        {

        }

        public void allUnits()
        {

        }

        public void deleteUnit()
        {

        }
        
        public void statusChange()
        {
            throw new NotImplementedException();
        }

        public void updateOrder()
        {
            throw new NotImplementedException();
        }

        public void updateUnit()
        {
            throw new NotImplementedException();
        }
    }
}
