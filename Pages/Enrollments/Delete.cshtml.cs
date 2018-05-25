using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lmsextreg.Data;
using lmsextreg.Models;

namespace lmsextreg.Pages.Enrollments
{
    public class DeleteModel : PageModel
    {
        private readonly lmsextreg.Data.ApplicationDbContext _context;

        public DeleteModel(lmsextreg.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProgramEnrollment ProgramEnrollment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProgramEnrollment = await _context.ProgramEnrollments
                .Include(p => p.LMSProgram).SingleOrDefaultAsync(m => m.LMSProgramID == id);

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
