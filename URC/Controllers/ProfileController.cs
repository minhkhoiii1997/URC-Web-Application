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
 * Controller for handling profiles
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URC.Areas.Identity.Data;
using URC.Data;
using URC.Models;

namespace URC.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly URC_Context _context;

        public ProfileController(UserManager<ApplicationUser> userManager, URC_Context context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: ProfileController
        // Thanks to https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-5.0#route-name
        // for route parameters
        [HttpGet("/Profile/{username}")]
        public async Task<ActionResult> Index(string username)
        {
            if(string.IsNullOrEmpty(username))
            {
                return NotFound("No user with that username could be found.");
            }
            var user = await _userManager.FindByNameAsync(username);
            if(user == null)
            {
                return Redirect("/Identity/Account/Login");
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

            if(student == null)
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

            var viewer = await _userManager.GetUserAsync(this.User);
            if(viewer != null)
            {
                ViewData["IsSignedIn"] = true;
                ViewData["IsViewerStudent"] = await _userManager.IsInRoleAsync(viewer, "Student");
                ViewData["ViewerId"] = viewer.Id;
                ViewData["ViewerName"] = viewer.Name;
            } else
            {
                ViewData["IsSignedIn"] = false;
                ViewData["IsViewerStudent"] = false;
                ViewData["ViewerId"] = -1;
            }

            ViewData["Student"] = student;
            return View(user);
        }

        // GET: ProfileController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
