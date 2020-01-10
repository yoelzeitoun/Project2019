using BE;
using DS;
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
    public class XML
    {
        XElement hostRoot;
        string hostPath = @"login_Xml.xml";

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
        public void AddHost(Host host)
        {
            XElement hostKey = new XElement("hostKey", host.HostKey);
            XElement email = new XElement("eMail", host.MailAddress);
            XElement password = new XElement("password", host.Password);
            XElement login = new XElement("login", email, password);
            XElement firstName = new XElement("firstName", host.FirstName);
            XElement lastName = new XElement("lastName", host.LastName);
            XElement name = new XElement("name", firstName, lastName);

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
        public void SaveHostLinq(List<Host> HostList)
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
        public Host GetHost(int id)
        {
            LoadData();
            Host host;
            try
            {
                host = (from item in hostRoot.Elements()
                        where int.Parse(item.Element("hostKey").Value) == id
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
    }
}