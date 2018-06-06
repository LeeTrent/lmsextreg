using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using lmsextreg.Constants;

namespace lmsextreg.Data
{
    public static class DataSeed
    {
        public static async Task Initialize (IServiceProvider svcProvider, string tempPW)
        {
            await EnsureRoles(svcProvider);
            //await EnsureApprovers(svcProvider, tempPW); 
        }

        private static async Task EnsureRoles(IServiceProvider svcProvider)
        {
            await EnsureRole(svcProvider, RoleConstants.STUDENT);
            await EnsureRole(svcProvider, RoleConstants.APPROVER);   
        }
        
        private static async Task<IdentityResult> EnsureRole(IServiceProvider svcProvider, string roleName)
        {
            IdentityResult IR = null;

            var roleMgr = svcProvider.GetService<RoleManager<IdentityRole>>();

            // IF role doesn't exist, create it
            if ( ! await roleMgr.RoleExistsAsync(roleName))
            {
                IR  = await roleMgr.CreateAsync(new IdentityRole(roleName));
                 
            }
            return IR;
        }

        private static async Task EnsureApprovers(IServiceProvider svcProvider, string tempPW)
        {
            var userID = await EnsureApprover(svcProvider, "lee.trent@icloud.gov", tempPW);
            await EnsureUserRole(svcProvider, RoleConstants.APPROVER, userID);

            userID = await EnsureApprover(svcProvider, "lee.trent.1@outlook.gov", tempPW);
            await EnsureUserRole(svcProvider, RoleConstants.APPROVER, userID);            
        }
        private static async Task<string> EnsureApprover(IServiceProvider svcProvider, string userName, string tempPW)
        {
            var userMgr = svcProvider.GetService<UserManager<ApplicationUser>>();
            
            var user = await userMgr.FindByNameAsync(userName);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = userName
                };
            
                await userMgr.CreateAsync(user, tempPW);
            }
            
            return user.Id;
        }

        private static async Task<IdentityResult> EnsureUserRole(IServiceProvider svcProvider, string roleName, string userID)
        {
            var userMgr = svcProvider.GetService<UserManager<ApplicationUser>>();
            var user = await userMgr.FindByIdAsync(userID);

            return await userMgr.AddToRoleAsync(user, roleName);
        }
     }
}