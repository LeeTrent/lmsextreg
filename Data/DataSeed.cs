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

            await EnsureApprover(svcProvider, "ProgramApproverPA1@gsa.gov", tempPW, "PA");

            await EnsureApprover(svcProvider, "ProgramApproverPB1@gsa.gov", tempPW, "PB");
            await EnsureApprover(svcProvider, "ProgramApproverPB2@gsa.gov", tempPW, "PB");

            await EnsureApprover(svcProvider, "ProgramApproverPC1@gsa.gov", tempPW, "PC");
            await EnsureApprover(svcProvider, "ProgramApproverPC2@gsa.gov", tempPW, "PC");
            await EnsureApprover(svcProvider, "ProgramApproverPC3@gsa.gov", tempPW, "PC");                        

            await EnsureApprover(svcProvider, "ProgramApproverPD1@gsa.gov", tempPW, "PD");                        
            await EnsureApprover(svcProvider, "ProgramApproverPD2@gsa.gov", tempPW, "PD");                                                
            await EnsureApprover(svcProvider, "ProgramApproverPD3@gsa.gov", tempPW, "PD");                        
            await EnsureApprover(svcProvider, "ProgramApproverPD4@gsa.gov", tempPW, "PD");                        

            await EnsureApprover(svcProvider, "ProgramApproverPE1@gsa.gov", tempPW, "PE");                        
            await EnsureApprover(svcProvider, "ProgramApproverPE2@gsa.gov", tempPW, "PE");                        
            await EnsureApprover(svcProvider, "ProgramApproverPE3@gsa.gov", tempPW, "PE");                        
            await EnsureApprover(svcProvider, "ProgramApproverPE4@gsa.gov", tempPW, "PE");                        
            await EnsureApprover(svcProvider, "ProgramApproverPE5@gsa.gov", tempPW, "PE");                                                                        

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

                // Console.WriteLine("program.LMSProgramI: " + program.LMSProgramID);
                // Console.WriteLine("user.Id: " + user.Id);
                // Console.WriteLine("programApprover description: ");
                // Console.WriteLine(programApprover.ToString());

                dbContext.ProgramApprovers.Add(programApprover);
                await dbContext.SaveChangesAsync();
            }
             Console.WriteLine("DataSeed.EnsureApprover: END");            

        }

        // private static async Task<IdentityResult> EnsureUserRole(IServiceProvider svcProvider, string roleName, string userID)
        // {
        //     Console.WriteLine("DataSeed.EnsureUserRole: BEGIN");

        //     var userMgr = svcProvider.GetService<UserManager<ApplicationUser>>();
        //     var user = await userMgr.FindByIdAsync(userID);

        //     Console.WriteLine("DataSeed.EnsureUserRole: END");
            
        //     return await userMgr.AddToRoleAsync(user, roleName);
        // }

    //   private static async Task<string> EnsureUser(IServiceProvider svcProvider, string userName, string tempPW)
    //     {
    //         Console.WriteLine("DataSeed.EnsureUser: BEGIN");
    //         Console.WriteLine("userName: " + userName);
    //         Console.WriteLine("tempPW: " + tempPW);

    //         var userMgr = svcProvider.GetService<UserManager<ApplicationUser>>();
            
    //         var user = await userMgr.FindByNameAsync(userName);
    //         if (user == null)
    //         {
    //             Console.WriteLine(userName + " not found");

    //             // user = new ApplicationUser
    //             // {
    //             //     UserName = userName,
    //             //     DateRegistered  = DateTime.Now,
    //             //     DateExpired     = DateTime.Now.AddDays(365)
    //             // };
            
    //             user = new ApplicationUser
    //             {
    //                 UserName = userName
    //             };

    //             try
    //             {
    //                 await userMgr.CreateAsync(user, tempPW);
    //             }
    //             catch(Exception exc)
    //             {
    //                 Console.WriteLine("BEGIN: exception message");
    //                 Console.WriteLine(exc.Message);
    //                 Console.WriteLine("END: exception message");
    //             }    
    //         }
            
    //         Console.WriteLine("DataSeed.EnsureUser: END");

    //         return user.Id;
    //     }

     }
}