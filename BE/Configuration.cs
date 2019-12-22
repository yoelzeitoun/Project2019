using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public static class Configuration
    {
        public static long NumStaticGuestRequest = 10000000;
        public static long NumStaticHostingUnit = 10000000;
        public static int NumStaticOrder = 1;
        public static int NumStaticOrderExpiration = 0;
        public static int Commission = 0;
        public static string WEB_LICENSE_MAIL;
        public static string MAIL_PASSWORD;
    }
}
