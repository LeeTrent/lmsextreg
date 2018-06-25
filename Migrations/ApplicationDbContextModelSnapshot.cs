﻿// <auto-generated />
using lmsextreg.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace lmsextreg.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("lmsextreg.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AgencyID");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("DateExpired");

                    b.Property<DateTime>("DateRegistered");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("JobTitle");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MiddleName");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<bool>("RulesOfBehaviorAgreedTo");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("SubAgencyID");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("AgencyID");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("SubAgencyID");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("lmsextreg.Models.Agency", b =>
                {
                    b.Property<string>("AgencyID");

                    b.Property<string>("AgencyName")
                        .IsRequired();

                    b.Property<int>("DisplayOrder");

                    b.Property<string>("OPMCode");

                    b.Property<string>("TreasuryCode");

                    b.HasKey("AgencyID");

                    b.ToTable("Agency");
                });

            modelBuilder.Entity("lmsextreg.Models.EnrollmentHistory", b =>
                {
                    b.Property<int>("EnrollmentHistoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActorRemarks");

                    b.Property<string>("ActorUserId")
                        .IsRequired();

                    b.Property<DateTime>("DateCreated");

                    b.Property<int>("ProgramEnrollmentID");

                    b.Property<int>("StatusTransitionID");

                    b.HasKey("EnrollmentHistoryID");

                    b.HasIndex("ActorUserId");

                    b.HasIndex("ProgramEnrollmentID");

                    b.HasIndex("StatusTransitionID");

                    b.ToTable("EnrollmentHistory");
                });

            modelBuilder.Entity("lmsextreg.Models.EnrollmentStatus", b =>
                {
                    b.Property<string>("StatusCode")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("StatusLabel")
                        .IsRequired();

                    b.HasKey("StatusCode");

                    b.HasIndex("StatusLabel")
                        .IsUnique();

                    b.ToTable("EnrollmentStatus");
                });

            modelBuilder.Entity("lmsextreg.Models.LMSProgram", b =>
                {
                    b.Property<int>("LMSProgramID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LongName")
                        .IsRequired();

                    b.Property<string>("ShortName")
                        .IsRequired();

                    b.HasKey("LMSProgramID");

                    b.ToTable("LMSProgram");
                });

            modelBuilder.Entity("lmsextreg.Models.ProgramApprover", b =>
                {
                    b.Property<int>("LMSProgramID");

                    b.Property<string>("ApproverUserId");

                    b.HasKey("LMSProgramID", "ApproverUserId");

                    b.HasIndex("ApproverUserId");

                    b.ToTable("ProgramApprover");
                });

            modelBuilder.Entity("lmsextreg.Models.ProgramEnrollment", b =>
                {
                    b.Property<int>("ProgramEnrollmentID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApproverUserId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateLastUpdated");

                    b.Property<int>("LMSProgramID");

                    b.Property<string>("StatusCode")
                        .IsRequired();

                    b.Property<string>("StudentUserId")
                        .IsRequired();

                    b.Property<string>("UserCreated")
                        .IsRequired();

                    b.Property<string>("UserLastUpdated");

                    b.HasKey("ProgramEnrollmentID");

                    b.HasIndex("ApproverUserId");

                    b.HasIndex("StatusCode");

                    b.HasIndex("StudentUserId");

                    b.HasIndex("LMSProgramID", "StudentUserId")
                        .IsUnique();

                    b.ToTable("ProgramEnrollment");
                });

            modelBuilder.Entity("lmsextreg.Models.StatusTransition", b =>
                {
                    b.Property<int>("StatusTransitionID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FromStatusCode")
                        .IsRequired();

                    b.Property<string>("ToStatusCode")
                        .IsRequired();

                    b.Property<string>("TransitionCode")
                        .IsRequired();

                    b.Property<string>("TransitionLabel")
                        .IsRequired();

                    b.HasKey("StatusTransitionID");

                    b.HasIndex("ToStatusCode");

                    b.HasIndex("FromStatusCode", "ToStatusCode")
                        .IsUnique();

                    b.ToTable("StatusTransition");
                });

            modelBuilder.Entity("lmsextreg.Models.SubAgency", b =>
                {
                    b.Property<string>("SubAgencyID");

                    b.Property<string>("AgencyID")
                        .IsRequired();

                    b.Property<int>("DisplayOrder");

                    b.Property<string>("OPMCode");

                    b.Property<string>("SubAgencyName")
                        .IsRequired();

                    b.Property<string>("TreasuryCode");

                    b.HasKey("SubAgencyID");

                    b.HasIndex("AgencyID");

                    b.ToTable("SubAgency");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("lmsextreg.Data.ApplicationUser", b =>
                {
                    b.HasOne("lmsextreg.Models.Agency", "Agency")
                        .WithMany()
                        .HasForeignKey("AgencyID");

                    b.HasOne("lmsextreg.Models.SubAgency", "SubAgency")
                        .WithMany()
                        .HasForeignKey("SubAgencyID");
                });

            modelBuilder.Entity("lmsextreg.Models.EnrollmentHistory", b =>
                {
                    b.HasOne("lmsextreg.Data.ApplicationUser", "Actor")
                        .WithMany()
                        .HasForeignKey("ActorUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("lmsextreg.Models.ProgramEnrollment")
                        .WithMany("EnrollmentHistory")
                        .HasForeignKey("ProgramEnrollmentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("lmsextreg.Models.StatusTransition", "StatusTransition")
                        .WithMany()
                        .HasForeignKey("StatusTransitionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("lmsextreg.Models.ProgramApprover", b =>
                {
                    b.HasOne("lmsextreg.Data.ApplicationUser", "Approver")
                        .WithMany()
                        .HasForeignKey("ApproverUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("lmsextreg.Models.LMSProgram", "LMSProgram")
                        .WithMany("ProgramApprovers")
                        .HasForeignKey("LMSProgramID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("lmsextreg.Models.ProgramEnrollment", b =>
                {
                    b.HasOne("lmsextreg.Data.ApplicationUser", "Approver")
                        .WithMany()
                        .HasForeignKey("ApproverUserId");

                    b.HasOne("lmsextreg.Models.LMSProgram", "LMSProgram")
                        .WithMany()
                        .HasForeignKey("LMSProgramID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("lmsextreg.Models.EnrollmentStatus", "EnrollmentStatus")
                        .WithMany()
                        .HasForeignKey("StatusCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("lmsextreg.Data.ApplicationUser", "Student")
                        .WithMany()
                        .HasForeignKey("StudentUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("lmsextreg.Models.StatusTransition", b =>
                {
                    b.HasOne("lmsextreg.Models.EnrollmentStatus", "FromStatus")
                        .WithMany()
                        .HasForeignKey("FromStatusCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("lmsextreg.Models.EnrollmentStatus", "ToStatus")
                        .WithMany()
                        .HasForeignKey("ToStatusCode")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("lmsextreg.Models.SubAgency", b =>
                {
                    b.HasOne("lmsextreg.Models.Agency", "Agency")
                        .WithMany("SubAgencies")
                        .HasForeignKey("AgencyID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("lmsextreg.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("lmsextreg.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("lmsextreg.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("lmsextreg.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
