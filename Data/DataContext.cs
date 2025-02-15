using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASP_Project.Models;

namespace ASP_Project.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<ASP_Project.Models.EBook> EBook { get; set; } = default!;
        public DbSet<ASP_Project.Models.User> User { get; set; } = default!;
        public DbSet<ASP_Project.Models.ELibrary> ELibrary { get; set; } = default!;
    }
}
