using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using lmsextreg.Data;
using lmsextreg.Models;

namespace lmsextreg.Pages.Enrollments
{
    [Authorize(Roles = "STUDENT")]
    public class DeleteModel : PageModel
    {
        private readonly lmsextreg.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(lmsextreg.Data.ApplicationDbContext context, 
                            UserManager<ApplicationUser> userMgr)
        {
            _context = context;
            _userManager = userMgr;
        }

        [BindProperty]
        public ProgramEnrollment ProgramEnrollment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // ProgramEnrollment = await _context.ProgramEnrollments
            //     .Include(p => p.LMSProgram).SingleOrDefaultAsync(m => m.ProgramEnrollmentID == id);

            var loggedInUserID = _userManager.GetUserId(User);
            ProgramEnrollment = await _context.ProgramEnrollments
                .Where(pe => pe.StudentUserId == loggedInUserID && pe.ProgramEnrollmentID == id) 
                .Include(p => p.LMSProgram)
                .SingleOrDefaultAsync();                   
                  
            if (ProgramEnrollment == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProgramEnrollment = await _context.ProgramEnrollments.FindAsync(id);

            if (ProgramEnrollment != null)
            {
                _context.ProgramEnrollments.Remove(ProgramEnrollment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
