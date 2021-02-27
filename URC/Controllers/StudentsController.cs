/**
 * Author:    Kevin Tran
 * Partner:   Calvin Tu
 * Date:      10/20/2020
 * Course:    CS 4540, University of Utah, School of Computing
 * Copyright: CS 4540 and Kevin Tran and Calvin Tu - This work may not be copied for use in Academic Coursework.
 *
 * We, Kevin Tran, Calvin Tu certify that we wrote this code from scratch and did 
 * not copy it in part or whole from another source.  Any references used 
 * in the completion of the assignment are cited in my README file and in
 * the appropriate method header.
 *
 * File Contents
 * Controller code for student applications
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using URC.Areas.Identity.Data;
using URC.Data;
using URC.Models;

namespace URC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly URC_Context _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly HashSet<string> acceptedResumeFormats = new HashSet<string>(new List<string> { ".pdf", ".docx", ".doc" });

        public StudentsController(URC_Context context, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var isStudent = this.User.IsInRole("Student");
            var user = await _userManager.GetUserAsync(this.User);
            var applications = await _context.StudentApplications
                .Include(e => e.Opportunity)
                .Where(e => (!isStudent && e.Opportunity.Professor.ProfessorId == user.Id) || (isStudent && e.Student.StudentId == user.Id))
                .ToListAsync();

            ViewData["IsStudent"] = isStudent;

            return View(applications);
        }

        // GET: Students/Details/5
        [Authorize(Roles = "Student,Professor")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isStudent = this.User.IsInRole("Student");
            var user = await _userManager.GetUserAsync(this.User);
            var studentApplication = await _context.StudentApplications
                .Include(o => o.Student)
                .ThenInclude(s => s.Courses)
                .ThenInclude(s => s.Course)
                .Include(o => o.Student)
                .ThenInclude(s => s.Interests)
                .ThenInclude(s => s.Interest)
                .Include(o => o.Student)
                .ThenInclude(s => s.Skills)
                .ThenInclude(s => s.Skill)
                .Include(e => e.Opportunity)
                .FirstOrDefaultAsync(m => m.StudentApplicationId == id && (!isStudent || (isStudent && m.Student.StudentId == user.Id)));

            if (studentApplication == null)
            {
                return NotFound();
            }

            ViewData["IsStudent"] = isStudent;
            return View(studentApplication);
        }

        // GET: Students/Create
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Create([FromQuery] int? opportunityId)
        {
            var user = await _userManager.GetUserAsync(this.User);
            await GetStudentAsync(user);
            if (opportunityId == null)
            {
                ViewData["Opportunities"] = await _context.Opportunities
                    .Include(e => e.Applications)
                    .ThenInclude(e => e.Student)
                    .Where(e => !e.Applications.Any(a => a.Student.StudentId == user.Id))
                    .ToListAsync();
                return View();
            }

            var oldApplication = await _context.StudentApplications
                .Include(e => e.Student)
                .Include(e => e.Opportunity)
                .Where(e => e.Student.StudentId == user.Id && e.Opportunity.OpportunityId == opportunityId)
                .FirstOrDefaultAsync();

            if (oldApplication != null)
            {
                // Thanks to https://stackoverflow.com/questions/10785245/redirect-to-action-in-another-controller
                return RedirectToAction("Edit", "Students", new { id = oldApplication.StudentApplicationId });
            }

            ViewData["Opportunities"] = await _context.Opportunities
                    .Where(e => e.OpportunityId == (int)opportunityId)
                    .ToListAsync();

            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Create([Bind("OpportunityId,ExpectedGraduation,DegreePlan,GPA,Availability,PersonalStatement,IsLookingForPosition")] StudentApplication studentApplication)
        {
            var user = await _userManager.GetUserAsync(this.User);
            var oldApplication = await _context.StudentApplications
                .Include(e => e.Student)
                .Include(e => e.Opportunity)
                .Where(e => e.Student.StudentId == user.Id && e.Opportunity.OpportunityId == studentApplication.OpportunityId)
                .FirstOrDefaultAsync();

            if(oldApplication != null)
            {
                return BadRequest("Cannot create application to oportunity you have already applied to");
            }

            var student = await GetStudentAsync(user);
            studentApplication.Student = student;
            studentApplication.UId = user.UId;
            studentApplication.TimeModified = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                _context.Add(studentApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentApplication);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _userManager.GetUserAsync(this.User);
            var student = await GetStudentAsync(user);
            var studentApplication = await _context.StudentApplications
                .Include(o => o.Student)
                .Include(o => o.Opportunity)
                .Where(o => o.StudentApplicationId == id)
                .FirstOrDefaultAsync();
            if (studentApplication == null)
            {
                return NotFound();
            }

            if (studentApplication.Student.StudentId != student.StudentId)
            {
                return BadRequest(new { message = "You do not have permission to edit this application" });
            }

            return View(studentApplication);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Edit(int id, [Bind("OpportunityId,ExpectedGraduation,DegreePlan,GPA,Availability,PersonalStatement,IsLookingForPosition")] StudentApplication studentApplication)
        {
            var user = await _userManager.GetUserAsync(this.User);
            var student = await _context.Students.FindAsync(user.Id);
            var application = await _context.StudentApplications
                .Include(o => o.Student)
                .FirstOrDefaultAsync(m => m.Student.StudentId == user.Id);

            studentApplication.Student = student;
            studentApplication.StudentApplicationId = application.StudentApplicationId;

            if (id != studentApplication.StudentApplicationId)
            {
                return NotFound();
            }

            if (studentApplication.Student.StudentId != user.Id)
            {
                return BadRequest(new { message = "You do not have permission to edit this application" });
            }

            ModelState.Remove("StudentId");
            ModelState.Remove("StudentApplicationId");
            if (ModelState.IsValid)
            {
                try
                {
                    application.ExpectedGraduation = studentApplication.ExpectedGraduation;
                    application.DegreePlan = studentApplication.DegreePlan;
                    application.GPA = studentApplication.GPA;
                    application.UId = studentApplication.UId;
                    application.Availability = studentApplication.Availability;
                    application.PersonalStatement = studentApplication.PersonalStatement;
                    application.IsLookingForPosition = studentApplication.IsLookingForPosition;

                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentApplicationExists(studentApplication.StudentApplicationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(studentApplication);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentApplication = await _context.StudentApplications
                .Include(o => o.Student)
                .ThenInclude(s => s.Courses)
                .ThenInclude(s => s.Course)
                .Include(o => o.Student)
                .ThenInclude(s => s.Interests)
                .ThenInclude(s => s.Interest)
                .Include(o => o.Student)
                .ThenInclude(s => s.Skills)
                .ThenInclude(s => s.Skill)
                .Include(o => o.Opportunity)
                .FirstOrDefaultAsync(m => m.StudentApplicationId == id);

            if (studentApplication == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(this.User);
            if (studentApplication.Student.StudentId != user.Id)
            {
                return BadRequest(new { message = "You do not have permission to edit this application" });
            }

            return View(studentApplication);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentApplication = await _context.StudentApplications
                .Include(o => o.Student)
                .Where(o => o.StudentApplicationId == id)
                .FirstOrDefaultAsync();

            var user = await _userManager.GetUserAsync(this.User);
            if (studentApplication.Student.StudentId != user.Id)
            {
                return BadRequest(new { message = "You do not have permission to delete this application" });
            }

            _context.StudentApplications.Remove(studentApplication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentApplicationExists(int id)
        {
            return _context.StudentApplications.Any(e => e.StudentApplicationId == id);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddTag(string type, string value)
        {
            var user = await _userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return BadRequest();
            }
            var student = await _context.Students
                .Include(e => e.Courses)
                .ThenInclude(e => e.Course)
                .Include(e => e.Interests)
                .ThenInclude(e => e.Interest)
                .Include(e => e.Skills)
                .ThenInclude(e => e.Skill)
                .Where(e => e.StudentId == user.Id)
                .FirstOrDefaultAsync();

            // Create extended user implicitly
            if (student == null)
            {
                student = new Student
                {
                    StudentId = user.Id,
                    Courses = new List<StudentCourse>(),
                    Interests = new List<StudentInterest>(),
                    Skills = new List<StudentSkill>()
                };

                _context.Students.Add(student);
                await _context.SaveChangesAsync();
            }

            // Have to duplicate if bodies because cannot define generic DbSet<T> variable
            if (type == "course")
            {
                var course = await _context.Courses
                    .Where(e => e.Name.ToLower() == value.ToLower())
                    .FirstOrDefaultAsync();

                if (course == null)
                {
                    course = new Course
                    {
                        Name = value
                    };
                    course = _context.Courses.Add(course).Entity;
                    await _context.SaveChangesAsync();
                }

                if (!student.Courses.Any(e => e.Course.Name.ToLower() == value.ToLower()))
                {
                    _context.StudentCourses.Add(new StudentCourse { StudentId = student.StudentId, CourseId = course.CourseId });
                }
            }
            else if (type == "interest")
            {
                var interest = await _context.Interests
                    .Where(e => e.Name.ToLower() == value.ToLower())
                    .FirstOrDefaultAsync();

                if (interest == null)
                {
                    interest = new Interest
                    {
                        Name = value
                    };
                    interest = _context.Interests.Add(interest).Entity;
                    await _context.SaveChangesAsync();
                }

                if (!student.Interests.Any(e => e.Interest.Name.ToLower() == value.ToLower()))
                {
                    _context.StudentInterests.Add(new StudentInterest { StudentId = student.StudentId, InterestId = interest.InterestId });
                }
            }
            else if (type == "skill")
            {
                var skill = await _context.Skills
                    .Where(e => e.Name.ToLower() == value.ToLower())
                    .FirstOrDefaultAsync();

                if (skill == null)
                {
                    skill = new Skill
                    {
                        Name = value
                    };
                    skill = _context.Skills.Add(skill).Entity;
                    await _context.SaveChangesAsync();
                }

                if (!student.Skills.Any(e => e.Skill.Name.ToLower() == value.ToLower()))
                {
                    _context.StudentSkills.Add(new StudentSkill { StudentId = student.StudentId, SkillId = skill.SkillId });
                }
            }
            else
            {
                return BadRequest();
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveTag(string type, string value)
        {
            var user = await _userManager.GetUserAsync(this.User);
            if (user == null || string.IsNullOrEmpty(value))
            {
                return BadRequest();
            }

            var student = await _context.Students
                .Include(e => e.Courses)
                .Include(e => e.Interests)
                .Include(e => e.Skills)
                .Where(e => e.StudentId == user.Id)
                .FirstOrDefaultAsync();

            // Create extended user implicitly
            if (student == null)
            {
                student = new Student
                {
                    StudentId = user.Id,
                    Courses = new List<StudentCourse>(),
                    Interests = new List<StudentInterest>(),
                    Skills = new List<StudentSkill>()
                };

                _context.Students.Add(student);
                await _context.SaveChangesAsync();
            }

            // Have to duplicate if bodies because cannot define generic DbSet<T> variable
            if (type == "course")
            {
                var course = await _context.StudentCourses
                    .Include(e => e.Student)
                    .Include(e => e.Course)
                    .Where(e => e.StudentId == student.StudentId && e.Course.Name.ToLower() == value.ToLower())
                    .FirstOrDefaultAsync();

                if (course != null)
                {
                    _context.StudentCourses.Remove(course);
                }
            }
            else if (type == "interest")
            {
                var interest = await _context.StudentInterests
                    .Include(e => e.Student)
                    .Include(e => e.Interest)
                    .Where(e => e.StudentId == student.StudentId && e.Interest.Name.ToLower() == value.ToLower())
                    .FirstOrDefaultAsync();

                if (interest != null)
                {
                    _context.StudentInterests.Remove(interest);
                }
            }
            else if (type == "skill")
            {
                var skill = await _context.StudentSkills
                    .Include(e => e.Student)
                    .Include(e => e.Skill)
                    .Where(e => e.StudentId == student.StudentId && e.Skill.Name.ToLower() == value.ToLower())
                    .FirstOrDefaultAsync();

                if (skill != null)
                {
                    _context.StudentSkills.Remove(skill);
                }
            }
            else
            {
                return BadRequest();
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        // File Upload for Opportunities
        // POST Students/ResumeUpload/:id
        [HttpPost]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> ResumeUpload(List<IFormFile> files, int id)
        {
            try
            {
                if (files.Count != 1)
                {
                    return BadRequest(new { message = "Must Submit One File" });
                }

                long size = files.Sum(f => f.Length);
                var filePaths = new List<string>();
                var new_name = "";
                foreach (var formFile in files)
                {
                    // check formFile length to make sure file is a reasonable size
                    // 50 * 1024 * 1024 is 50 MB to Bytes
                    if (formFile.Length > 50 * 1024 * 1024)
                    {
                        return BadRequest(new { message = "File must be less than 50MB." });
                    }
                    // check formFile length to make sure it’s not zero...
                    if (formFile.Length <= 0)
                    {
                        return BadRequest(new { message = "An invalid file was uploaded." });
                    }

                    // Reference from https://stackoverflow.com/questions/8477664/how-can-i-generate-uuid-in-c-sharp
                    // and from https://stackoverflow.com/questions/24625078/how-generate-a-unique-file-name-at-upload-files-in-webserver-mvc

                    string ext = Path.GetExtension(formFile.FileName);
                    // Rudimentary file type security
                    if (!acceptedResumeFormats.Contains(ext))
                    {
                        return BadRequest(new { message = "Invalid file format." });
                    }

                    new_name = Guid.NewGuid().ToString() + ext;

                    var user = await _userManager.GetUserAsync(this.User);
                    var application = await _context.StudentApplications.FindAsync(id);

                    if (application == null)
                    {
                        return BadRequest(new { message = "Could not find an application with that id." });
                    }

                    if (application.Student.StudentId != user.Id)
                    {
                        return BadRequest(new { message = "You do not have permission to do this." });
                    }

                    var filePath = Path.Combine(_configuration["ResumeUploadPath"], new_name);
                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    application.ResumeFilename = new_name;
                    _context.StudentApplications.Update(application);
                    await _context.SaveChangesAsync();
                }
                return Ok(new { message = new_name, count = files.Count, size, filePaths });
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                return BadRequest(new { message = "Error has occured" });
            }
        }

        /// <summary>
        /// Gets a student object for the given user, creating one if it does not exist.
        /// </summary>
        /// <param name="user">The user to get the student of</param>
        /// <returns>The student data for the user</returns>
        private async Task<Student> GetStudentAsync(ApplicationUser user)
        {
            var student = await _context.Students.FindAsync(user.Id);
            if (student == null)
            {
                student = _context.Students.Add(new Student {
                    StudentId = user.Id
                }).Entity;
                await _context.SaveChangesAsync();
            }

            return student;
        }

        // Student/Search
        [Authorize(Roles = "Admin,Professor")]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Professor")]
        public async Task<JsonResult> Find(string keywords)
        {
            List<StudentSummary> summaries = new List<StudentSummary>();

            // No keywords, no results (avoid the hassle of sending a 400 with JsonResult in ASP.NET)
            if (keywords == null)
            {
                return Json(summaries);
            }

            string[] words = keywords.Split(",");
            var applications = _context.StudentApplications
                .Include(o => o.Student)
                .ThenInclude(s => s.Skills);

            foreach (var app in applications)
            {
                HashSet<string> skills = new HashSet<string>();
                foreach (var skill in app.Student.Skills)
                {
                    skills.Add(skill.Skill.Name.ToLower());
                }

                // an OR search, not AND
                foreach (var word in words)
                {
                    if (skills.Contains(word.Trim().ToLower()))
                    {
                        var student = await _userManager.FindByIdAsync(app.Student.StudentId);
                        StudentSummary summary = new StudentSummary
                        {
                            Name = student.UserName,
                            ApplicationId = app.StudentApplicationId,
                            UId = app.UId,
                            GPA = app.GPA,
                            Skills = skills.ToList<string>(),
                            Summary = app.PersonalStatement
                        };
                        summaries.Add(summary);
                        break;
                    }
                }
            }

            return Json(summaries);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> Apply(string id, string state)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var application = await _context.StudentApplications
                .Include(o => o.Student)
                .FirstOrDefaultAsync(e => e.Student.StudentId == user.Id);

            if (application == null)
            {
                return NotFound();
            }

            if (application.Student.StudentId != user.Id)
            {
                return BadRequest(new { message = "You do not have permission to do this." });
            }

            if (state == "apply")
            {
                application.IsLookingForPosition = true;
            }
            else if (state == "remove")
            {
                application.IsLookingForPosition = false;
            }
            else
            {
                return BadRequest(new { message = "State must be either \"apply\" or \"remove\"" });
            }

            _context.Update(application);
            await _context.SaveChangesAsync();

            // Thanks https://stackoverflow.com/questions/37690114/how-to-return-a-specific-status-code-and-no-contents-from-controller
            return NoContent();
        }

        struct StudentSummary
        {
            public string Name { get; set; }
            public int ApplicationId { get; set; }
            public string UId { get; set; }
            public double GPA { get; set; }
            public List<string> Skills { get; set; }
            public string Summary { get; set; }
        }
    }
}
