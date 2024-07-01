using CMS.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data
{
    public class CMSDbContext : DbContext
    {
        public CMSDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Users> Users { get; set; }

        public DbSet<Courses> Courses { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Users>().HasKey(u => u.UserId);
        //}
    }
}
