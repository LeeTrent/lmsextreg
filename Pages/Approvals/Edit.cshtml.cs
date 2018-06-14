using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

using lmsextreg.Data;
using lmsextreg.Models;

namespace lmsextreg.Pages.Approvals
{
    [Authorize(Roles = "APPROVER")]
    public class EditModel: PageModel
    {
         private readonly ApplicationDbContext _dbContext;
         private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(lmsextreg.Data.ApplicationDbContext dbCntx, UserManager<ApplicationUser> usrMgr)
        {
            _dbContext = dbCntx;
            _userManager = usrMgr;
        }         

        [BindProperty]
        public ProgramEnrollment ProgramEnrollment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sql = " SELECT * FROM public.\"ProgramEnrollment\" "
                    + "  WHERE  \"ProgramEnrollmentID\" = {0} "
                    + "    AND  \"LMSProgramID\" " 
                    + "     IN "
                    + "      ( "
                    + "        SELECT \"LMSProgramID\" "
                    + "          FROM public.\"ProgramApprover\" "
                    + "         WHERE \"ApproverUserId\" = {1} "
                    + "      ) ";

            // ProgramEnrollment = await _dbContext.ProgramEnrollments
            //                   .Where(pe => pe.ProgramEnrollmentID == id) 
            //                   .Include(pe => pe.LMSProgram)
            //                   .Include(pe => pe.Student)
            //                   .Include(pe => pe.EnrollmentStatus)
            //                   .SingleOrDefaultAsync();

            ProgramEnrollment = await _dbContext.ProgramEnrollments
                                .FromSql(sql, id, _userManager.GetUserId(User))
                                .Include(pe => pe.LMSProgram)
                                .Include(pe => pe.Student)
                                .Include(pe => pe.EnrollmentStatus)
                                .SingleOrDefaultAsync();


            if (ProgramEnrollment == null)
            {
                return NotFound();
            }
            
            return Page();                              
        }

    }
}