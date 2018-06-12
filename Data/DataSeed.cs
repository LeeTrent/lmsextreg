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

        private static async Task EnsurePrograms(ApplicationDbContext dbContext)
        {
            Console.WriteLine("DataSeed.EnsurePrograms: BEGIN");

            await EnsureProgram(dbContext, "PA", "Program A");
            await EnsureProgram(dbContext, "PB", "Program B");
            await EnsureProgram(dbContext, "PC", "Program C");
            await EnsureProgram(dbContext, "PD", "Program D");
            await EnsureProgram(dbContext, "PE", "Program E");

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

            await EnsureApprover(svcProvider, "ProgramApproverPA1@state.gov", tempPW, "PA");

            await EnsureApprover(svcProvider, "ProgramApproverPB1@state.gov", tempPW, "PB");
            await EnsureApprover(svcProvider, "ProgramApproverPB2@state.gov", tempPW, "PB");

            await EnsureApprover(svcProvider, "ProgramApproverPC1@state.gov", tempPW, "PC");
            await EnsureApprover(svcProvider, "ProgramApproverPC2@state.gov", tempPW, "PC");
            await EnsureApprover(svcProvider, "ProgramApproverPC3@state.gov", tempPW, "PC");                        

            await EnsureApprover(svcProvider, "ProgramApproverPD1@state.gov", tempPW, "PD");                        
            await EnsureApprover(svcProvider, "ProgramApproverPD2@state.gov", tempPW, "PD");                                                
            await EnsureApprover(svcProvider, "ProgramApproverPD3@state.gov", tempPW, "PD");                        
            await EnsureApprover(svcProvider, "ProgramApproverPD4@state.gov", tempPW, "PD");                        

            await EnsureApprover(svcProvider, "ProgramApproverPE1@state.gov", tempPW, "PE");                        
            await EnsureApprover(svcProvider, "ProgramApproverPE2@state.gov", tempPW, "PE");                        
            await EnsureApprover(svcProvider, "ProgramApproverPE3@state.gov", tempPW, "PE");                        
            await EnsureApprover(svcProvider, "ProgramApproverPE4@state.gov", tempPW, "PE");                        
            await EnsureApprover(svcProvider, "ProgramApproverPE5@state.gov", tempPW, "PE");                                                                        

            Console.WriteLine("DataSeed.EnsureApprovers: END");
        }
  
        private static async Task EnsureApprover(IServiceProvider svcProvider, string userName, string tempPW, string programShortName)
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
                    EmailConfirmed = true
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

            await EnsureStudent(svcProvider, "student01@state.gov", tempPW);
            await EnsureStudent(svcProvider, "student02@state.gov", tempPW);
            await EnsureStudent(svcProvider, "student03@state.gov", tempPW);
            await EnsureStudent(svcProvider, "student04@state.gov", tempPW);
            await EnsureStudent(svcProvider, "student05@state.gov", tempPW);
            await EnsureStudent(svcProvider, "student06@state.gov", tempPW);
            await EnsureStudent(svcProvider, "student07@state.gov", tempPW);
            await EnsureStudent(svcProvider, "student08@state.gov", tempPW);
            await EnsureStudent(svcProvider, "student09@state.gov", tempPW);
            await EnsureStudent(svcProvider, "student10@state.gov", tempPW);

            Console.WriteLine("DataSeed.EnsureStudents: END");
        }

        private static async Task EnsureStudent(IServiceProvider svcProvider, string userName, string tempPW)
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
                    EmailConfirmed = true
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