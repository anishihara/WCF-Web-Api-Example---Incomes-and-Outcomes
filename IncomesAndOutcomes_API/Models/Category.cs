using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IncomesAndOutcomes.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }

        public string Name { get; set; }
        public float Balance { get; set; }
        public bool IsDeleted { get; set; }
    }
}
