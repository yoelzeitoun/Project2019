using System;
using System.Collections.Generic;
using System.Text;
using BE;


namespace DAL
{
    interface Idal
    {
        void addGuestRequest(GuestRequest guestRequest);
        void statusChange();
        void addUnit(HostingUnit hostingUnit);
        void deleteUnit();
        void updateUnit();
        void addOrder(Order order);
        void updateOrder();
        void allUnits();
        void allGuestRequests();
        void allOrders();
        List<BankBranch> allBanks();
    }
}
