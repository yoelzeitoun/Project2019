using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class HostingUnit
    {
        public long HostingUnitKey = 10000000;
        Host Owner{ get; set; }
        public string HostingUnitName { get; set; }
        public bool[,] Diary { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
