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
        private static XML instance = null;
        public static XML GetMyXML()
        {
            if (instance == null)
            {
                instance = new XML();
            }
            return instance;
        }
        XElement hostRoot;
        XElement guestRoot;
        XElement orderRoot;
        XElement configRoot;
        string hostPath = @"login_XML.xml";
        string guestPath = @"guest_XML.xml";
        string orderPath = @"order_XML.xml";
        string configPath = @"config_XML.xml";
        public XML()
        {
            if (!File.Exists(hostPath))
                CreateHostFiles();
            else
                LoadHostData();

            if (!File.Exists(guestPath))
                CreateGuestFiles();
            else
                LoadGuestData();

            if (!File.Exists(orderPath))
                CreateOrderFiles();
            else
                LoadOrderData();

            if (!File.Exists(configPath))
                CreateConfigFiles();
            else
                LoadConfigData();
        }

        private void CreateHostFiles()
        {
            hostRoot = new XElement("hosts");
            hostRoot.Save(hostPath);
        }
        private void LoadHostData()
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
        private void CreateGuestFiles()
        {
            guestRoot = new XElement("guests");
            guestRoot.Save(guestPath);
        }
        private void LoadGuestData()
        {
            try
            {
                guestRoot = XElement.Load(guestPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        private void CreateOrderFiles()
        {
            orderRoot = new XElement("orders");
            orderRoot.Save(orderPath);
        }

        private void LoadOrderData()
        {
            try
            {
                orderRoot = XElement.Load(orderPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        private void CreateConfigFiles()
        {
            configRoot = new XElement("Configuration",
                new XElement("HostKey", Configuration._HostKey),
                new XElement("GuestRequestKey", Configuration._GuestRequestKey),
                new XElement("HostingUnitKey", Configuration._HostingUnitKey),
                new XElement("OrderKey", Configuration._OrderKey),
                new XElement("NumStaticOrderExpiration", Configuration.NumStaticOrderExpiration),
                new XElement("Commission", Configuration.Commission),
                new XElement("Admin_PASSWORD", Configuration.Admin_PASSWORD));
            configRoot.Save(configPath);
        }

        private void LoadConfigData()
        {
            try
            {
                configRoot = XElement.Load(configPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        #region Configuration
        public void UpdateConfiguration()
        {

        }
        #endregion
        #region Order
        public void AddOrder(Order order)
        {
            XElement HostingUnitKey = new XElement("hostingUnitKey", order.HostingUnitKey);
            XElement GuestRequestKey = new XElement("guestRequestKey", order.GuestRequestKey);
            XElement OrderKey = new XElement("orderKey", order.OrderKey);
            XElement status_Order = new XElement("status_Order", order.status_Order);
            XElement CreateDate = new XElement("createDate", order.CreateDate);
            XElement OrderDate = new XElement("orderDate", order.OrderDate);

            hostRoot.Add(new XElement("order", HostingUnitKey, GuestRequestKey, OrderKey, status_Order, CreateDate, OrderDate));
            hostRoot.Save(orderPath);
        }
        #endregion
        #region HOST
        public void SetHostKey(Host host)
        {
            LoadConfigData();
            int newValue = int.Parse(configRoot.Element("HostKey").Value) + 1;
            configRoot.Element("HostKey").Value = newValue.ToString();
            host.HostKey = newValue;
            configRoot.Save(configPath);
        }
        public void SetHostingUnitKey(HostingUnit hostingUnit)
        {
            LoadConfigData();
            int newValue = int.Parse(configRoot.Element("HostingUnitKey").Value) + 1;
            configRoot.Element("HostingUnitKey").Value = newValue.ToString();
            hostingUnit.HostingUnitKey = newValue;
            configRoot.Save(configPath);
        }
        public void SetGuestRequestKey(GuestRequest guest)
        {
            LoadConfigData();
            int newValue = int.Parse(configRoot.Element("GuestRequestKey").Value) + 1;
            configRoot.Element("GuestRequestKey").Value = newValue.ToString();
            guest.GuestRequestKey = newValue;
            configRoot.Save(configPath);
        }
        public void SetOrderKey(Order order)
        {
            LoadConfigData();
            int newValue = int.Parse(configRoot.Element("OrderKey").Value) + 1;
            configRoot.Element("OrderKey").Value = newValue.ToString();
            order.OrderKey = newValue;
            configRoot.Save(configPath);
        }
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
            LoadHostData();
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
            LoadHostData();
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
            LoadHostData();
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
            LoadHostData();
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
            LoadHostData();
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
            LoadHostData();
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
                                                new XElement("HostingUnitKey", hostingUnit.HostingUnitKey),
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
                                                new XElement("diary", SetDiary(hostingUnit)),
                                                new XElement("pictures")));

                    var hostingElement = (from item in hostElement.Elements()
                                   where item.Element("name").Value == hostingUnit.HostingUnitName
                                   select item).FirstOrDefault();
                    for (int i = 0; i < 10; i++)
                    {
                        hostingElement.Element("pictures").Add(new XElement("pic" + i, hostingUnit.Pictures[i]));
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
            LoadHostData();
            var hostElement = (from item in hostRoot.Elements()
                             where item.Element("login").Element("eMail").Value == hostingUnit.Owner.MailAddress
                             select item.Element("hosting-units")).FirstOrDefault();
            var hostingUnitElement = (from item1 in hostElement.Elements()
                                           where item1.Element("name").Value == hostingUnit.HostingUnitName
                                           select item1
                                           ).FirstOrDefault();
            hostingUnitElement.Element("HostingUnitKey").Value = hostingUnit.HostingUnitKey.ToString();
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
            hostingUnitElement.Element("diary").Value = SetDiary(hostingUnit);
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
                                      select item1).FirstOrDefault();
                hostingUnitElement.Remove();
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
            LoadHostData();
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
                                   Owner = GetHost(email),
                                   HostingUnitKey = int.Parse(item.Element("HostingUnitKey").Value),
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
                                   pool = (Pool)Enum.Parse(typeof(Pool), item.Element("pool").Value, true),
                                   Diary = GetDiary(item.Element("diary").Value),
                                   Pictures = (from pic in item.Element("pictures").Elements()
                                               select pic.Value ).ToArray() 
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
            LoadHostData();
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
        #region guestRequest
        public void AddGuestRequest(GuestRequest guestRequest)
        {
            XElement guestRequestkey = new XElement("GuestRequestKey", guestRequest.GuestRequestKey);
            XElement email = new XElement("eMail", guestRequest.MailAddress);
            XElement firstName = new XElement("firstName", guestRequest.FirstName);
            XElement lastName = new XElement("lastName", guestRequest.LastName);
            XElement phoneNumber = new XElement("phoneNumber", guestRequest.PhoneNumber);
            XElement name = new XElement("name", firstName, lastName, phoneNumber);
            XElement pool = new XElement("pool", guestRequest.pool.ToString());
            XElement childrenAttractions = new XElement("childrenAttractions", guestRequest.childrenAttractions.ToString());
            XElement jaccuzzi = new XElement("jaccuzzi", guestRequest.jacuzzi.ToString());
            XElement garden = new XElement("garden", guestRequest.garden.ToString());
            XElement area = new XElement("area", guestRequest.area.ToString());
            XElement hostingUnitType = new XElement("hostingUnitType", guestRequest.type.ToString());
            XElement options = new XElement("options", pool, childrenAttractions, jaccuzzi, garden, hostingUnitType, area);
            XElement entryDate = new XElement("entryDate", guestRequest.EntryDate.ToString("dd/MM/yyyy"));
            XElement releaseDate = new XElement("releaseDate", guestRequest.ReleaseDate.ToString("dd/MM/yyyy"));
            XElement registrationDate = new XElement("registrationDate", guestRequest.RegistrationDate.ToString("dd/MM/yyyy"));
            XElement date = new XElement("date", entryDate, releaseDate, registrationDate);
            XElement numAdults = new XElement("numAdults", guestRequest.NumAdults);
            XElement numChildren = new XElement("numChildren", guestRequest.NumChildren);
            XElement totalNumPersons = new XElement("totalNumPersons", guestRequest.TotalNumPersons);
            XElement numbers = new XElement("numbers", numAdults, numChildren, totalNumPersons);

            guestRoot.Add(new XElement("GuestRequest", guestRequestkey, email, name, date, options,numbers));
            guestRoot.Save(guestPath);
        }
        public IEnumerable<GuestRequest> GuestRequestList()
        {
            LoadGuestData();
            IEnumerable<GuestRequest> guestRequestList;
            try
            {
                guestRequestList = (from item in guestRoot.Elements()
                                  select new GuestRequest()
                                  {
                                      GuestRequestKey = int.Parse(item.Element("GuestRequestKey").Value),
                                      MailAddress = item.Element("eMail").Value,
                                      FirstName = item.Element("name").Element("firstName").Value,
                                      LastName = item.Element("name").Element("lastName").Value,
                                      PhoneNumber = item.Element("name").Element("phoneNumber").Value,
                                      EntryDate = DateTime.Parse(item.Element("date").Element("entryDate").Value),
                                      ReleaseDate = DateTime.Parse(item.Element("date").Element("releaseDate").Value),
                                      RegistrationDate = DateTime.Parse(item.Element("date").Element("registrationDate").Value),
                                      pool = (Pool)Enum.Parse(typeof(Pool), item.Element("options").Element("pool").Value, true),
                                      childrenAttractions = (ChildrensAttractions)Enum.Parse(typeof(ChildrensAttractions), item.Element("options").Element("childrenAttractions").Value, true),
                                      jacuzzi = (Jaccuzzi)Enum.Parse(typeof(Jaccuzzi), item.Element("options").Element("jaccuzzi").Value, true),
                                      garden = (Garden)Enum.Parse(typeof(Garden), item.Element("options").Element("garden").Value, true),
                                      type = (Type)Enum.Parse(typeof(Type), item.Element("options").Element("hostingUnitType").Value, true),
                                      area = (Area)Enum.Parse(typeof(Area), item.Element("options").Element("area").Value, true),
                                      NumAdults = int.Parse(item.Element("numbers").Element("numAdults").Value),
                                      NumChildren = int.Parse(item.Element("numbers").Element("numChildren").Value),
                                  }
                                  ).ToList();
            }
            catch
            {
                guestRequestList = null;
            }
            return guestRequestList;
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
        #endregion

        #region fonctions

        public string SetDiary(HostingUnit hostingUnit)
        {
            string result = "";
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 31; j++)
                {
                    if (hostingUnit.Diary[i, j] == true)
                        result += 1 + ",";
                    else result += 0 + ",";
                }
            return result;
        }
        public bool[,] GetDiary( string strDiary)
        {
            bool[,] diary = new bool[12, 31];
            int index = 0;
            string[] strArr = strDiary.Split(',');
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 31; j++)
                {
                    if (int.Parse(strArr[index++]) == 1)
                        diary[i, j] = true;
                    else diary[i, j] = false;
                }
            return diary;
        }

        public IEnumerable<Host> GetHostsList(Func<Host, bool> predicat = null)
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

        public void DiaryChangeToOccuped(HostingUnit hostingUnit, GuestRequest guest)
        {
            LoadHostData();
            for (var date = guest.EntryDate; date < guest.ReleaseDate; date = date.AddDays(1))
            {
                hostingUnit.Diary[date.Month, date.Day] = true;
            }
        }

        public IEnumerable<HostingUnit> GetHostingUnitList()
        {
            LoadHostData();
            IEnumerable<HostingUnit> hostingUnitName = new List<HostingUnit>();
            try
            {
                var selectHost = (from item in hostRoot.Elements()

                                  select item.Element("hosting-units")).ToList();


                hostingUnitName = (from item in selectHost.Elements()
                                   select new HostingUnit()
                                   {
                                       //  HostingUnitKey=long.Parse(item.Element("HostingUnitKey").Value),
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
                                       pool = (Pool)Enum.Parse(typeof(Pool), item.Element("pool").Value, true),
                                       Pictures = (from pic in item.Element("pictures").Elements()
                                                   select pic.Value).ToArray()

                                   }
                                   ).ToList();
            }
            catch
            {
                hostingUnitName = null;
            }
            return hostingUnitName;

        }
    

    public bool SendGuestToHost(GuestRequest guest, HostingUnit hosting)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}