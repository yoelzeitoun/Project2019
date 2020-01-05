using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Factory
    {
        public static IDal getDal()
        {
            return new Dal_imp();
        }
    }
}
