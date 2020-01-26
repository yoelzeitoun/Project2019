using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class HostingUnit
    {
        public long HostingUnitKey { get; set; }
        public string HostingUnitName { get; set; }
        public Area area { get; set; }
        public Type type { get; set; }
        public Pool pool { get; set; }
        public Jaccuzzi jacuzzi { get; set; }
        public Garden garden { get; set; }
        public ChildrensAttractions childrenAttractions { get; set; }
        public int NumOfAdults { get; set; }
        public int PriceForAdult { get; set; }
        public int PriceForChild { get; set; }
        public int NumOfChildren { get; set; }
        public int NumTotalPerson
        { get { return NumOfAdults + NumOfChildren;} }
        public Host Owner{ get; set; }
        public string City { get; set; }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string[] Pictures { get; set; }
        public bool DebitAuthorization { get; set; }
        public bool[,] Diary = new bool[12, 31];
        
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
