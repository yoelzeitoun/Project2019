using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Factory_DAL
    {
        protected static XML instance = null;
        public static XML GetXML()
        {
            if (instance == null)
            {
                instance = new XML();
            }
            return instance;
        }
    }
}
