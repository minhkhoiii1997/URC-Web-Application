/**
 * Author:    Kevin Tran
 * Partner:   Calvin Tu
 * Date:      October 2nd, 2020
 * Course:    CS 4540, University of Utah, School of Computing
 * Copyright: CS 4540 and Kevin Tran and Calvin Tu - This work may not be copied for use in Academic Coursework.
 *
 * We, Kevin Tran, Calvin Tu certify that we wrote this code from scratch and did 
 * not copy it in part or whole from another source.  Any references used 
 * in the completion of the assignment are cited in my README file and in
 * the appropriate method header.
 *
 * File Contents
 * Controller for admin actions
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using URC.Areas.Identity.Data;

namespace URC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: AdminController
        public ActionResult Index()
        {
            var users = _userManager.Users.ToList();

            // Model is Email --> (userId, roles)
            Dictionary<string, Tuple<string, List<string>>> userRolesMapping = new Dictionary<string, Tuple<string,List<string>>>();
            foreach(var user in users)
            {
                var roles = _userManager.GetRolesAsync(user).Result.ToList();
                userRolesMapping[user.Email] = new Tuple<string, List<string>>(user.Id, roles);
            }

            return View(userRolesMapping);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(string userId, string role, bool removeRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return BadRequest(new { success = false, message = $"Could not find user by id: {userId}" });
            }

            var roleExists = await _roleManager.RoleExistsAsync(role);

            if(!roleExists)
            {
                return BadRequest(new { success = false, message = $"Could not find role: {role}" });
            }

            if(removeRole)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
                return Ok(new { success = true, message = $"Removed {role} role from {user.Email} successfully." });
            } else
            {
                await _userManager.AddToRoleAsync(user, role);
                return Ok(new { success = true, message = $"Added {role} role to {user.Email} successfully." });
            }

        }
    }
}
