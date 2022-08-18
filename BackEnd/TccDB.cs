using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.BackEnd
{
    public class TccDB : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public TccDB() : base("TccDBConnectionStrring") { }
    }
}
