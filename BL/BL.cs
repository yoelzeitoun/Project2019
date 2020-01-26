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
        public XML xml = Factory_XML.GetXML();
        private static BL instance = null;
        public static BL getMyBL()
        {
            if (instance == null)
            {
                instance = new BL();
            }
            return instance;
        }
        #region SetKey
        public void SetHostKey(Host host)
        {
            xml.SetHostKey(host);
        }
        public void SetHostingUnitKey(HostingUnit hostingUnit)
        {
            xml.SetHostingUnitKey(hostingUnit);
        }
        public void SetGuestRequestKey(GuestRequest guestRequest)
        {
            xml.SetGuestRequestKey(guestRequest);
        }
        public void SetOrderKey(Order order)
        {
            xml.SetOrderKey(order);
        }
        #endregion
        #region Host XML
        public void AddHost (Host host)
        {
            xml.AddHost(host);
        }
        public bool IsHostExists(string email)
        {
            return xml.IsHostExists(email);
        }
        public bool CheckPass(string email, string password)
        {
            return xml.CheckPass(email, password);
        }
        public Host GetHost(string email)
        {
            return xml.GetHost(email);
        }
        public IEnumerable<Host> GetHostList()
        {
            return xml.GetHostList();
        }
        #endregion
        #region other
        public void AddGuestRequest(GuestRequest guestRequest)
        {
            // check if the release date is at least one day after entry day
            if (DateTime.Compare(guestRequest.EntryDate, guestRequest.ReleaseDate) >= 0)
                throw new DuplicateWaitObjectException("Error in the interval date");
            else
            {
                xml.AddGuestRequest(guestRequest);
            }
        }

        public IEnumerable<string> HostingUnitList(string email)
        {
            return xml.HostingUnitList(email);
        }

        public void AddHostingUnit(HostingUnit hostingUnit)
        {
            xml.AddHostingUnit(hostingUnit);
        }

        public void AddOrder(Order order)
        {
            xml.AddOrder(order);
        }

        public bool DeleteHostingUnit(string email, string hu)
        {
            //Order order = DataSource.orderList.FirstOrDefault(o => o.HostingUnitKey == hostingUnit.HostingUnitKey);
            //if (order.status_Order== Status_order.In_progress)
            //    throw new Exception("This hosting Unit has an open order!");
            //if (d.DeleteHostingUnit(hostingUnit))
            //    return true;
            //return false;
            if (xml.DeleteHostingUnit(email, hu))
                return true;
            return false;
        }

        public GuestRequest GetGuestRequest(long guestRequestKey)
        {
            return d.GetGuestRequest(guestRequestKey);
        }

        public IEnumerable<GuestRequest> GetGuestRequestList()
        {
            return d.GetGuestRequestList();
        }

        public HostingUnit GetHostingUnit(string email, string hu)
        {
            return xml.GetHostingUnit(email, hu);
        }

        public IEnumerable<HostingUnit> GetHostingUnitList()
        {
            return xml.GetHostingUnitList();
        }

        public Order GetOrder(long orderKey)
        {
            return d.GetOrder(orderKey);
        }

        public IEnumerable<Order> GetOrderList(Func<Order, bool> predicate = null)
        {
            return d.GetOrderList(predicate);
        }

        public void UpdateGuestRequest(GuestRequest guestRequest)
        {
            d.UpdateGuestRequest(guestRequest);
        }

        public void UpdateHostingUnit(HostingUnit hostingUnit)
        {
            //HostingUnit hu = DataSource.hostingUnitList.FirstOrDefault(h => h.HostingUnitKey == hostingUnit.HostingUnitKey);
            //Order order = DataSource.orderList.FirstOrDefault(o => o.HostingUnitKey == hostingUnit.HostingUnitKey);
            //if an order is in progress we can't cancel the Debit Authorisation
            //if (!hostingUnit.DebitAuthorization && hu.DebitAuthorization && order.status_Order==Status_order.In_progress)
            //    throw new Exception("You can't cancel the Debit Authorisation as an order is in progress!");
            xml.UpdateHostingUnit(hostingUnit);
        }

        public bool UpdateOrder(Order order)
        {
            //find the Hosting Unit and Guest Request of the order
            HostingUnit hosting1 = DataSource.hostingUnitList.FirstOrDefault(t => t.HostingUnitKey == order.HostingUnitKey);
            GuestRequest gs1 = DataSource.guestRequestList.FirstOrDefault(t => t.GuestRequestKey == order.GuestRequestKey);
            Order order1 = DataSource.orderList.FirstOrDefault(t => t.OrderKey == order.OrderKey);
            
            // verify if the Host accepted the Collection Clearance
            if (order.status_Order == Status_order.Email_sent && !d.FindHost(order).CollectionClearance)
                throw new Exception("Please accept the Collection Clearance!");

            // if the status order changed to email_sent, automatically send a confirmation email.
            if(order.status_Order==Status_order.Email_sent && order1.status_Order != Status_order.Email_sent)
                Console.WriteLine(gs1.ToString());

            //check if the dates are available
            if (!CheckDatesAvailable(hosting1, gs1))
            {
                throw new Exception("the dates you chose are not available!");
            }
            //if status order is "Closed_for_customer_response" don't allow to change it.
            if (!(order.status_Order == Status_order.Closed_for_customer_response))
                order.status_Order = Status_order.In_progress;
            //if status order is "Closed_for_customer_response" change the status of the Guest Request.
            if (order.status_Order == Status_order.Closed_for_customer_response)
            { 
                gs1.status_Client = Status_client.Closed_through_site;
                //when a guest close an order, this fonction closes all his others orders
                foreach ( var item in DataSource.orderList)
                {
                    if (item.GuestRequestKey == order.GuestRequestKey)
                        item.status_Order = Status_order.Closed_for_another_order;
                }
            }
            
            
            if (d.UpdateOrder(order))

            { // if all the verifications are ok, change the dates to "occuped" in the Diary 
                d.DiaryChangeToOccuped(hosting1, gs1);
                // add comission on each hosting's day when the status order changed to "email_sent"
                if (order.status_Order == Status_order.Email_sent)
                    d.FindHost(order).Total_commission += Configuration.Commission * Time_Span(gs1.EntryDate, gs1.ReleaseDate);
                return true;
            }
            return false;
        }
        
        //fonction that checks if the dates are available
        public bool CheckDatesAvailable(HostingUnit hu, GuestRequest gs)
        {
            for (var date = gs.EntryDate; date < gs.ReleaseDate; date = date.AddDays(1))
            {
                if (hu.Diary[date.Month, date.Day])
                    return false;
            }
            return true;
        }

        //IEnumerable<HostingUnit> GetAvailableHostingUnitList(Func<DateTime, int, HostingUnit> predicate = null)
        //{
        //    var v = from item in DataSource.hostingUnitList
        //            select new HostingUnit(); //default constructor!!

        //    if (predicate == null)
        //        return v.AsEnumerable().OrderByDescending(h => h.HostingUnitName);

        //    return v.Where(predicate).OrderByDescending(h => h.HostingUnitName);
        //}
        public IEnumerable<HostingUnit> GetAvailableHostingUnitList (DateTime dt, int days)
        {
            List<HostingUnit> tempList = new List<HostingUnit>();
            bool flag = true;
            DateTime releaseDate = dt.AddDays(days);
            foreach (var item in DataSource.hostingUnitList)
            {
                for(DateTime date=dt;date<releaseDate;date.AddDays(1))
                {
                    if (item.Diary[date.Month, date.Day])
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                    tempList.Add(item);
                flag = true;
            }
            return tempList;
        }
        public int Time_Span(params DateTime[] list)
        {
            if (list.Length == 1)
            {
                return (int)(DateTime.Now - list[0]).TotalDays + 1;
            }
            else
            {
                return (int)(list[1] - list[0]).TotalDays + 1;
            }
        }

        public void DiaryChangeToOccuped(HostingUnit hostingUnit, GuestRequest guest)
        {
            for (var date = guest.EntryDate; date < guest.ReleaseDate; date = date.AddDays(1))
            {
                hostingUnit.Diary[date.Month, date.Day] = true;
            }
        }

        public List<HostingUnit> HostingUnitPerHost(Host h)
        {
            List<HostingUnit> HostingUnitList = new List<HostingUnit>();

            foreach (var item in d.GetHostingUnitList())
            {
                if (item.Owner.HostKey == h.HostKey)
                {
                    HostingUnitList.Add(item);
                }
            }
            return HostingUnitList;
        }
        #endregion
        #region Grouping
        public IGrouping<Area, GuestRequest> GetGuestReqGroupByArea(bool sorted = false)
        {
            return (IGrouping<Area, GuestRequest>)from gs in d.GetGuestRequestList()
                                                  group gs by gs.area;
        }

        public IGrouping<int, GuestRequest> GetGuestRequestGroupByPersons(bool sorted = false)
        {
            return (IGrouping<int, GuestRequest>)from gs in d.GetGuestRequestList()
                                                 group gs by gs.TotalNumPersons;
        }

        public IGrouping<int, Host> GetHostGroupByNumofHU(bool sorted = false)
        {
            return (IGrouping<int, Host>)from host in d.GetHostsList()
                                         group host by HostingUnitPerHost(host).Count();
        }

        public IGrouping<Area, HostingUnit> GetHUGroupByArea(bool sorted = false)
        {
            return (IGrouping<Area, HostingUnit>)from hu in d.GetHostingUnitList()
                                                             group hu by hu.area;
        }

        public IEnumerable<GuestRequest> GuestRequestList()
        {
            return xml.GuestRequestList();
        }

        public bool SendGuestToHost (GuestRequest guest, HostingUnit hosting)
        {
            if ((guest.jacuzzi != Jaccuzzi.Option && guest.jacuzzi != hosting.jacuzzi)
                || (guest.pool != Pool.Option && guest.pool != hosting.pool)
                || (guest.childrenAttractions != ChildrensAttractions.Option && guest.childrenAttractions != hosting.childrenAttractions)
                || (guest.garden != Garden.Option && guest.garden != hosting.garden)
                || (guest.area != Area.All && guest.area != hosting.area)
                || (guest.type != Type.All && guest.type != hosting.type)
                || !CheckDatesAvailable(hosting, guest)
                )
                return false;
            return true;
        }

        public IEnumerable<GuestRequest> GetGuestRequestList(Func<GuestRequest, bool> predicat = null)
        {
            throw new NotImplementedException();
        }
        //public IEnumerable<HostingUnit> GetHostingUnitList(Func<HostingUnit, bool> predicat = null)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
    }
}
