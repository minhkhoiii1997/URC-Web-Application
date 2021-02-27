/**
 * Author:    Kevin Tran
 * Partner:   Calvin Tu
 * Date:      September 2020
 * Course:    CS 4540, University of Utah, School of Computing
 * Copyright: CS 4540 and Kevin Tran and Calvin Tu - This work may not be copied for use in Academic Coursework.
 *
 * We, Kevin Tran, Calvin Tu certify that we wrote this code from scratch and did 
 * not copy it in part or whole from another source.  Any references used 
 * in the completion of the assignment are cited in my README file and in
 * the appropriate method header.
 *
 * File Contents
 * Controller for opportunities
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using URC.Data;
using URC.Models;
using URC.Areas.Identity.Data;

namespace URC.Controllers
{
    
    public class OpportunitiesController : Controller
    {
        private readonly URC_Context _context;

        private IConfiguration _configuration;

        private readonly UserManager<ApplicationUser> _userManager;

        public OpportunitiesController(URC_Context context, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
        }

        // GET: Opportunities
        public async Task<IActionResult> Index()
        {

            //Visitor counter++
            var con = HttpContext.Session;
            if (con.Get("Date") == null)
            {
                con.SetString("Date", DateTime.Now.ToString("HH"));
                var visiter_info = await _context.GeneralInfos.FirstOrDefaultAsync(o => o.ID == 1);
                visiter_info.number++;
                _context.Update(visiter_info);
                await _context.SaveChangesAsync();
            }
            else
            {
                var current = DateTime.Now.ToString("HH");
                int a = Int32.Parse(current);
                int b = Int32.Parse(con.GetString("Date"));

                if (Math.Abs(a - b) >= 1)
                {
                    con.SetString("Date", DateTime.Now.ToString("mm"));
                    var visiter_info = await _context.GeneralInfos.FirstOrDefaultAsync(o => o.ID == 1);
                    visiter_info.number++;
                    _context.Update(visiter_info);
                    await _context.SaveChangesAsync();
                }
            }
            ///////////////////////////////////////




            var opportunities = await _context.Opportunities
                .Include(o => o.Professor)
                .Include(o => o.Tags)
                .ThenInclude(o => o.SearchTag)
                .Include(o => o.RequiredSkills)
                .ThenInclude(o => o.Skill)
                .Include(o => o.PreferredSkills)
                .ThenInclude(o => o.Skill)
                .AsNoTracking()
                .ToListAsync();

            return View(opportunities);
        }

        // GET: Opportunities/List
        [Authorize(Roles = "Admin,Professor")]
        public async Task<IActionResult> List()
        {
            //Reference from https://stackoverflow.com/questions/38751616/asp-net-core-identity-get-current-user
            if (this.User.IsInRole("Admin"))
            {
                ViewData["IsAdmin"] = true;
                return View(await _context.Opportunities.ToListAsync());
            }
            else
            {
                var user = await _userManager.GetUserAsync(this.User);
                var opportunities = await _context.Opportunities.Include(o => o.Professor).Where(o => o.Professor.ProfessorId == user.Id).ToListAsync();
                ViewData["IsAdmin"] = false;
                return View(opportunities);
            }
        }


        // GET: Opportunities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var opportunities = await _context.Opportunities
                .Include(o => o.Professor)
                .Include(o => o.Tags)
                .ThenInclude(o => o.SearchTag)
                .Include(o => o.RequiredSkills)
                .ThenInclude(o => o.Skill)
                .Include(o => o.PreferredSkills)
                .ThenInclude(o => o.Skill)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.OpportunityId == id);

            if (id == null)
            {
                return NotFound();
            }

            if (opportunities == null)
            {
                return NotFound();
            }

            //update counter++
            var counter = await _context.OpportunityCounters
                .FirstOrDefaultAsync(m => m.OpportunityId == id);
            counter.counter++;
            _context.Update(counter);
            await _context.SaveChangesAsync();
            /////////////////////////////////////////////////////


            return View(opportunities);
        }

        // GET: Opportunities/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var users = _userManager.Users.ToList();
            List<ApplicationUser> professors = new List<ApplicationUser>();
            foreach(var user in users)
            {
                var roles = _userManager.GetRolesAsync(user).Result.ToList();
                if(roles.Contains("Professor"))
                {
                    professors.Add(user);
                }
            }
            ViewData["Professors"] = professors;
            return View();
        }

        // POST: Opportunities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Name,Description,RoleDescription,Responsibilities,Image,Mentor,Deadline,Pay,IsFilled,ProfessorId")] Opportunity opportunity)
        {
            opportunity.Professor = await _context.Professors.FindAsync(opportunity.ProfessorId);
            if (ModelState.IsValid)
            {
                opportunity.PostedDate = DateTime.UtcNow;
                opportunity.IsFilled = false;

                //add new counter for new opportunity
                opportunity.OpportunityCounter = new OpportunityCounter { counter = 0 };
                /////////////////////////////////////

                _context.Add(opportunity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var users = _userManager.Users.ToList();
            List<ApplicationUser> professors = new List<ApplicationUser>();
            foreach (var user in users)
            {
                var roles = _userManager.GetRolesAsync(user).Result.ToList();
                if (roles.Contains("Professor"))
                {
                    professors.Add(user);
                }
            }
            ViewData["Professors"] = professors;

            return View(opportunity);
        }

        // GET: Opportunities/Edit/5
        [Authorize(Roles = "Admin,Professor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(this.User);
            bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            var opportunity = _context.Opportunities
                .Include(o => o.Professor)
                .Include(o => o.Tags)
                .ThenInclude(o => o.SearchTag)
                .Include(o => o.RequiredSkills)
                .ThenInclude(o => o.Skill)
                .Include(o => o.PreferredSkills)
                .ThenInclude(o => o.Skill)
                .Where(o => (isAdmin || o.Professor.ProfessorId == user.Id) && o.OpportunityId == id)
                .FirstOrDefault();
            if (opportunity == null)
            {
                return NotFound();
            }
            return View(opportunity);
        }

        // POST: Opportunities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Professor")]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Professor,Description,RoleDescription,Responsibilities,Image,Mentor,Deadline,Pay,IsFilled")] Opportunity opportunity)
        {
            if (id != opportunity.OpportunityId)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(this.User);
            bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            var originalOpportunity = _context.Opportunities
                .Include(o => o.Professor)
                .Where(o => (isAdmin || o.Professor.ProfessorId == user.Id) && o.OpportunityId == id)
                .FirstOrDefault();

            if(originalOpportunity == null)
            {
                return BadRequest(new { message = "Opportunitiy does not exist" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opportunity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpportunityExists(opportunity.OpportunityId))
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
            return View(opportunity);
        }

        // GET: Opportunities/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opportunity = await _context.Opportunities
                .FirstOrDefaultAsync(m => m.OpportunityId == id);
            if (opportunity == null)
            {
                return NotFound();
            }

            return View(opportunity);
        }

        // POST: Opportunities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var opportunity = await _context.Opportunities.FindAsync(id);
            _context.Opportunities.Remove(opportunity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpportunityExists(int id)
        {
            return _context.Opportunities.Any(e => e.OpportunityId == id);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddTag(string type, string value, int? id)
        {
            var user = await _userManager.GetUserAsync(this.User);
            if (user == null || type == null || value == null || id == null)
            {
                return BadRequest();
            }
            var professor = await _context.Professors
                .Where(e => e.ProfessorId == user.Id)
                .FirstOrDefaultAsync();

            // Create extended user implicitly
            if (professor == null)
            {
                professor = new Professor
                {
                    ProfessorId = user.Id,
                    Name = user.Name,
                    Opportunities = new List<Opportunity>()
                };

                _context.Professors.Add(professor);
                await _context.SaveChangesAsync();
            }

            var opportunity = await _context.Opportunities
                .Include(e => e.Tags)
                .ThenInclude(e => e.SearchTag)
                .Include(e => e.RequiredSkills)
                .ThenInclude(e => e.Skill)
                .Include(e => e.PreferredSkills)
                .ThenInclude(e => e.Skill)
                .Where(e => e.Professor.ProfessorId == professor.ProfessorId && e.OpportunityId == id)
                .FirstOrDefaultAsync();

            if(opportunity == null)
            {
                return NotFound($"No opportunity with id {id} found.");
            }

            // Have to duplicate if bodies because cannot define generic DbSet<T> variable
            if (type == "searchtag")
            {
                var tag = await _context.SearchTags
                    .Where(e => e.Name.ToLower() == value.ToLower())
                    .FirstOrDefaultAsync();

                if (tag == null)
                {
                    tag = new SearchTag
                    {
                        Name = value
                    };
                    tag = _context.SearchTags.Add(tag).Entity;
                    await _context.SaveChangesAsync();
                }

                if (!opportunity.Tags.Any(e => e.SearchTag.Name.ToLower() == value.ToLower()))
                {
                    _context.OpportunitySearchTags.Add(new OpportunitySearchTag { OpportunityId = (int)id, SearchTagId = tag.SearchTagId });
                }
            }
            else if (type == "requiredskill")
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

                if (!opportunity.RequiredSkills.Any(e => e.Skill.Name.ToLower() == value.ToLower()))
                {
                    _context.OpportunityRequiredSkills.Add(new OpportunityRequiredSkill { OpportunityId = (int)id, SkillId = skill.SkillId });
                }
            }
            else if (type == "preferredskill")
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

                if (!opportunity.PreferredSkills.Any(e => e.Skill.Name.ToLower() == value.ToLower()))
                {
                    _context.OpportunityPreferredSkills.Add(new OpportunityPreferredSkill { OpportunityId = (int)id, SkillId = skill.SkillId });
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
        public async Task<IActionResult> RemoveTag(string type, string value, int? id)
        {
            var user = await _userManager.GetUserAsync(this.User);
            if (user == null || string.IsNullOrEmpty(value) || id == null)
            {
                return BadRequest();
            }

            var professor = await _context.Professors
                .Where(e => e.ProfessorId == user.Id)
                .FirstOrDefaultAsync();

            // Create extended user implicitly
            if (professor == null)
            {
                professor = new Professor
                {
                    ProfessorId = user.Id,
                    Opportunities = new List<Opportunity>()
                };

                _context.Professors.Add(professor);
                await _context.SaveChangesAsync();
            }

            // Have to duplicate if bodies because cannot define generic DbSet<T> variable
            if (type == "searchtag")
            {
                var tag = await _context.OpportunitySearchTags
                    .Include(e => e.Opportunity)
                    .ThenInclude(e => e.Professor)
                    .Include(e => e.SearchTag)
                    .Where(e => e.Opportunity.Professor.ProfessorId == professor.ProfessorId && e.SearchTag.Name.ToLower() == value.ToLower())
                    .FirstOrDefaultAsync();

                if (tag != null)
                {
                    _context.OpportunitySearchTags.Remove(tag);
                }
            }
            else if (type == "requiredskill")
            {
                var skill = await _context.OpportunityRequiredSkills
                    .Include(e => e.Opportunity)
                    .ThenInclude(e => e.Professor)
                    .Include(e => e.Skill)
                    .Where(e => e.Opportunity.Professor.ProfessorId == professor.ProfessorId && e.Skill.Name.ToLower() == value.ToLower())
                    .FirstOrDefaultAsync();

                if (skill != null)
                {
                    _context.OpportunityRequiredSkills.Remove(skill);
                }
            }
            else if (type == "preferredskill")
            {
                var skill = await _context.OpportunityPreferredSkills
                    .Include(e => e.Opportunity)
                    .ThenInclude(e => e.Professor)
                    .Include(e => e.Skill)
                    .Where(e => e.Opportunity.Professor.ProfessorId == professor.ProfessorId && e.Skill.Name.ToLower() == value.ToLower())
                    .FirstOrDefaultAsync();

                if (skill != null)
                {
                    _context.OpportunityPreferredSkills.Remove(skill);
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
        // POST Opportunities/CoverImageUpload/:id
        [HttpPost]
        [Authorize(Roles = "Admin,Professor")]
        public async Task<IActionResult> CoverImageUpload(List<IFormFile> files, int id)
        {
            try
            {
                if (files.Count != 1)
                {
                    return BadRequest(new { message = "You can submit only a single file." });
                }

                long size = files.Sum(f => f.Length);
                var filePaths = new List<string>();
                var new_name = "";
                foreach (var formFile in files)
                {
                    // check formFile length to make sure file is a reasonable size
                    // 50 * 1024 * 1024 is 50 MB to Bytes
                    if(formFile.Length > 50*1024*1024)
                    {
                        return BadRequest(new { message = "File must be less than 50MB." });
                    }
                    // check formFile length to make sure it’s not zero...
                    if(formFile.Length <= 0)
                    {
                        return BadRequest(new { message = "An invalid file was uploaded." });
                    }

                    // Reference from https://stackoverflow.com/questions/8477664/how-can-i-generate-uuid-in-c-sharp
                    // and from https://stackoverflow.com/questions/24625078/how-generate-a-unique-file-name-at-upload-files-in-webserver-mvc

                    new_name = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);

                    var user = await _userManager.GetUserAsync(this.User);
                    bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                    var opportunity = _context.Opportunities
                        .Include(o => o.Professor)
                        .Where(o => (isAdmin || o.Professor.ProfessorId == user.Id) && o.OpportunityId == id)
                        .FirstOrDefault();

                    if(opportunity == null)
                    {
                        return BadRequest(new { message = "Could not find an opportunity with that id." });
                    }

                    var filePath = Path.Combine(_configuration["ImageUploadPath"], new_name);
                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    opportunity.Image = new_name;
                    _context.Opportunities.Update(opportunity);
                    await _context.SaveChangesAsync();
                }
                return Ok(new { message = new_name, count = files.Count, size, filePaths });
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                return BadRequest(new { message = "Error has occured" });
            }
        }
    }
}