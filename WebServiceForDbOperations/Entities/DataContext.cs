using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServiceForDbOperations.Entities
{
    public class DataContext : DbContext
    {
        public DbSet<AppBoard> Boards { get; set; }
        public DbSet<AppTask> Tasks { get; set; }
        public DataContext(DbContextOptions opt) : base(opt)
        {

        }
    }
}
