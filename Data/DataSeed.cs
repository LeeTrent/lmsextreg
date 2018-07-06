using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using lmsextreg.Constants;
using lmsextreg.Models;

namespace lmsextreg.Data
{
    public static class DataSeed
    {
        public static async Task Initialize (IServiceProvider svcProvider, string tempPW)
        {
            Console.WriteLine("DataSeed.Initialize: BEGIN");

            var dbContext = svcProvider.GetRequiredService<ApplicationDbContext>();

            await EnsureRoles(svcProvider);
            await EnsureEnrollmentStatuses(dbContext);
            await EnsureStatusTransitions(dbContext);
            await EnsurePrograms(dbContext);
            await EnsureApprovers(svcProvider, tempPW); 
            await EnsureStudents(svcProvider, tempPW); 
            
            Console.WriteLine("DataSeed.Initialize: END");
        }

        private static async Task EnsureRoles(IServiceProvider svcProvider)
        {
            Console.WriteLine("DataSeed.EnsureRoles: BEGIN");

            await EnsureRole(svcProvider, RoleConstants.STUDENT);
            await EnsureRole(svcProvider, RoleConstants.APPROVER);   

            Console.WriteLine("DataSeed.EnsureRoles: END");
        }
        
        private static async Task<IdentityResult> EnsureRole(IServiceProvider svcProvider, string roleName)
        {
            Console.WriteLine("DataSeed.EnsureRole: BEGIN");

            IdentityResult IR = null;

            var roleMgr = svcProvider.GetService<RoleManager<IdentityRole>>();

            if ( ! await roleMgr.RoleExistsAsync(roleName))
            {
                IR  = await roleMgr.CreateAsync(new IdentityRole(roleName));
                 
            }

            Console.WriteLine("DataSeed.EnsureRole: END");

            return IR;
        }
        
        private static async Task EnsureEnrollmentStatuses(ApplicationDbContext dbContext)
        {
            Console.WriteLine("DataSeed.EnsureEnrollmentStatuses: BEGIN");

            await EnsureEnrollmentStatus(dbContext, StatusCodeConstants.NONE,       StatusLabelConstants.NONE);
            await EnsureEnrollmentStatus(dbContext, StatusCodeConstants.PENDING,    StatusLabelConstants.PENDING);
            await EnsureEnrollmentStatus(dbContext, StatusCodeConstants.WITHDRAWN,  StatusLabelConstants.WITHDRAWN);
            await EnsureEnrollmentStatus(dbContext, StatusCodeConstants.APPROVED,   StatusLabelConstants.APPROVED);
            await EnsureEnrollmentStatus(dbContext, StatusCodeConstants.DENIED,     StatusLabelConstants.DENIED);
            await EnsureEnrollmentStatus(dbContext, StatusCodeConstants.REVOKED,    StatusLabelConstants.REVOKED);

            Console.WriteLine("DataSeed.EnsureEnrollmentStatuses: END");
        }        
        private static async Task EnsureEnrollmentStatus(ApplicationDbContext dbContext, string statusCode, string statusLabel)
        {
            Console.WriteLine("DataSeed.EnsureEnrollmentStatus: BEGIN");

            var enrollmentStatus = await dbContext.EnrollmentStatuses.FirstOrDefaultAsync( es => es.StatusCode == statusCode );
            if ( enrollmentStatus == null )
            {
                enrollmentStatus = new EnrollmentStatus
                {
                    StatusCode = statusCode,
                    StatusLabel = statusLabel
                };

                dbContext.EnrollmentStatuses.Add(enrollmentStatus);
                await dbContext.SaveChangesAsync();
            }

            Console.WriteLine("DataSeed.EnsureEnrollmentStatus: END");
        }    

        private static async Task EnsureStatusTransitions(ApplicationDbContext dbContext)
        {
            Console.WriteLine("DataSeed.EnsureStatusTransitions: BEGIN");

            await EnsureStatusTransition
            (
                dbContext, StatusCodeConstants.NONE, StatusCodeConstants.PENDING, TransitionCodeConstants.NONE_TO_PENDING, TransitionLabelConstants.NONE_TO_PENDING
            );
            
            await EnsureStatusTransition
            (
                dbContext, StatusCodeConstants.PENDING,  StatusCodeConstants.WITHDRAWN, TransitionCodeConstants.PENDING_TO_WITHDRAWN,  TransitionLabelConstants.PENDING_TO_WITHDRAWN
            );
            
            await EnsureStatusTransition
            (
                dbContext, StatusCodeConstants.PENDING,  StatusCodeConstants.APPROVED, TransitionCodeConstants.PENDING_TO_APPROVED,  TransitionLabelConstants.PENDING_TO_APPROVED
            );
            
            await EnsureStatusTransition
            (
                dbContext, StatusCodeConstants.PENDING,  StatusCodeConstants.DENIED,  TransitionCodeConstants.PENDING_TO_DENIED,  TransitionLabelConstants.PENDING_TO_DENIED
            );

            await EnsureStatusTransition
            (
                dbContext, StatusCodeConstants.APPROVED,  StatusCodeConstants.REVOKED,  TransitionCodeConstants.APPROVED_TO_REVOKED,  TransitionLabelConstants.APPROVED_TO_REVOKED
            );

            await EnsureStatusTransition
            (
                dbContext, StatusCodeConstants.DENIED,  StatusCodeConstants.APPROVED,  TransitionCodeConstants.DENIED_TO_APPROVED,  TransitionLabelConstants.DENIED_TO_APPROVED
            );            

            Console.WriteLine("DataSeed.EnsureStatusTransitions: END");
        }
        private static async Task EnsureStatusTransition(ApplicationDbContext dbContext, string fromStatusCode, string toStatusCode,
                                                            string transitionCode, string transitionLabel)
        {
            Console.WriteLine("DataSeed.EnsureStatusTransition: BEGIN");
            
            var statusTransition = await dbContext.StatusTransitions.FirstOrDefaultAsync(st => st.FromStatusCode == fromStatusCode && st.ToStatusCode == toStatusCode);
            if ( statusTransition == null)
            {
                statusTransition = new StatusTransition
                {
                    FromStatusCode = fromStatusCode,
                    ToStatusCode = toStatusCode,
                    TransitionCode = transitionCode,
                    TransitionLabel = transitionLabel
                };

                dbContext.StatusTransitions.Add(statusTransition);
                await dbContext.SaveChangesAsync();
            }

            Console.WriteLine("DataSeed.EnsureStatusTransition: END");
        }
        
        private static async Task EnsurePrograms(ApplicationDbContext dbContext)
        {
            Console.WriteLine("DataSeed.EnsurePrograms: BEGIN");

            await EnsureProgram(dbContext, "PA", "Program A");
            await EnsureProgram(dbContext, "PB", "Program B");
            await EnsureProgram(dbContext, "PC", "Program C");
            await EnsureProgram(dbContext, "PD", "Program D");
            await EnsureProgram(dbContext, "PE", "Program E");
            await EnsureProgram(dbContext, "PF", "Program F");

            Console.WriteLine("DataSeed.EnsurePrograms: END");
        }
        private static async Task EnsureProgram(ApplicationDbContext dbContext, string shortName, string longName)
        {
            Console.WriteLine("DataSeed.EnsureProgram: BEGIN");

            LMSProgram program = await dbContext.LMSPrograms.FirstOrDefaultAsync( p => p.ShortName == shortName );
            if ( program == null )
            {
                program = new LMSProgram
                {
                    ShortName = shortName,
                    LongName = longName
                };

                dbContext.LMSPrograms.Add(program);
                await dbContext.SaveChangesAsync();
            }

            Console.WriteLine("DataSeed.EnsureProgram: END");
        }


        private static async Task EnsureApprovers(IServiceProvider svcProvider, string tempPW)
        {
            Console.WriteLine("DataSeed.EnsureApprovers: BEGIN");

            await EnsureApprover(svcProvider, "ProgramApproverPA1@gsa.gov", tempPW, "Approver", "PA1", "GS", "GS30", "PA" );

            await EnsureApprover(svcProvider, "ProgramApproverPB1@gsa.gov", tempPW, "Approver", "PB1", "GS", "GS30", "PB");
            await EnsureApprover(svcProvider, "ProgramApproverPB2@gsa.gov", tempPW, "Approver", "PB2", "GS", "GS30", "PB");

            await EnsureApprover(svcProvider, "ProgramApproverPC1@gsa.gov", tempPW, "Approver", "PC1", "GS", "GS30", "PC");
            await EnsureApprover(svcProvider, "ProgramApproverPC2@gsa.gov", tempPW, "Approver", "PC2", "GS", "GS30", "PC");
            await EnsureApprover(svcProvider, "ProgramApproverPC3@gsa.gov", tempPW, "Approver", "PC3", "GS", "GS30", "PC");                        

            await EnsureApprover(svcProvider, "ProgramApproverPD1@gsa.gov", tempPW, "Approver", "PD1", "GS", "GS03", "PD");                        
            await EnsureApprover(svcProvider, "ProgramApproverPD2@gsa.gov", tempPW, "Approver", "PD2", "GS", "GS03", "PD");                                                
            await EnsureApprover(svcProvider, "ProgramApproverPD3@gsa.gov", tempPW, "Approver", "PD3", "GS", "GS03", "PD");                        
            await EnsureApprover(svcProvider, "ProgramApproverPD4@gsa.gov", tempPW, "Approver", "PD4", "GS", "GS03", "PD");                        

            await EnsureApprover(svcProvider, "ProgramApproverPE1@gsa.gov", tempPW, "Approver", "PE1", "GS", "GS03", "PE");                        
            await EnsureApprover(svcProvider, "ProgramApproverPE2@gsa.gov", tempPW, "Approver", "PE2", "GS", "GS03", "PE");                        
            await EnsureApprover(svcProvider, "ProgramApproverPE3@gsa.gov", tempPW, "Approver", "PE3", "GS", "GS03", "PE");                        
            await EnsureApprover(svcProvider, "ProgramApproverPE4@gsa.gov", tempPW, "Approver", "PE4", "GS", "GS03", "PE");                        
            await EnsureApprover(svcProvider, "ProgramApproverPE5@gsa.gov", tempPW, "Approver", "PE5", "GS", "GS03", "PE");    

            await EnsureApprover(svcProvider, "lee.trent@gsa.gov",          tempPW, "Lee", "Trent - PF1", "GS", "GS03", "PF");    
            await EnsureApprover(svcProvider, "lee.trent@icloud.com",       tempPW, "Lee", "Trent - PF2", "GS", "GS03", "PF");    

            Console.WriteLine("DataSeed.EnsureApprovers: END");
        }
  
        private static async Task EnsureApprover(IServiceProvider svcProvider, string userName, string tempPW, 
                                                    string firstName, string lastName,
                                                    string agencyID, string subagencyID, string programShortName)
        {
             Console.WriteLine("DataSeed.EnsureApprover: BEGIN");

            ////////////////////////////////////////////////////////////////////
            // Create Approver
            ////////////////////////////////////////////////////////////////////            
            var userMgr = svcProvider.GetService<UserManager<ApplicationUser>>();
            var user = await userMgr.FindByNameAsync(userName);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true,
                    FirstName = firstName,
                    LastName = lastName, 
                    JobTitle = "Program Administrator",       
                    AgencyID = agencyID,
                    SubAgencyID = subagencyID,
                    DateRegistered = DateTime.Now,
                    DateAccountExpires = DateTime.Now.AddDays(AccountConstants.DAYS_ACCOUNT_EXPIRES),
                    DatePasswordExpires = DateTime.Now,
                    RulesOfBehaviorAgreedTo = true                             
                };
                
                await userMgr.CreateAsync(user, tempPW);
                
                ////////////////////////////////////////////////////////////////////
                // Assign Approver Role to Approver
                ////////////////////////////////////////////////////////////////////            
                await userMgr.AddToRoleAsync(user, RoleConstants.APPROVER);

                ////////////////////////////////////////////////////////////////////
                // Assign Approver to Program that he/she is authorized for 
                ////////////////////////////////////////////////////////////////////            
                var dbContext = svcProvider.GetRequiredService<ApplicationDbContext>();
                LMSProgram program = await dbContext.LMSPrograms.FirstOrDefaultAsync( p => p.ShortName == programShortName );
                var programApprover = new ProgramApprover
                {
                    LMSProgramID = program.LMSProgramID,
                    ApproverUserId = user.Id
                };

                dbContext.ProgramApprovers.Add(programApprover);
                await dbContext.SaveChangesAsync();
            }
             Console.WriteLine("DataSeed.EnsureApprover: END");            
        }

        private static async Task EnsureStudents(IServiceProvider svcProvider, string tempPW)
        {
            Console.WriteLine("DataSeed.EnsureStudents: BEGIN");

            await EnsureStudent(svcProvider, "student01@state.gov", tempPW, "Student", "#1", "ST", "ST00");
            await EnsureStudent(svcProvider, "student02@state.gov", tempPW, "Student", "#2", "ST", "ST00");
            await EnsureStudent(svcProvider, "student03@state.gov", tempPW, "Student", "#3", "ST", "ST00");
            await EnsureStudent(svcProvider, "student04@state.gov", tempPW, "Student", "#4", "ST", "ST00");
            await EnsureStudent(svcProvider, "student05@state.gov", tempPW, "Student", "#5", "ST", "ST00");
            await EnsureStudent(svcProvider, "student06@state.gov", tempPW, "Student", "#6", "ST", "ST00");
            await EnsureStudent(svcProvider, "student07@state.gov", tempPW, "Student", "#7", "ST", "ST00");
            await EnsureStudent(svcProvider, "student08@state.gov", tempPW, "Student", "#8", "ST", "ST00");
            await EnsureStudent(svcProvider, "student09@state.gov", tempPW, "Student", "#9", "ST", "ST00");
            await EnsureStudent(svcProvider, "student10@state.gov", tempPW, "Student", "#10", "ST", "ST00");

            await EnsureStudent(svcProvider, "lee.trent.1@gmail.com", tempPW, "Lee", "Trent", "DJ", "DJ02");

            Console.WriteLine("DataSeed.EnsureStudents: END");
        }

        private static async Task EnsureStudent(IServiceProvider svcProvider, string userName, string tempPW,
                                                    string firstName, string lastName, string agencyID, string subagencyID)
        {
            Console.WriteLine("DataSeed.EnsureStudent: BEGIN");

            ////////////////////////////////////////////////////////////////////
            // Create Student
            ////////////////////////////////////////////////////////////////////            
            var userMgr = svcProvider.GetService<UserManager<ApplicationUser>>();
            var user = await userMgr.FindByNameAsync(userName);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true,   
                    FirstName = firstName,
                    LastName = lastName, 
                    JobTitle = "IT Specialist",      
                    AgencyID = agencyID,
                    SubAgencyID = subagencyID,
                    DateRegistered = DateTime.Now,
                    DateAccountExpires = DateTime.Now.AddDays(AccountConstants.DAYS_ACCOUNT_EXPIRES),
                    DatePasswordExpires = DateTime.Now.AddDays(AccountConstants.DAYS_PASSWORD_EXPIRES),
                    RulesOfBehaviorAgreedTo = true                          
                };
                
                await userMgr.CreateAsync(user, tempPW);
                
                ////////////////////////////////////////////////////////////////////
                // Assign Student Role to Student
                ////////////////////////////////////////////////////////////////////            
                await userMgr.AddToRoleAsync(user, RoleConstants.STUDENT);       

                Console.WriteLine("DataSeed.EnsureStudent: END");     
            }
        }
     }
}