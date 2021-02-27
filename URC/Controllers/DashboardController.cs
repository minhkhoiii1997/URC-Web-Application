/**
 * Author:    Ping Cheng Chung
 * Partner:   Calvin Tu,Kevin Tran
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
 *  Dashboard controller
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
using Microsoft.AspNetCore.Http;
using URC.Areas.Identity.Data;
using System.Threading;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace URC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly URC_Context _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(URC_Context context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //check set the current culture before creat other threads
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var lang = Request.Cookies["lang"];
            switch (lang)
            {
                case "zh":
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("zh");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("zh");
                    break;
                case "en":
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en");
                    break;
                default:
                    Response.Cookies.Append("lang", "en");
                    break;
            }

            base.OnActionExecuting(context);
        }

        public async Task<IActionResult> Index()
        {
            var con = HttpContext;
            con.Session.SetString("keyname", "123");
            var fijf = con.Session.GetString("keyname");

            var aisdjis = Resources.dashboard.title;


            var dede = Response.Cookies;
            var isj = Request.Cookies[""];

            var visiter_info = await _context.GeneralInfos.FirstOrDefaultAsync(o => o.ID == 1);
            ViewData["visitor"] = visiter_info.number;

            var online_info = await _context.GeneralInfos.FirstOrDefaultAsync(o => o.ID == 2);
            ViewData["online_"] = online_info.number;

            var student = (await _userManager.GetUsersInRoleAsync("Student")).Count;
            var Professor = (await _userManager.GetUsersInRoleAsync("Professor")).Count;
            ViewData["students"] = student;
            ViewData["professors"] = Professor;

            //Applications
            var applications = (await _context.StudentApplications.ToArrayAsync()).Length;
            var LFJ = (await _context.StudentApplications.Where(x => x.IsLookingForPosition).ToArrayAsync()).Length;
            ViewData["applications"] = applications;
            ViewData["LF"] = (LFJ * 100) / applications;

            //Opportunities
            var opportunities = (await _context.Opportunities.ToArrayAsync()).Length;
            var open_opportunities = (await _context.Opportunities.Where(x => !(x.IsFilled)).ToArrayAsync()).Length;
            ViewData["opportunities"] = opportunities;
            ViewData["open_opportunities"] = (open_opportunities * 100) / opportunities;

            //top5 popular opportunities

            var Opportunity_array = (await _context.Opportunities.Include(x => x.OpportunityCounter).Where(x => !(x.IsFilled)).OrderByDescending(x => x.OpportunityCounter.counter).ToListAsync()).ToArray();
            if (Opportunity_array.Length > 4)
            {

                var top1_counter = Opportunity_array[0].OpportunityCounter.counter;
                ViewData["top_1"] = top1_counter;
                ViewData["top_1_name"] = Opportunity_array[0].Name;
                ViewData["top_1_percentage"] = 100;
                ViewData["top_1_ID"] = Opportunity_array[0].OpportunityId;

                var top2_counter = Opportunity_array[1].OpportunityCounter.counter;
                ViewData["top_2"] = top2_counter;
                ViewData["top_2_name"] = Opportunity_array[1].Name;
                ViewData["top_2_percentage"] = top2_counter * 100 / top1_counter;
                ViewData["top_2_ID"] = Opportunity_array[1].OpportunityId;

                var top3_counter = Opportunity_array[2].OpportunityCounter.counter;
                ViewData["top_3"] = top3_counter;
                ViewData["top_3_name"] = Opportunity_array[2].Name;
                ViewData["top_3_percentage"] = top3_counter * 100 / top1_counter;
                ViewData["top_3_ID"] = Opportunity_array[2].OpportunityId;

                var top4_counter = Opportunity_array[3].OpportunityCounter.counter;
                ViewData["top_4"] = top4_counter;
                ViewData["top_4_name"] = Opportunity_array[3].Name;
                ViewData["top_4_percentage"] = top4_counter * 100 / top1_counter;
                ViewData["top_4_ID"] = Opportunity_array[3].OpportunityId;

                var top5_counter = Opportunity_array[4].OpportunityCounter.counter;
                ViewData["top_5"] = top5_counter;
                ViewData["top_5_name"] = Opportunity_array[4].Name;
                ViewData["top_5_percentage"] = top5_counter * 100 / top1_counter;
                ViewData["top_5_ID"] = Opportunity_array[4].OpportunityId;
            }
            return View();
        }


        //set language to Chinese in cookies
        public ActionResult setChinese()
        {
            Response.Cookies.Append("lang", "zh");
            return RedirectToAction("Index", "Dashboard");
        }
        //set language to English in cookies
        public ActionResult setEnglish()
        {
            Response.Cookies.Append("lang", "en");
            return RedirectToAction("Index", "Dashboard");
        }

        //return with students' interest dictionary for ajax get request
        [HttpGet]
        public async Task<IActionResult> GetStudentsInerestData()
        {
            //Required Skill
            // var skill_list = (await _context.Skills.ToArrayAsync()).ToArray();
            var interest = (await _context.StudentInterests.Include(o => o.Interest).ToArrayAsync()).ToArray();
            Dictionary<string, int> interest_map = new Dictionary<string, int>();


            foreach (var c in interest)
            {
                var key = c.Interest.Name;
                if (!(interest_map.ContainsKey(key)))
                    interest_map.Add(key, 0);
                interest_map[key]++;
            }

            var myList = interest_map.ToList();
            myList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            return Ok(myList);
        }

        //return with students' skills dictionary for ajax get request
        [HttpGet]
        public async Task<IActionResult> GetStudentSkillData()
        {
            //Required Skill
            // var skill_list = (await _context.Skills.ToArrayAsync()).ToArray();
            var skills = (await _context.StudentSkills.Include(o => o.Skill).ToArrayAsync()).ToArray();
            int number_skils = skills.Length;
            Dictionary<string, int> skill_map = new Dictionary<string, int>();


            foreach (var c in skills)
            {
                var key = c.Skill.Name;
                if (!(skill_map.ContainsKey(key)))
                    skill_map.Add(key, 0);
                skill_map[key]++;
            }

            var myList = skill_map.ToList();
            myList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            return Ok(myList);
        }


        //return with porjects' required skills dictionary for ajax get request
        [HttpGet]
        public async Task<IActionResult> GetReuiredSkillData()
        {
            //Required Skill
            // var skill_list = (await _context.Skills.ToArrayAsync()).ToArray();
            var skills = (await _context.OpportunityRequiredSkills.Include(o => o.Skill).ToArrayAsync()).ToArray();
            int number_skils = skills.Length;
            Dictionary<string, int> skill_map = new Dictionary<string, int>();


            foreach (var c in skills)
            {
                var key = c.Skill.Name;
                if (!(skill_map.ContainsKey(key)))
                    skill_map.Add(key, 0);
                skill_map[key]++;
            }

            var myList = skill_map.ToList();
            myList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            return Ok(myList);
        }

        [HttpGet]
        public async Task<IActionResult> GetCompareSkillsData()
        {
            //Required Skill
            // var skill_list = (await _context.Skills.ToArrayAsync()).ToArray();
            var skills = (await _context.OpportunityRequiredSkills.Include(o => o.Skill).ToArrayAsync()).ToArray();
            int number_skils = skills.Length;
            Dictionary<string, int> skill_map = new Dictionary<string, int>();


            foreach (var c in skills)
            {
                var key = c.Skill.Name;
                if (!(skill_map.ContainsKey(key)))
                    skill_map.Add(key, 0);
                skill_map[key]++;
            }

            var myList = skill_map.ToList();
            myList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            int l = (myList.Count < 5) ? myList.Count : 5;
            Da[] data = new Da[l];


            for (int i = 0; i < l; i++)
            {

                var n = (await _context.StudentSkills.Include(o => o.Skill).Where(o => o.Skill.Name.Equals(myList[i].Key)).ToArrayAsync()).Length;


                data[i].name = myList[i].Key;
                data[i].reuquiredskill = myList[i].Value;
                data[i].studentskill = n;
            }

            return Ok(data);
        }

        //find the first 5 required
        private struct Da
        {

            public string name { get; set; }
            public int reuquiredskill { get; set; }
            public int studentskill { get; set; }
        }
    }
}
