using IdentitySvc.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySvc
{
    public class IdentityDB: DbContext
    {
        public IdentityDB(DbContextOptions<IdentityDB> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> User { get; set; }
    }
}
