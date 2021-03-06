﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Host
    {
        public long HostKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public string Password { get; set; }
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
