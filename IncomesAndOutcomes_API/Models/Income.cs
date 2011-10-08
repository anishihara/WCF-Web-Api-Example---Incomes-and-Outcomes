using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IncomesAndOutcomes_API.Models
{
    public class Income
    {
        public int IncomeId { get; set;}
        public int AccountBalanceId { get; set; }
        public virtual AccountBalance AccountBalance { get; set; }
        public int MonthBufferId { get; set; }
        public virtual MonthBuffer MonthBuffer { get; set; }

        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public string Memo { get; set; }
        public bool IsDeleted { get; set; }
        public bool AvailableNextMonth { get; set; }
    }
}
