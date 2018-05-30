using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.EntityFrameworkCore;
using lmsextreg.Data;
using lmsextreg.Models;

namespace lmsextreg.Pages.Enrollments
{
    public class IndexModel : PageModel
    {
        private readonly lmsextreg.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(lmsextreg.Data.ApplicationDbContext context, UserManager<ApplicationUser> usrMgr)
        {
            _context = context;
            _userManager = usrMgr;
        }

        public IList<ProgramEnrollment> ProgramEnrollment { get;set; }

        public ApplicationUser LoggedInUser {get;set;}

        // public async Task OnGetAsync()
        // {
        //     ProgramEnrollment = await _context.ProgramEnrollments
        //         .Include(p => p.LMSProgram).ToListAsync();
        // }
        
        public async Task OnGetAsync()
        {
            LoggedInUser = await GetCurrentUserAsync();

            ProgramEnrollment = await _context.ProgramEnrollments
                .Where(p => p.StudentUserId == LoggedInUser.Id)
                .Include(p => p.LMSProgram)
                .Include(p => p.EnrollmentStatus)
                .Include(p => p.Student)
                .ToListAsync();
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}