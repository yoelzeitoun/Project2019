using System;
using System.Collections.Generic;
using BE;
using DS;
using System.Linq;

namespace DAL
{
    public class Dal_imp : IDal
    {
        public void AddGuestRequest(GuestRequest guestRequest)
        {
            guestRequest.GuestRequestKey = ++BE.Configuration._GuestRequestKey;
            GuestRequest guestRequest1 = DataSource.guestRequestList.FirstOrDefault(t => t.GuestRequestKey == guestRequest.GuestRequestKey); ;
            if (guestRequest1 != null)
                throw new Exception("DAL: guestRequest with the same key already exists...");
            DataSource.guestRequestList.Add(guestRequest);
        }
        public GuestRequest GetGuestRequest(long guestRequestKey)
        {
            int index = DataSource.guestRequestList.FindIndex(t => t.GuestRequestKey == guestRequestKey);
            if (index == -1)
                throw new Exception("DAL: Gest Request with the same key not found...");
            return DataSource.guestRequestList.FirstOrDefault(t => t.GuestRequestKey == guestRequestKey);
        }
        public void UpdateGuestRequest(GuestRequest guestRequest)
        {
            int index = DataSource.guestRequestList.FindIndex(t => t.GuestRequestKey == guestRequest.GuestRequestKey);
            if (index == -1)
                throw new Exception("DAL: Guest Request with the same key not found...");

            DataSource.guestRequestList[index] = guestRequest;
        }

        public void AddHostingUnit(HostingUnit hostingUnit)
        {
            hostingUnit.HostingUnitKey = ++BE.Configuration._HostingUnitKey;
            HostingUnit hosting1 = DataSource.hostingUnitList.FirstOrDefault(t => t.HostingUnitKey == hostingUnit.HostingUnitKey);
            if (hosting1 != null)
                throw new Exception("DAL: hosting Unit with the same key already exists...");
            DataSource.hostingUnitList.Add(hostingUnit);
        }
        /// <summary>
        /// help function-return the hosting unit with the recieved hostingUnitKey
        /// </summary>
        /// <param name="hostingUnitKey"></param>
        /// <returns></returns>
        public HostingUnit GetHostingUnit(long hostingUnitKey)
        {
            long index = DataSource.hostingUnitList.FindIndex(t => t.HostingUnitKey == hostingUnitKey);
            if (index == -1)
                throw new Exception("DAL: Hosting Unit with the same key not found!");
            return DataSource.hostingUnitList.FirstOrDefault(t => t.HostingUnitKey == hostingUnitKey);
        }
        public bool DeleteHostingUnit(HostingUnit hostingUnit)
        {
            HostingUnit h = GetHostingUnit(hostingUnit.HostingUnitKey);
            // verify if this hosting unit has an order, so don't allow delete
            if (DataSource.orderList.Exists(hu => hu.HostingUnitKey == hostingUnit.HostingUnitKey))
                throw new Exception("DAL: Order has been determinated for this Hosting Unit!");

            return DataSource.hostingUnitList.Remove(h);
        }
        public void UpdateHostingUnit(HostingUnit hostingUnit)
        {
            int index = DataSource.hostingUnitList.FindIndex(t => t.HostingUnitKey == hostingUnit.HostingUnitKey);
            if (index == -1)
                throw new Exception("DAL: Hosting Unit with the same key not found!");

            DataSource.hostingUnitList[index] = hostingUnit;
        }
        public void AddOrder(Order order)
        {
            order.OrderKey = ++BE.Configuration._OrderKey;
            Order o = DataSource.orderList.FirstOrDefault(t1 => t1.OrderKey == order.OrderKey);
            if (o != null)
                throw new Exception("DAL: Order with the same key already exists!");
            if (!DataSource.guestRequestList.Exists(gs => gs.GuestRequestKey == order.GuestRequestKey))
                throw new Exception("DAL: Guest Request with this key not found!");
            if (!DataSource.hostingUnitList.Exists(ts => ts.HostingUnitKey == order.HostingUnitKey))
                throw new Exception("DAL: Hosting Unit with this key not found!");
            DataSource.orderList.Add(order);
        }
        public bool UpdateOrder(Order order)
        {
            int index = DataSource.orderList.FindIndex(t => t.OrderKey == order.OrderKey);
            if (index == -1)
                throw new Exception("DAL: Order with this key not found!");
            if (DataSource.orderList[index].GuestRequestKey != order.GuestRequestKey)
                throw new Exception("DAL: Guest Request key not match to the current order");
            if (DataSource.orderList[index].HostingUnitKey != order.HostingUnitKey)
                throw new Exception("DAL: Hosting Unit key not match to the current order");
            GuestRequest gs1 = DataSource.guestRequestList.FirstOrDefault(gs => gs.GuestRequestKey == order.GuestRequestKey);
            HostingUnit hu1 = DataSource.hostingUnitList.FirstOrDefault(hu => hu.HostingUnitKey == order.HostingUnitKey);
            
            order.CreateDate = DateTime.Now;
            DataSource.orderList[index] = order;

            return true;
        }

        //fonction that change the status of the dairy to "occuped"
        public void DiaryChangeToOccuped(HostingUnit hu, GuestRequest gs)
        {
            for (var date = gs.EntryDate; date < gs.ReleaseDate; date = date.AddDays(1))
            {
                hu.Diary[date.Month, date.Day] = true;
            }
        }
        public Order GetOrder(long orderKey)
        {
            int index = DataSource.orderList.FindIndex(o => o.OrderKey == orderKey);
            if (index == -1)
                throw new Exception("DAL: Order with this key not found!");
            return DataSource.orderList.FirstOrDefault(t => t.OrderKey == orderKey);
        }

        //public List<BankBranch> GetAllBanks()
        //{
        //    List<BankBranch> bankList = new List<BankBranch>
        //    {
        //        new BankBranch()
        //        {
        //            Bank_Code=12,
        //            Bank_Name="בנק הפועלים בעמ",
        //            Branch_Code=201,
        //            Branch_Address="עבד אל כרים קאסם 0",
        //            Branch_City="כפר קאסם"
        //        },
        //        new BankBranch()
        //        {
        //            Bank_Code = 12,
        //            Bank_Name="בנק הפועלים בע",
        //            Branch_Code=671,
        //            Branch_Address="בורנשטיין צבי 915",
        //            Branch_City="ירוחם"
        //        },
        //        new BankBranch()
        //        {
        //            Bank_Code = 12,
        //            Bank_Name = "בנק הפועלים בע",
        //            Branch_Code =203,
        //            Branch_Address="רחוב ראשי 0",
        //            Branch_City="ערערה"
        //        },
        //        new BankBranch()
        //        {
        //            Bank_Code = 12,
        //            Bank_Name="בנק הפועלים בע",
        //            Branch_Code=551,
        //            Branch_Address="רחוב ראשי 0",
        //            Branch_City="קלנסווה"
        //        },
        //        new BankBranch()
        //        {
        //            Bank_Code = 12,
        //            Bank_Name = "בנק הפועלים בע",
        //            Branch_Code =538,
        //            Branch_Address="כיסופים 801",
        //            Branch_City="ירושלים"
        //        }
        //    };
        //    return bankList;
        //}
        public IEnumerable<GuestRequest> GetGuestRequestList(Func<GuestRequest, bool> predicate = null)
        {
            var v = from item in DataSource.guestRequestList
                    select new GuestRequest(); //default constructor!!
            
            if (predicate == null)
                return v.AsEnumerable().OrderByDescending(s => s.LastName);

            return v.Where(predicate).OrderByDescending(s => s.LastName);
        }
        public IEnumerable<HostingUnit> GetHostingUnitList(Func<HostingUnit, bool> predicate = null)
        {
            var v = from item in DataSource.hostingUnitList
                    select new HostingUnit();//default constructor!!

            if (predicate == null)
                return v.AsEnumerable().OrderByDescending(s => s.HostingUnitName);

            return v.Where(predicate).OrderByDescending(s => s.HostingUnitName);
        }
        public IEnumerable<Order> GetOrderList(Func<Order, bool> predicat = null)
        {
            var v = from item in DataSource.orderList
                    select new Order();

            if (predicat == null)
                return v.AsEnumerable().OrderByDescending(s => s.OrderKey);

            return v.Where(predicat).OrderByDescending(s => s.OrderKey);
        }
        //Find the Host of an order
        public Host FindHost(Order order)
        {
            return DataSource.hostingUnitList.FirstOrDefault(h => h.HostingUnitKey == order.HostingUnitKey).Owner;
        }
        
        public GuestRequest FindGuestRequest(Order order)
        {
            return DataSource.guestRequestList.FirstOrDefault(g => g.GuestRequestKey == order.GuestRequestKey);
        }
        public HostingUnit FindHostingUnit(Order order)
        {
            return DataSource.hostingUnitList.FirstOrDefault(h => h.HostingUnitKey == order.HostingUnitKey);
        }

        public IEnumerable<Host> GetHostsList(Func<Host, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.hostlist.AsEnumerable();
            return DataSource.hostlist.Where(predicate).OrderByDescending(s=>s.HostKey);
        }

        public void AddHost(Host host)
        {
            throw new NotImplementedException();
        }

        public bool RemoveHost(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateHost(Host host)
        {
            throw new NotImplementedException();
        }

        public void SaveHostList(List<Host> HostList)
        {
            throw new NotImplementedException();
        }

        public List<Host> GetHostList()
        {
            throw new NotImplementedException();
        }

        public Host GetHost(int id)
        {
            throw new NotImplementedException();
        }

        public string GetHostName(int id)
        {
            throw new NotImplementedException();
        }

        public Host GetHost(string email)
        {
            throw new NotImplementedException();
        }
    }
}