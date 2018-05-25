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
    public class IndexModel : PageModel
    {
        private readonly lmsextreg.Data.ApplicationDbContext _context;

        public IndexModel(lmsextreg.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ProgramEnrollment> ProgramEnrollment { get;set; }

        public async Task OnGetAsync()
        {
            ProgramEnrollment = await _context.ProgramEnrollments
                .Include(p => p.LMSProgram).ToListAsync();
        }
    }
}
