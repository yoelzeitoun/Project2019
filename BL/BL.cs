﻿using System;
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
            Order order = DataSource.orderList.FirstOrDefault(o => o.HostingUnitKey == hostingUnit.HostingUnitKey);
            if (order.status_Order== Status_order.In_progress)
                throw new Exception("This hosting Unit has an open order!");
            if (d.DeleteHostingUnit(hostingUnit))
                return true;
            return false;
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
            //faut faire
            throw new NotImplementedException();
        }

        public void UpdateHostingUnit(HostingUnit hostingUnit)
        {
            HostingUnit hu = DataSource.hostingUnitList.FirstOrDefault(h => h.HostingUnitKey == hostingUnit.HostingUnitKey);
            Order order = DataSource.orderList.FirstOrDefault(o => o.HostingUnitKey == hostingUnit.HostingUnitKey);
            //if an order is in progress we can't cancel the Debit Authorisation
            if (!hostingUnit.DebitAuthorization && hu.DebitAuthorization && order.status_Order==Status_order.In_progress)
                throw new Exception("You can't cancel the Debit Authorisation as an order is in progress!");
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
    }
}