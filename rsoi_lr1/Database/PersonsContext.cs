using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rsoi_lr1.Database
{
    public class PersonsContext : DbContext
    {
        protected PersonsContext()
        { 
        }

        public PersonsContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PersonEntity> Persons { get; set; }
    }
}
