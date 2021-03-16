//using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextToDB.Models
{
    public class DateTextDbContext : DbContext
    {
        public DateTextDbContext(DbContextOptions<DateTextDbContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected DateTextDbContext()
        {
        }

        public DbSet<DateText> dateTexts { get; set; }

    }
}
