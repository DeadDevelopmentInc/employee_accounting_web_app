using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppEmpAcc.Models;
using WebAppEmpAcc.Models.Departments;

namespace WebAppEmpAcc.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Database with positions
        /// </summary>
        public DbSet<Position> Positions { get; set; }

        /// <summary>
        /// Database with departments
        /// </summary>
        public DbSet<Department> Departments { get; set; }

        /// <summary>
        /// Database with branchs
        /// </summary>
        public DbSet<Branch> Branchs { get; set; }

        /// <summary>
        /// Database with sectors
        /// </summary>
        public DbSet<Sector> Sectors { get; set; }

        /// <summary>
        /// Database with pictures of user
        /// </summary>
        public DbSet<Picture> Pictures { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
