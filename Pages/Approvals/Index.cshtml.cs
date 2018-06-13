using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using lmsextreg.Data;
using lmsextreg.Models;
using lmsextreg.Constants;

namespace lmsextreg.Pages.Approvals
{
    [Authorize(Roles = "APPROVER")]
    public class IndexModel: PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(lmsextreg.Data.ApplicationDbContext dbCntx, UserManager<ApplicationUser> usrMgr)
        {
            _dbContext = dbCntx;
            _userManager = usrMgr;
        }
        public IList<ProgramEnrollment> ProgramEnrollment { get;set; }
        public ApplicationUser LoggedInUser {get;set;}

        public async Task OnGetAsync()
        {
            Console.WriteLine("User is APPROVER: " + User.IsInRole(RoleConstants.APPROVER));

            LoggedInUser = await GetCurrentUserAsync();

            if ( User.IsInRole(RoleConstants.APPROVER))
            {
                var loggedInUserID = _userManager.GetUserId(User);
                
                // ProgramEnrollment = await _dbContext.ProgramEnrollments
                // .Include(pe => pe.LMSProgram)
                //     .ThenInclude(p => p.ProgramApprovers)
                //         .ThenInclude(pa => pa.ApproverUserId == loggedInUserID)
                // .ToListAsync();

            // ProgramEnrollment = await _dbContext.ProgramEnrollments
            //     .Where(p => p.StudentUserId == LoggedInUser.Id)
            //     .Include(p => p.LMSProgram)
            //     .Include(p => p.EnrollmentStatus)
            //     .Include(p => p.Student)
            //     .ToListAsync();

            //     ProgramEnrollment = await _dbContext.ProgramEnrollments
            //     .Include(pe => pe.LMSProgram)
            //         .ThenInclude(p => p.ProgramApprovers)
            //     .ToListAsync();

                // ProgramEnrollment = await _dbContext.ProgramEnrollments
                // .Include( p => p.LMSProgram)
                // .Include( p => p.EnrollmentStatus)
                // .Include( p => p.Student)
                // .Include( p => p.Approver)
                // .OrderBy( p => p.LMSProgram.LongName).ThenBy(p => p.Student.FullName).ThenBy(p => p.EnrollmentStatus.StatusCode)
                // .ToListAsync();    

                ProgramEnrollment = await _dbContext.ProgramEnrollments
                .Include( p => p.LMSProgram).ThenInclude(p => p.ProgramApprovers)
                .Include( p => p.EnrollmentStatus)
                .Include( p => p.Student)
                .Include( p => p.Approver)
                .OrderBy( p => p.LMSProgram.LongName).ThenBy(p => p.Student.FullName).ThenBy(p => p.EnrollmentStatus.StatusCode)
                .ToListAsync();                             
                
                // ProgramEnrollment = await _dbContext.ProgramEnrollments
                // .Include( p => p.LMSProgram)
                //     .ThenInclude(p => p.ProgramApprovers).Where(p => p.ApproverUserId == loggedInUserID)
                // .Include( p => p.EnrollmentStatus)
                // .Include( p => p.Student)
                // .Include( p => p.Approver)
                // .OrderBy( p => p.LMSProgram.LongName).ThenBy(p => p.Student.FullName).ThenBy(p => p.EnrollmentStatus.StatusCode)
                // .ToListAsync();    

                Console.WriteLine("ProgramEnrollment.Count: " + ProgramEnrollment.Count);
            }
            

        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}