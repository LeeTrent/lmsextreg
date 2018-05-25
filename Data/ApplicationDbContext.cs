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
        public DbSet<SubAgency> SubAgencies { get; set; }
        public DbSet<LMSProgram> LMSPrograms { get; set; }
        public DbSet<ProgramApprover> ProgramApprovers { get; set; }
        public DbSet<EnrollmentStatus> EnrollmentStatuses { get; set; }
        public DbSet<ProgramEnrollment> ProgramEnrollments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            /***************************************************************************
             Customize the ASP.NET Identity model and override the defaults if needed.
             For example, you can rename the ASP.NET Identity table names and more.
             Add your customizations after calling base.OnModelCreating(builder);
             ***************************************************************************/
            builder.Entity<Agency>().ToTable("Agency");
            builder.Entity<SubAgency>().ToTable("SubAgency");
            builder.Entity<LMSProgram>().ToTable("LMSProgram");
            builder.Entity<ProgramApprover>().ToTable("ProgramApprover");
            builder.Entity<EnrollmentStatus>().ToTable("EnrollmentStatus");
            builder.Entity<ProgramEnrollment>().ToTable("ProgramEnrollment");
            
            /************************************************************************
             There are some configurations that can only be done with the fluent API
             (specifying a composite PK).
             ************************************************************************/            
            
            /////////////////////////////////////////////////////////////////////////
            //ProgramApprover: 
            // - Composite Primary Key
            /////////////////////////////////////////////////////////////////////////
            builder.Entity<ProgramApprover>()
                .HasKey( pa => new { pa.LMSProgramID, pa.ApproverUserId } );  
            
            /////////////////////////////////////////////////////////////////////////
            // ProgramEnrollment:
            //  - Composite Primary Key
            /////////////////////////////////////////////////////////////////////////
            builder.Entity<ProgramEnrollment>()
                .HasKey( e => new { e.LMSProgramID, e.LearnerUserId } ); 
            
            /////////////////////////////////////////////////////////////////////////
            // ProgramEnrollment:
            //  - Foreign Key (EnrollmentStatus.StatusCode)
            /////////////////////////////////////////////////////////////////////////
            builder.Entity<ProgramEnrollment>()
                .HasOne( pe => pe.EnrollmentStatus)
                .WithMany()
                .HasForeignKey(pe => pe.StatusCode);

            /////////////////////////////////////////////////////////////////////////
            // StatusTransition:
            //  - Composite Primary Key
            /////////////////////////////////////////////////////////////////////////
            //builder.Entity<StatusTransition>()
            //    .HasKey( st => new { st.FromStatusCode, st.ToStatusCode } );      
                
            /////////////////////////////////////////////////////////////////////////                           
            // EnrollmentStatus:
            // - Unique Key Constraint
            /////////////////////////////////////////////////////////////////////////
            builder.Entity<EnrollmentStatus>()
                .HasIndex(es => es.StatusName)
                .IsUnique();                         
        }
    }
}