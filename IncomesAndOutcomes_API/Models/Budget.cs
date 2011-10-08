using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IncomesAndOutcomes_API.Models
{
    public class Budget
    {
        public int BudgetId { get; set; }
        public int MonthBufferId { get; set; }
        public virtual MonthBuffer MonthBuffer { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public ICollection<Outcome> Outcomes { get; set; }

        public float Amount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

    }
}
