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
 * Basic home controller, essentially just serves the landing page
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using URC.Data;
using URC.Models;

namespace URC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly URC_Context _context;
        public HomeController(ILogger<HomeController> logger, URC_Context context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
             //Visitor counter++
            var con = HttpContext.Session;
            if (con.Get("Date") == null)
            {
                con.SetString("Date", DateTime.Now.ToString("mm"));
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
                    con.SetString("Date", DateTime.Now.ToString("HH"));
                    var visiter_info = await _context.GeneralInfos.FirstOrDefaultAsync(o => o.ID == 1);
                    visiter_info.number++;
                    _context.Update(visiter_info);
                    await _context.SaveChangesAsync();
                }
            }
            ///////////////////////////////////////




            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Handmade_Opportunities()
        {
            return View();
        }

        public IActionResult Handmade_Details()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        
      
        [Route("robots.txt")]
        public FileContentResult Robots()
        {
            StringBuilder robotsEntries = new StringBuilder();
            robotsEntries.AppendLine("User-agent: *");


            robotsEntries.AppendLine("Disallow: /Dashboard");
            robotsEntries.AppendLine("Disallow: /Privacy");
            robotsEntries.AppendLine("Crawl-deslay: 60");

            //    robotsEntries.AppendLine("Sitemap: http://www.surinderbhomra.com/sitemap.xml");


            return File(Encoding.UTF8.GetBytes(robotsEntries.ToString()), "text/plain");
        }


        








    }
}
