using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.IO;


namespace BE
{
    public static class HelpClass
    {
        // static generic function wich show all the properties for each class
        public static string ToStringProperty<T>(this T t)
        {
            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties())
            {
                if ((item.Name != "HostingUnitKey") && (item.Name != "Pictures") && (item.Name != "DebitAuthorization") && (item.Name != "Diary") && (item.Name != "MailAddress") && (item.Name != "Password") && (item.Name != "Owner"))
                {
                    str += InsertSpaces(item.Name) + ": " + item.GetValue(t, null) + "\n";
                }
            }
            return str;
        }
        // insert spaces into properties names
        public static string InsertSpaces(string str)
        {
            for (int i = 1; i < str.Length; i++)
            {
                if (char.IsUpper(str[i]))
                {
                    str = str.Substring(0, i) + ' ' + str.Substring(i++);
                }
            }
            return str;
        }
    }
}