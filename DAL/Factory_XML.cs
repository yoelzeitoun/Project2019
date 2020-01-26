using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Factory_XML
    {
        public static XML GetXML()
        {
            return XML.GetMyXML();
        }
    }
}
