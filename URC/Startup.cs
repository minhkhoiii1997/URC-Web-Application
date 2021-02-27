/**
 * Author:    Kevin Tran
 * Partner:   Calvin Tu
 * Date:      August 2020
 * Course:    CS 4540, University of Utah, School of Computing
 * Copyright: CS 4540 and Kevin Tran and Calvin Tu - This work may not be copied for use in Academic Coursework.
 *
 * We, Kevin Tran, Calvin Tu certify that we wrote this code from scratch and did 
 * not copy it in part or whole from another source.  Any references used 
 * in the completion of the assignment are cited in my README file and in
 * the appropriate method header.
 *
 * File Contents
 * Program startup; configures all services and dependency injection
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using URC.Data;
using Microsoft.AspNetCore.Identity;
using URC.Areas.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using SignalRChat.Hubs;

namespace URC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mvc = services.AddControllersWithViews();

            // prepare for dependency injection
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddDbContext<URC_Context>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("URC_Context")));

            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Thanks to https://stackoverflow.com/questions/57727595/send-requestverificationtoken-with-fetch-api-and-recieve-with-an-validateanti
            services.AddAntiforgery(x =>
            {
                x.HeaderName = "X-CSRF-TOKEN-URC";
            });

            services.AddSignalR();
            services.AddRazorPages()
                    .AddRazorRuntimeCompilation();

            #if (DEBUG)
                mvc.AddRazorRuntimeCompilation();
            #endif

            // add session function
            services.AddDistributedMemoryCache();
            services.AddSession();
            /////////////
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // add session function
            app.UseSession();

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<ChatHub>("/chathub");

                endpoints.MapRazorPages();
            });
        }
    }
}
