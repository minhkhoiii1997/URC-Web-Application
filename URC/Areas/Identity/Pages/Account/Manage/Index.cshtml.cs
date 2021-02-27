/**
 * Author:    Calvin Tu
 * Date:      December 2020
 * Course:    CS 4540, University of Utah, School of Computing
 * Copyright: CS 4540 and Kevin Tran and Calvin Tu - This work may not be copied for use in Academic Coursework.
 *
 * We, Kevin Tran, Calvin Tu, and Ping Cheng Chung certify that we wrote this code from scratch and did 
 * not copy it in part or whole from another source.  Any references used 
 * in the completion of the assignment are cited in my README file and in
 * the appropriate method header.
 *
 * File Contents
 * "Controller" to handle changes to profile data
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using URC.Areas.Identity.Data;
using URC.Data;

namespace URC.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly URC_Context _context;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            URC_Context context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        // Use InputModel instead of ApplicationUser to separate validation and data model concerns
        // (will run into issues with [Required] tags and form validation)

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            // Thanks https://docs.microsoft.com/en-us/aspnet/core/security/authentication/add-user-data?view=aspnetcore-3.1&tabs=visual-studio#add-custom-user-data-to-the-identity-db
            [DataType(DataType.Text)]
            [MaxLength(100)]
            public string Name { get; set; }

            [Display(Name = "Date of Birth")]
            [DataType(DataType.Date)]
            public DateTime DOB { get; set; }


            [EnumDataType(typeof(Models.Departments))]
            public string Department { get; set; }

            [MaxLength(1000)]
            public string Description { get; set; }

            [RegularExpression(@"^[Uu]\d{7}$", ErrorMessage = "uID must be in the form uXXXXXXX where X is any number")]
            [Display(Name = "uID")]
            public string UId { get; set; }

            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; } // override because super does not provide display name

            [EmailAddress]
            public string Email { get; set; } // override to provide [EmailAddress] validation
        }

        private async Task LoadAsync(ApplicationUser user)
        {

            Username = await _userManager.GetUserNameAsync(user);
            Input = new InputModel
            {
                Name = user.Name,
                DOB = user.DOB,
                Department = user.Department,
                Description = user.Description,
                UId = user.UId,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };

            ViewData["Student"] = await _context.Students
                .Include(e => e.Courses)
                .ThenInclude(e => e.Course)
                .Include(e => e.Interests)
                .ThenInclude(e => e.Interest)
                .Include(e => e.Skills)
                .ThenInclude(e => e.Skill)
                .Where(e => e.StudentId == user.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if(Input.Name != user.Name)
            {
                user.Name = Input.Name;
                var professor = await _context.Professors.FindAsync(user.Id);
                if(professor != null)
                {
                    professor.Name = Input.Name;
                    _context.Professors.Update(professor);
                    await _context.SaveChangesAsync();
                }
            }

            if(Input.DOB != user.DOB)
            {
                user.DOB = Input.DOB;
            }

            if(Input.Department != user.Department)
            {
                user.Department = Input.Department;
            }

            if(Input.UId != user.UId)
            {
                user.UId = Input.UId;
            }

            if(Input.Description != user.Description)
            {
                user.Description = Input.Description;
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
