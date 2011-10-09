﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IncomesAndOutcomes_API.Models
{
    public class Outcome
    {
        public int Id { get; set; }
        public int AccountBalanceId { get; set; }
        public virtual AccountBalance AccountBalance { get; set; }
        public int BudgetId { get; set; }
        public virtual Budget Budget { get; set; }

        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public string Memo { get; set; }
        public bool IsDeleted { get; set; }
    }
}
