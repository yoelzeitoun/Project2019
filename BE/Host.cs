using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Host
    {
        public int HostKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public BankBranch bankBranchDetails { get; set; }
        public int BankAccountNumber { get; set; }
        public bool CollectionClearance { get; set; }
        public long Total_commission { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
