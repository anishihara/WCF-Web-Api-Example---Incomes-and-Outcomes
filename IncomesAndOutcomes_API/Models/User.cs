using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncomesAndOutcomes_API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}