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

            hostRoot.Add(new XElement("host", hostKey, login, name));
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
        public bool IsExists(string email, string password)
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
                            + " " +
                                item.Element("name").Element("lastName").Value
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
        public void AddHostingUnit(HostingUnit hostingUnit)
        {
            try
            {
                XElement hostElement = (from item in hostRoot.Elements("hosts").Elements("host")
                                        where item.Element("login").Element("eMail").Value == hostingUnit.Owner.MailAddress
                                        select item).FirstOrDefault();

                hostElement.Add(new XElement("hosting-unit", new XElement("city", hostingUnit.City),
                                                                           new XElement("house-number", hostingUnit.HouseNumber),
                                                                           new XElement("street", hostingUnit.Street),
                                                                           new XElement("num-of-adults", Convert.ToString(hostingUnit.NumOfAdults)),
                                                                           new XElement("num-of-children", Convert.ToString(hostingUnit.NumOfChildren)),
                                                                           new XElement("area", Enum.GetNames(typeof(Area)), hostingUnit.area),
                                                                           new XElement("hosting-unit-type", Enum.GetNames(typeof(Type)), hostingUnit.type),
                                                                           new XElement("children-attractions", Enum.GetNames(typeof(ChildrensAttractions)), hostingUnit.childrenAttractions),
                                                                           new XElement("garden", Enum.GetNames(typeof(Garden)), hostingUnit.garden),
                                                                           new XElement("jaccuzi", Enum.GetNames(typeof(Jaccuzzi)), hostingUnit.jacuzzi),
                                                                           new XElement("pool", Enum.GetNames(typeof(Pool)), hostingUnit.pool)));

                hostRoot.Save(hostPath);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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

        public bool DeleteHostingUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }

        public void UpdateHostingUnit(HostingUnit hostingUnit)
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
        #endregion
    }
}