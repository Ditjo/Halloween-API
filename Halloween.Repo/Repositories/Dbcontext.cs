using Halloween.Repo.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halloween.Repo.Repositories
{
    public class Dbcontext : DbContext //EF CORE
    {
        // a class has 2 things (methods and properties)
        public Dbcontext(DbContextOptions<Dbcontext> option) : base(option)
        {
            // if I want a direct access to db, I write it here (like Program.cs)
        }

        public DbSet<SuperHero> SuperHero { get; set; }
        public DbSet<Team> Team { get; set; }
    }
}
