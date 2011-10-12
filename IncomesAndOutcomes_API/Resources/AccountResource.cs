using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncomesAndOutcomes_API.Resources
{
    public class AccountResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }
    }
}