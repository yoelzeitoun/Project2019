using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class HostingUnit
    {
        public long HostingUnitKey = 10000000;
        public long NumHostingUnit { get; set; }
        public Area area { get; set; }
        public Pool pool { get; set; }
        public Jaccuzzi jacuzzi { get; set; }
        public Garden garden { get; set; }
        public ChildrensAttractions childrenAttractions { get; set; }
        public Host Owner{ get; set; }
        public string HostingUnitName { get; set; }
        public bool DebitAuthorization { get; set; }
        public bool[,] Diary = new bool[12, 31];
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
