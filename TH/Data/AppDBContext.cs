using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TH.Models;

namespace TH.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        private Func<DbContextOptions<AppDBContext>> testDbContextOptions;

        public AppDBContext(Func<DbContextOptions<AppDBContext>> testDbContextOptions)
        {
            this.testDbContextOptions = testDbContextOptions;
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Survey> Surveys { get; set; }
    }
}
