using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace IncomesAndOutcomes.Models
{
    public class MonthBuffer
    {
        public int MonthBufferId { get; set; }
        public int? LastBufferId { get; set; }
        [ForeignKey("LastBufferId")]
        public virtual MonthBuffer LastBuffer { get; set; }
        public ICollection<Income> Incomes { get; set; }
        public ICollection<Budget> Budgets { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }
        public float Amount { get; set; }
    }
}
