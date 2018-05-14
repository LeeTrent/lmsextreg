using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using lmsextreg.Models;

namespace lmsextreg.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Country> Countries { get; set; }
        //public DbSet<Program> Programs { get; set; }
        //public DbSet<ProgramEnrollment> ProgramEnrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            /***************************************************************************
             Customize the ASP.NET Identity model and override the defaults if needed.
             For example, you can rename the ASP.NET Identity table names and more.
             Add your customizations after calling base.OnModelCreating(builder);
             ***************************************************************************/
            builder.Entity<Agency>().ToTable("Agency");
            builder.Entity<Country>().ToTable("Country");
            //builder.Entity<Program>().ToTable("Program");
            //builder.Entity<ProgramEnrollment>().ToTable("ProgramEnrollment");

            /************************************************************************
             There are some configurations that can only be done with the fluent API
             (specifying a composite PK).
             ************************************************************************/            
            //builder.Entity<ProgramEnrollment>()
            //    .HasKey(pe => new { pe.ProgramID, pe.LearnerID });         
        }
    }
}