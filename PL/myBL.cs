using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;

namespace PLWPF
{
    public class myBL : IBL
    {
        public void AddGuestRequest(GuestRequest guestRequest)
        {
            throw new NotImplementedException();
        }

        public void AddHost(Host host)
        {
            throw new NotImplementedException();
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

        public IEnumerable<HostingUnit> GetAvailableHostingUnitList(DateTime dt, int days)
        {
            throw new NotImplementedException();
        }

        public IGrouping<Area, GuestRequest> GetGuestReqGroupByArea(bool sorted = false)
        {
            throw new NotImplementedException();
        }

        public GuestRequest GetGuestRequest(long guestRequestKey)
        {
            throw new NotImplementedException();
        }

        public IGrouping<int, GuestRequest> GetGuestRequestGroupByPersons(bool sorted = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GuestRequest> GetGuestRequestList(Func<GuestRequest, bool> predicat = null)
        {
            throw new NotImplementedException();
        }

        public IGrouping<int, Host> GetHostGroupByNumofHU(bool sorted = false)
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

        public IGrouping<Area, HostingUnit> GetHUGroupByArea(bool sorted = false)
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

        public List<HostingUnit> HostingUnitPerHost(Host h)
        {
            throw new NotImplementedException();
        }

        public int Time_Span(params DateTime[] list)
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

        public bool UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
