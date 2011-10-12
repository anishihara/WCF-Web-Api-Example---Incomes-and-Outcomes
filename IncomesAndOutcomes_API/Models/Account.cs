﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncomesAndOutcomes_API.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        //public virtual ICollection<AccountBalance> AccountBalances { get; set; }
    }
}