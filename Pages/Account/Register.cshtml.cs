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
using lmsextreg.Services;
using lmsextreg.Models;

namespace lmsextreg.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel
            (
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                ILogger<LoginModel> logger,
                IEmailSender emailSender,
                ApplicationDbContext dbContext,
                RoleManager<IdentityRole> roleManager

            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _dbContext = dbContext;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public SelectList AgencySelectList { get; set; }
        public SelectList SubAgencySelectList { get; set; }
        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Middle Name")]
            public string MiddleName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Job Title")]
            public string JobTitle { get; set; }   

            [Required]
            [Display(Name = "Agency")]  
            public string AgencyID { get; set; }

            [Required]
            [Display(Name = "SubAgency")]  
            public string SubAgencyID { get; set; }            
        }

        public void OnGet(string returnUrl = null)
        {
            AgencySelectList    = new SelectList(_dbContext.Agencies, "AgencyID", "AgencyName");
            SubAgencySelectList = new SelectList(_dbContext.SubAgencies, "SubAgencyID", "SubAgencyName");
            ReturnUrl           = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                //var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };

                var user = new ApplicationUser
                { 
                    UserName        = Input.Email, 
                    Email           = Input.Email,
                    FirstName       = Input.FirstName,
                    MiddleName      = Input.MiddleName,
                    LastName        = Input.LastName,
                    JobTitle        = Input.JobTitle,
                    AgencyID        = Input.AgencyID,
                    SubAgencyID     = Input.SubAgencyID,
                    DateRegistered  = DateTime.Now,
                    DateExpired     = DateTime.Now.AddDays(365)
                };

                // Create User
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if ( !await _roleManager.RoleExistsAsync("Learner") )
                    {
                        // Create Role
                        result = await _roleManager.CreateAsync(new IdentityRole("Learner"));
                    }
                    if (result.Succeeded)
                    {
                        // Create User Role 
                        result = await _userManager.AddToRoleAsync(user, "Learner");
                    }
                }    
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(Input.Email, callbackUrl);

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //return LocalRedirect(Url.GetLocalUrl(returnUrl));
                    return RedirectToPage("./RegisterConfirmation");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

       public JsonResult OnGetCountiesInContinent(string agencyID) 
        {
            List<SubAgency> subAgencyList = _dbContext.SubAgencies.Where( sa => sa.AgencyID == agencyID ).ToList();
            return new JsonResult(new SelectList(subAgencyList, "SubAgencyID", "SubAgencyName"));
        }         
    }
}