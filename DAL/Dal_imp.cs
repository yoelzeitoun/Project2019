using System;
using System.Collections.Generic;
using BE;
using DS;

namespace DAL 
{
    public class Dal_imp : Idal
    {
        public void addGuestRequest(GuestRequest guestRequest)
        {
            DataSource.gsList.Add(guestRequest);
        }

        public void addOrder(Order order)
        {
            DataSource.orderList.Add(order);
        }

        public void addUnit(HostingUnit hostingUnit)
        {
            DataSource.huList.Add(hostingUnit);
        }

        public List<BankBranch> allBanks()
        {
            List<BankBranch> bankList = new List<BankBranch>
            {
                new BankBranch()
                {
                    Bank_Code=12,
                    Bank_Name="בנק הפועלים בעמ",
                    Branch_Code=201,
                    Branch_Address="עבד אל כרים קאסם 0",
                    Branch_City="כפר קאסם"
                },
                new BankBranch()
                {
                    Bank_Code = 12,
                    Bank_Name="בנק הפועלים בע",
                    Branch_Code=671,
                    Branch_Address="בורנשטיין צבי 915",
                    Branch_City="ירוחם"
                },
                new BankBranch()
                {
                    Bank_Code = 12,
                    Bank_Name = "בנק הפועלים בע",
                    Branch_Code =203,
                    Branch_Address="רחוב ראשי 0",
                    Branch_City="ערערה"
                },
                new BankBranch()
                {
                    Bank_Code = 12,
                    Bank_Name="בנק הפועלים בע",
                    Branch_Code=551,
                    Branch_Address="רחוב ראשי 0",
                    Branch_City="קלנסווה"
                },
                new BankBranch()
                {
                    Bank_Code = 12,
                    Bank_Name = "בנק הפועלים בע",
                    Branch_Code =538,
                    Branch_Address="כיסופים 801",
                    Branch_City="ירושלים"
                }
            };
            return bankList;
        }

        public void allGuestRequests()
        {
            throw new NotImplementedException();
        }

        public void allOrders()
        {
            throw new NotImplementedException();
        }

        public void allUnits()
        {
            throw new NotImplementedException();
        }

        public void deleteUnit()
        {
            throw new NotImplementedException();
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
