using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogWebApp.Models;

namespace BlogWebApp.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext (DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        public DbSet<BlogWebApp.Models.AdminInfo> AdminInfo { get; set; } = default!;

        public DbSet<BlogWebApp.Models.BlogInfo>? BlogInfo { get; set; }

        public DbSet<BlogWebApp.Models.EmpInfo>? EmpInfo { get; set; }
    }
}
