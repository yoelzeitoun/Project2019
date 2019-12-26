using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class BankBranch
    {
        public int Bank_Code { get; set; }
        public string Bank_Name { get; set; }
        public int Branch_Code { get; set; }
        public string Branch_Address { get; set; }
        public string Branch_City { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
