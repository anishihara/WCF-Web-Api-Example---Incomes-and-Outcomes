using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IncomesAndOutcomes_API.Models
{
    public class UserSession
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public DateTime OpenSessionTime { get; set; }
        public DateTime? EndSessionTime { get; set; }
    }
}