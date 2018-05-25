// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using lmsextreg.Data;
// using lmsextreg.Models;
// using lmsextreg.Constants;
// using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using lmsextreg.Data;
using lmsextreg.Models;
using lmsextreg.Constants;


namespace lmsextreg.Pages.Enrollments
{
    public class CreateModel : PageModel
    {
        private readonly lmsextreg.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel( lmsextreg.Data.ApplicationDbContext context,
                            UserManager<ApplicationUser> userMgr )
        {
            _context = context;
            _userManager = userMgr;
        }

        public class InputModel
        {
            [Required]
            [Display(Name = "Program")]  
            public string LMSProgramID { get; set; }
        }
        
        [BindProperty]
        public InputModel Input { get; set; }
        public SelectList ProgramSelectList { get; set; }

        public IActionResult OnGet()
        {
            ProgramSelectList = new SelectList(_context.LMSPrograms, "LMSProgramID", "LongName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("ModelState.IsValid: " + ModelState.IsValid);
            
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState IS NOT valid");
                return Page();
             }
            
            Console.WriteLine("ModelState IS valid");
            
            var pe = new ProgramEnrollment();
            pe.LMSProgramID         = Int32.Parse(Input.LMSProgramID);
            pe.LearnerUserId        = _userManager.GetUserId(User);
            pe.UserCreated          = _userManager.GetUserId(User);
            pe.DateCreated          = DateTime.Now;
            pe.StatusCode           = StatusConstants.PENDING;

            _context.ProgramEnrollments.Add(pe);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }        

        // public IActionResult OnGet()
        // {
        // ViewData["LMSProgramID"] = new SelectList(_context.LMSPrograms, "LMSProgramID", "LongName");
        //     return Page();
        // }

        // [BindProperty]
        // public ProgramEnrollment ProgramEnrollment { get; set; }

        // public async Task<IActionResult> OnPostAsync()
        // {
        //     Console.WriteLine("ModelState.IsValid: " + ModelState.IsValid);
            
        //     if (!ModelState.IsValid)
        //     {
        //         Console.WriteLine("ModelState IS NOT valid");
        //         return Page();
        //      }
            
        //     Console.WriteLine("ModelState IS valid");
            
        //     ProgramEnrollment.LearnerUserId = _userManager.GetUserId(User);
        //     ProgramEnrollment.UserCreated   = _userManager.GetUserId(User);
        //     ProgramEnrollment.DateCreated   = DateTime.Now;
        //     ProgramEnrollment.Status.StatusCode = StatusConstants.PENDING;

        //     _context.ProgramEnrollments.Add(ProgramEnrollment);
        //     await _context.SaveChangesAsync();

        //     return RedirectToPage("./Index");
        // }
    }
}