using System;
using System.Collections.Generic;
using System.Text;
using BE;


namespace DAL
{
    public static class Cloning
    {
        public static BankBranch Clone(this BankBranch original)
        {
            BankBranch target = new BankBranch();
            target.Bank_Code = original.Bank_Code;
            target.Bank_Name = original.Bank_Name;
            target.Branch_Code = original.Branch_Code;
            target.Branch_Address = original.Branch_Address;
            target.Branch_City = original.Branch_City;
            return target;
        }

        public static GuestRequest Clone(this GuestRequest original)
        {
            GuestRequest target = new GuestRequest();
            target.GuestRequestKey = original.GuestRequestKey;
            target.FirstName = original.FirstName;
            target.LastName = original.LastName;
            target.MailAddress = original.MailAddress;
            target.PhoneNumber = original.PhoneNumber;
            target.status_Client = original.status_Client;
            target.RegistrationDate = original.RegistrationDate;
            target.EntryDate = original.EntryDate;
            target.ReleaseDate = original.ReleaseDate;
            target.area = original.area;
            target.type = original.type;
            target.NumAdults = original.NumAdults;
            target.NumChildren = original.NumChildren;
            target.pool = original.pool;
            target.jacuzzi = original.jacuzzi;
            target.garden = original.garden;
            target.childrenAttractions = original.childrenAttractions;
            return target;
        }

        public static Host Clone(this Host original)
        {
            Host target = new Host();
            target.HostKey = original.HostKey;
            target.FirstName = original.FirstName;
            target.LastName = original.LastName;
            target.PhoneNumber = original.PhoneNumber;
            target.MailAddress = original.MailAddress;
            target.bankBranchDetails = original.bankBranchDetails;
            target.BankAccountNumber = original.BankAccountNumber;
            target.CollectionClearance = original.CollectionClearance;
            return target;
        }

        public static HostingUnit Clone(this HostingUnit original)
        {
            HostingUnit target = new HostingUnit();
            target.HostingUnitKey = original.HostingUnitKey;
            target.NumHostingUnit = original.NumHostingUnit;
            target.Owner = original.Owner;
            target.HostingUnitName = original.HostingUnitName;
            target.Diary = original.Diary;
            return target;

        }
        public static Order Clone(this Order original)
        {
            Order target = new Order();
            target.HostingUnitKey = original.HostingUnitKey;
            target.GuestRequestKey = original.GuestRequestKey;
            target.OrderKey = original.OrderKey;
            target.status_Order = original.status_Order;
            target.CreateDate = original.CreateDate;
            target.OrderDate = original.OrderDate;
            return target;
        }
    }
}