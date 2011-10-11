using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IncomesAndOutcomes_API.Models
{
    public class IncomesAndOutcomes_APIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<IncomesAndOutcomes_API.Models.IncomesAndOutcomes_APIContext>());

        public DbSet<IncomesAndOutcomes_API.Models.Account> Accounts { get; set; }

        public DbSet<IncomesAndOutcomes_API.Models.AccountBalance> AccountBalances { get; set; }

        public DbSet<IncomesAndOutcomes_API.Models.Budget> Budgets { get; set; }

        public DbSet<IncomesAndOutcomes_API.Models.Category> Categories { get; set; }

        public DbSet<IncomesAndOutcomes_API.Models.Income> Incomes { get; set; }

        public DbSet<IncomesAndOutcomes_API.Models.MonthBuffer> MonthBuffers { get; set; }

        public DbSet<IncomesAndOutcomes_API.Models.Outcome> Outcomes { get; set; }

        public DbSet<IncomesAndOutcomes_API.Models.User> Users { get; set; }

        public DbSet<IncomesAndOutcomes_API.Models.UserSession> UserSessions { get; set; }
    }
}