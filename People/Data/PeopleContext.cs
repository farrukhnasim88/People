using Microsoft.EntityFrameworkCore;
using People.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace People.Data
{
    public class PeopleContext: DbContext
    {

        public PeopleContext(DbContextOptions<PeopleContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Children> Children { get; set; }
        public DbSet<Sibling> Siblings { get; set; }

    }
}
