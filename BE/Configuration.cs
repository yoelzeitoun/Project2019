using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public static class Configuration
    {
        public static long _HostKey = 0;
        public static long _GuestRequestKey = 0;
        public static long _HostingUnitKey = 0;
        public static int _OrderKey = 0;
        public static int NumStaticOrderExpiration = 0;
        public static int Commission = 10;
        public static string Admin_PASSWORD = "1";
    }
}