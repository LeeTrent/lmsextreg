using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

using lmsextreg.Constants;
using lmsextreg.Data;
using lmsextreg.Models;

namespace lmsextreg.Pages.Approvals
{
    [Authorize(Roles = "APPROVER")]
    public class ReviewModel: PageModel
    {
         private readonly ApplicationDbContext _dbContext;
         private readonly UserManager<ApplicationUser> _userManager;

        public ReviewModel(lmsextreg.Data.ApplicationDbContext dbCntx, UserManager<ApplicationUser> usrMgr)
        {
            _dbContext = dbCntx;
            _userManager = usrMgr;
        }         

        public class InputModel
        {
            [Display(Name = "Remarks")]  
            public string Remarks { get; set; }
        }   


        [BindProperty]
        public InputModel Input { get; set; }
        public ProgramEnrollment ProgramEnrollment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ////////////////////////////////////////////////////////////
            // Step #1:
            // Check to see if records exists
            ////////////////////////////////////////////////////////////
            ProgramEnrollment = await _dbContext.ProgramEnrollments
                              .Where(pe => pe.ProgramEnrollmentID == id) 
                              .SingleOrDefaultAsync();     

            ////////////////////////////////////////////////////////////
            // Return "Not Found" if record doesn't exist
            ////////////////////////////////////////////////////////////
            if (ProgramEnrollment == null)
            {
                return NotFound();
            }                                     

            ////////////////////////////////////////////////////////////
            // Step #2:
            // Now that record exists, make sure that the logged-in user
            // is authorized to edit (approver/deny) enrollment
            // applications for this particular LMS Program.
            ////////////////////////////////////////////////////////////
            var sql = " SELECT * FROM public.\"ProgramEnrollment\" "
                    + "  WHERE  \"ProgramEnrollmentID\" = {0} "
                    + "    AND  \"LMSProgramID\" " 
                    + "     IN "
                    + "      ( "
                    + "        SELECT \"LMSProgramID\" "
                    + "          FROM public.\"ProgramApprover\" "
                    + "         WHERE \"ApproverUserId\" = {1} "
                    + "      ) ";

            ProgramEnrollment = null;
            ProgramEnrollment = await _dbContext.ProgramEnrollments
                                .FromSql(sql, id, _userManager.GetUserId(User))
                                .Include(pe => pe.LMSProgram)
                                .Include(pe => pe.Student)
                                    .ThenInclude(s => s.SubAgency)
                                    .ThenInclude(sa => sa.Agency)
                                .Include(pe => pe.EnrollmentStatus)
                                .SingleOrDefaultAsync();

            /////////////////////////////////////////////////////////////
            // We already know that record exists from Step #1 so if we
            // get a "Not Found" in Step #2, we know it's because the 
            // logged-in user is not authorized to edit (approve/deny)
            // enrollment applications for this LMS Program.
            /////////////////////////////////////////////////////////////
            if (ProgramEnrollment == null)
            {
                return Unauthorized();
            }
            
            return Page();                              
        }

        public async Task<IActionResult> OnPostApproveAsync(int id)
        {
            Console.WriteLine("Approvals.Review.OnPostApproveAsync():");
            Console.WriteLine("programEnrollmentID: " + id);
            Console.WriteLine("Remarks: " + Input.Remarks);

            return await this.OnPostAsync
            (
                id, 
                StatusCodeConstants.APPROVED, 
                TransitionCodeConstants.PENDING_TO_APPROVED
            );
        }

        public async Task<IActionResult> OnPostDenyAsync(int id)
        {
            Console.WriteLine("Approvals.Review.OnPostDenyAsync():");
            Console.WriteLine("programEnrollmentID: " + id);
            Console.WriteLine("Remarks: " + Input.Remarks);

            return await this.OnPostAsync
            (
                id, 
                StatusCodeConstants.DENIED, 
                TransitionCodeConstants.PENDING_TO_DENIED
            );
        }

        public async Task<IActionResult> OnPostAsync(int programEnrollmentID, string statusCode, string statusTransitionCode)
        {
            Console.WriteLine("Approvals.Review.OnPostApproveAsync(): BEGIN");
            Console.WriteLine("id: " + programEnrollmentID);
            Console.WriteLine("Remarks: " + Input.Remarks);

            ////////////////////////////////////////////////////////////
            // Step #1:
            // Check to see if records exists
            ////////////////////////////////////////////////////////////
            var lvProgramEnrollment = await _dbContext.ProgramEnrollments
                                        .Where(pe => pe.ProgramEnrollmentID == programEnrollmentID) 
                                        .SingleOrDefaultAsync();     

            ////////////////////////////////////////////////////////////
            // Return "Not Found" if record doesn't exist
            ////////////////////////////////////////////////////////////
            if (lvProgramEnrollment == null)
            {
                Console.WriteLine("ProgramEnrollment NOT FOUND in Step #1");
                return NotFound();
            } 

            ////////////////////////////////////////////////////////////
            // Step #2:
            // Now that record exists, make sure that the logged-in user
            // is authorized to edit (approver/deny) enrollment
            // applications for this particular LMS Program.
            ////////////////////////////////////////////////////////////
            var sql = " SELECT * FROM public.\"ProgramEnrollment\" "
                    + "  WHERE  \"ProgramEnrollmentID\" = {0} "
                    + "    AND  \"LMSProgramID\" " 
                    + "     IN "
                    + "      ( "
                    + "        SELECT \"LMSProgramID\" "
                    + "          FROM public.\"ProgramApprover\" "
                    + "         WHERE \"ApproverUserId\" = {1} "
                    + "      ) ";

            lvProgramEnrollment = null;
            lvProgramEnrollment = await _dbContext.ProgramEnrollments
                    .FromSql(sql, programEnrollmentID, _userManager.GetUserId(User))
                    .Include(pe => pe.EnrollmentHistory)
                    .SingleOrDefaultAsync();

            /////////////////////////////////////////////////////////////
            // We already know that record exists from Step #1 so if we
            // get a "Not Found" in Step #2, we know it's because the 
            // logged-in user is not authorized to edit (approve/deny)
            // enrollment applications for this LMS Program.
            /////////////////////////////////////////////////////////////
            if (lvProgramEnrollment == null)
            {
                return Unauthorized();
            }            

            ////////////////////////////////////////////////////////////
            // Retrieve the correct StatusTransition
            ////////////////////////////////////////////////////////////            
            var lvStatusTranstion = await _dbContext.StatusTransitions
                                    .Where(st => st.TransitionCode == statusTransitionCode)
                                    .SingleOrDefaultAsync();
            
            ////////////////////////////////////////////////////////////////
            // Create EnrollmentHistory using the correct StatusTranistion
            ////////////////////////////////////////////////////////////////            
            var lvEnrollmentHistory = new EnrollmentHistory()
            {
                    StatusTransitionID = lvStatusTranstion.StatusTransitionID,
                    ActorUserId = _userManager.GetUserId(User),
                    ActorRemarks = Input.Remarks,
                    DateCreated = DateTime.Now
            };

            ////////////////////////////////////////////////////////////
            // Instantiate EnrollmentHistory, if necessary
            ////////////////////////////////////////////////////////////
            if ( lvProgramEnrollment.EnrollmentHistory == null) 
            {
                lvProgramEnrollment.EnrollmentHistory = new List<EnrollmentHistory>();
            }

            ///////////////////////////////////////////////////////////////////
            // Add newly created EnrollmentHistory with StatusTransition  
            // to ProgramEnrollment's EnrollmentHistory Collection
            ///////////////////////////////////////////////////////////////////            
            lvProgramEnrollment.EnrollmentHistory.Add(lvEnrollmentHistory);

            /////////////////////////////////////////////////////////////////
            // Update ProgramEnrollment Record with
            //  1. EnrollmentStatus of "APPROVED"
            //  2. ApproverUserId (logged-in user)
            //  3. EnrollmentHistory (PENDING TO APPROVED)
            /////////////////////////////////////////////////////////////////
            lvProgramEnrollment.StatusCode = statusCode;
            lvProgramEnrollment.ApproverUserId = _userManager.GetUserId(User);
            _dbContext.ProgramEnrollments.Update(lvProgramEnrollment);
            await _dbContext.SaveChangesAsync();

            /////////////////////////////////////////////////////////////////
            // Redirect to Approval Index Page
            /////////////////////////////////////////////////////////////////
            return RedirectToPage("./Index");          
        }
    }
}