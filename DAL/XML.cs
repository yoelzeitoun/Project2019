using BE;
using DS;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DAL
{
    public class XML:IDal
    {
        XElement hostRoot;
        string hostPath = @"login_XML.xml";

        public XML()
        {
            if (!File.Exists(hostPath))
                CreateFiles();
            else
                LoadData();
        }

        private void CreateFiles()
        {
            hostRoot = new XElement("hosts");
            hostRoot.Save(hostPath);
        }
        private void LoadData()
        {
            try
            {
                hostRoot = XElement.Load(hostPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        #region HOST
        public void AddHost(Host host)
        {
            XElement hostKey = new XElement("hostKey", host.HostKey);
            XElement email = new XElement("eMail", host.MailAddress);
            XElement password = new XElement("password", host.Password);
            XElement login = new XElement("login", email, password);
            XElement firstName = new XElement("firstName", host.FirstName);
            XElement lastName = new XElement("lastName", host.LastName);
            XElement phoneNumber = new XElement("phoneNumber", host.PhoneNumber);
            XElement name = new XElement("name", firstName, lastName, phoneNumber);
            XElement hostingUnits = new XElement("hosting-units");

            hostRoot.Add(new XElement("host", hostKey, login, name, hostingUnits));
            hostRoot.Save(hostPath);
        }
        public bool RemoveHost(int id)
        {
            XElement hostElement;
            try
            {
                hostElement = (from item in hostRoot.Elements()
                                  where int.Parse(item.Element("hostKey").Value) == id
                                  select item).FirstOrDefault();
                hostElement.Remove();
                hostRoot.Save(hostPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void UpdateHost(Host host)
        {
            XElement hostElement = (from item in hostRoot.Elements()
                                       where int.Parse(item.Element("id").Value) == host.HostKey
                                       select item).FirstOrDefault();

            hostElement.Element("login").Element("eMail").Value = host.MailAddress;
            hostElement.Element("login").Element("password").Value = host.Password;
            hostElement.Element("name").Element("firstName").Value = host.FirstName;
            hostElement.Element("name").Element("lastName").Value = host.LastName;

            hostRoot.Save(hostPath);
        }
        public void SaveHostList(List<Host> HostList)
        {
            hostRoot = new XElement("hosts",
                                    from p in HostList
                                    select new XElement("host",
                                                new XElement("hostKey", p.HostKey),
                                                new XElement("login",
                                                    new XElement("eMail", p.MailAddress),
                                                    new XElement("password", p.Password)),
                                                new XElement("name",
                                                    new XElement("firstName", p.FirstName),
                                                    new XElement("lastName", p.LastName)))
                                                        );
            hostRoot.Save(hostPath);
        }
        public List<Host> GetHostList()
        {
            LoadData();
            List<Host> hosts;
            try
            {
                hosts = (from item in hostRoot.Elements()
                         select new Host()
                         {
                             HostKey = int.Parse(item.Element("hostKey").Value),
                             MailAddress = item.Element("login").Element("eMail").Value,
                             Password = item.Element("login").Element("password").Value,
                             FirstName = item.Element("name").Element("firstName").Value,
                             LastName = item.Element("name").Element("lastName").Value
                         }).ToList();
            }
            catch
            {
                hosts = null;
            }
            return hosts;
        }
        public bool CheckPass(string email, string password)
        {
            LoadData();
            bool host;
            try
            {
                host = (from item in hostRoot.Elements()
                           where item.Element("login").Element("eMail").Value == email && item.Element("login").Element("password").Value== password
                           select item
                           ).Any();
            }
            catch
            {
                host = false;
                return host;
            }
            return host;
        }
        public bool IsHostExists(string email)
        {
            LoadData();
            bool host;
            try
            {
                host = (from item in hostRoot.Elements()
                           where item.Element("login").Element("eMail").Value == email
                           select item).Any();
            }
            catch
            {
                host = false;
                return host;
            }
            return host;
        }
        public Host GetHost(string email)
        {
            LoadData();
            Host host;
            try
            {
                host = (from item in hostRoot.Elements()
                        where item.Element("login").Element("eMail").Value == email
                        select new Host()
                        {
                            HostKey = int.Parse(item.Element("hostKey").Value),
                            MailAddress = item.Element("login").Element("eMail").Value,
                            Password = item.Element("login").Element("password").Value,
                            FirstName = item.Element("name").Element("firstName").Value,
                            LastName = item.Element("name").Element("lastName").Value
                        }).FirstOrDefault();
            }
            catch
            {
                host = null;
            }
            return host;
        }
        public string GetHostName(int id)
        {
            LoadData();
            string hostName;
            try
            {
                hostName = (from item in hostRoot.Elements()
                            where int.Parse(item.Element("hostKey").Value) == id
                            select item.Element("name").Element("firstName").Value
                                   + " " + item.Element("name").Element("lastName").Value
                            ).FirstOrDefault();
            }
            catch
            {
                hostName = null;
            }
            return hostName;
        }
        #endregion

        #region Hosting Unit
        public bool IsHostingUnitExists(HostingUnit hostingUnit)
        {
            LoadData();
            bool hosting;
            try
            {
                XElement host = (from item in hostRoot.Elements()
                                 where item.Element("login").Element("eMail").Value == hostingUnit.Owner.MailAddress
                                 select item.Element("hosting-units")).FirstOrDefault();

                hosting = (from item1 in host.Elements()
                           where item1.Element("name").Value == hostingUnit.HostingUnitName
                           select item1).Any();
            }
            catch
            {
                hosting = false;
                return hosting;
            }
            return hosting;
        }
        public void AddHostingUnit(HostingUnit hostingUnit)
        {
            if (!IsHostingUnitExists(hostingUnit))
            {
                try
                {
                    XElement hostElement = (from item in hostRoot.Elements()
                                            where item.Element("login").Element("eMail").Value == hostingUnit.Owner.MailAddress
                                            select item.Element("hosting-units")).FirstOrDefault();

                    hostElement.Add(new XElement("hosting-unit",
                                                new XElement("name", hostingUnit.HostingUnitName),
                                                new XElement("city", hostingUnit.City),
                                                new XElement("house-number", hostingUnit.HouseNumber),
                                                new XElement("street", hostingUnit.Street),
                                                new XElement("num-of-adults", Convert.ToString(hostingUnit.NumOfAdults)),
                                                new XElement("num-of-children", Convert.ToString(hostingUnit.NumOfChildren)),
                                                new XElement("price-of-adult", Convert.ToString(hostingUnit.PriceForAdult)),
                                                new XElement("price-of-child", Convert.ToString(hostingUnit.PriceForChild)),
                                                new XElement("area", hostingUnit.area.ToString()),
                                                new XElement("hosting-unit-type", hostingUnit.type.ToString()),
                                                new XElement("children-attractions", hostingUnit.childrenAttractions.ToString()),
                                                new XElement("garden", hostingUnit.garden.ToString()),
                                                new XElement("jaccuzi", hostingUnit.jacuzzi.ToString()),
                                                new XElement("pool", hostingUnit.pool.ToString()),
                                                new XElement("pictures")));

                    hostElement = (from item in hostElement.Elements()
                                   where item.Element("name").Value == hostingUnit.HostingUnitName
                                   select item).FirstOrDefault();
                    for (int i = 0; i < 10; i++)
                    {
                        hostElement.Element("pictures").Add(new XElement("pic" + i, hostingUnit.Pictures[i]));
                    }

                    hostRoot.Save(hostPath);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            else UpdateHostingUnit(hostingUnit);
        }
        public void UpdateHostingUnit(HostingUnit hostingUnit)
        {
            XElement hostElement = (from item in hostRoot.Elements()
                             where item.Element("login").Element("eMail").Value == hostingUnit.Owner.MailAddress
                             select item.Element("hosting-units")).FirstOrDefault();
            XElement hostingUnitElement = (from item1 in hostElement.Elements()
                                           where item1.Element("name").Value == hostingUnit.HostingUnitName
                                           select item1
                                           ).FirstOrDefault();

            hostingUnitElement.Element("name").Value = hostingUnit.HostingUnitName;
            hostingUnitElement.Element("city").Value = hostingUnit.City;
            hostingUnitElement.Element("house-number").Value = hostingUnit.HouseNumber;
            hostingUnitElement.Element("street").Value = hostingUnit.Street;
            hostingUnitElement.Element("num-of-adults").Value = hostingUnit.NumOfAdults.ToString();
            hostingUnitElement.Element("num-of-children").Value = hostingUnit.NumOfChildren.ToString();
            hostingUnitElement.Element("price-of-adult").Value = hostingUnit.PriceForAdult.ToString();
            hostingUnitElement.Element("price-of-child").Value = hostingUnit.PriceForChild.ToString();
            hostingUnitElement.Element("area").Value = hostingUnit.area.ToString();
            hostingUnitElement.Element("hosting-unit-type").Value = hostingUnit.type.ToString();
            hostingUnitElement.Element("children-attractions").Value = hostingUnit.childrenAttractions.ToString();
            hostingUnitElement.Element("garden").Value = hostingUnit.garden.ToString();
            hostingUnitElement.Element("jaccuzi").Value = hostingUnit.jacuzzi.ToString();
            hostingUnitElement.Element("pool").Value = hostingUnit.pool.ToString();
            for (int i = 0; i < 10; i++)
            {
                hostingUnitElement.Element("pictures").Element("pic" + i).Value = hostingUnit.Pictures[i];
            }
            hostRoot.Save(hostPath);
        }
        public bool DeleteHostingUnit(string email, string hu)
        {
            XElement hostingUnitElement;
            try
            {
                XElement hostElement = (from item in hostRoot.Elements()
                                        where item.Element("login").Element("eMail").Value == email
                                        select item.Element("hosting-units")).FirstOrDefault();
                hostingUnitElement = (from item1 in hostElement.Elements()
                                      where item1.Element("name").Value == hu
                                      select item1
                                      ).FirstOrDefault();
                hostElement.Remove();
                hostRoot.Save(hostPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public HostingUnit GetHostingUnit(string email, string hu)
        {
            LoadData();
            HostingUnit hostingUnit;
            try
            {
                var selectHost = (from item in hostRoot.Elements()
                                  where item.Element("login").Element("eMail").Value == email
                                  select item.Element("hosting-units")).FirstOrDefault();

                hostingUnit = (from item in selectHost.Elements()
                               where item.Element("name").Value == hu
                               select new HostingUnit()
                               {
                                   HostingUnitName = item.Element("name").Value,
                                   City = item.Element("city").Value,
                                   HouseNumber = item.Element("house-number").Value,
                                   Street = item.Element("street").Value,
                                   NumOfAdults = int.Parse(item.Element("num-of-adults").Value),
                                   NumOfChildren = int.Parse(item.Element("num-of-children").Value),
                                   PriceForAdult = int.Parse(item.Element("price-of-adult").Value),
                                   PriceForChild = int.Parse(item.Element("price-of-child").Value),
                                   area = (Area)Enum.Parse(typeof(Area), item.Element("area").Value, true),
                                   type = (Type)Enum.Parse(typeof(Type), item.Element("hosting-unit-type").Value, true),
                                   childrenAttractions = (ChildrensAttractions)Enum.Parse(typeof(ChildrensAttractions), item.Element("children-attractions").Value, true),
                                   garden = (Garden)Enum.Parse(typeof(Garden), item.Element("garden").Value, true),
                                   jacuzzi = (Jaccuzzi)Enum.Parse(typeof(Jaccuzzi), item.Element("jaccuzi").Value, true),
                                   pool = (Pool)Enum.Parse(typeof(Pool), item.Element("pool").Value, true)
                                   //Pictures = item.Element("pictures").Element("pic0").Value
                               }).FirstOrDefault();
            }
            catch
            {
                hostingUnit = null;
            }
            return hostingUnit;
        }

        public IEnumerable<string> HostingUnitList(string email)
        {
            LoadData();
            IEnumerable<string> hostingUnitName;
            try
            {
                var selectHost = (from item in hostRoot.Elements()
                                  where item.Element("login").Element("eMail").Value == email
                                  select item.Element("hosting-units")).FirstOrDefault();


                hostingUnitName = (from item in selectHost.Elements()
                                   select item.Element("name").Value
                                   ).ToList();
            }
            catch
            {
                hostingUnitName = null;
            }
            return hostingUnitName;
        }
        #endregion

        #region fonctions
        public void AddGuestRequest(GuestRequest guestRequest)
        {
            throw new NotImplementedException();
        }

        public void UpdateGuestRequest(GuestRequest guestRequest)
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

        

        public IEnumerable<Host> GetHostsList(Func<Host, bool> predicat = null)
        {
            throw new NotImplementedException();
        }

        public void AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOrder(Order order)
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

        public GuestRequest FindGuestRequest(Order order)
        {
            throw new NotImplementedException();
        }

        public HostingUnit FindHostingUnit(Order order)
        {
            throw new NotImplementedException();
        }

        public Host FindHost(Order order)
        {
            throw new NotImplementedException();
        }

        public void DiaryChangeToOccuped(HostingUnit hu, GuestRequest gs)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HostingUnit> GetHostingUnitList(Func<HostingUnit, bool> predicat = null)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}