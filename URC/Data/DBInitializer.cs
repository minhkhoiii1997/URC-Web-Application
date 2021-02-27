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
 * Code to seed database with default opportunities
 */

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URC.Areas.Identity.Data;
using URC.Models;

namespace URC.Data
{
    public class DBInitializer
    {
        public static void Initialize(URC_Context context)
        {
            context.Database.EnsureCreated();

            // Early return if there are any opportunities already
            if (context.Opportunities.Any())
            {
                return;
            }

            // Thanks https://stackoverflow.com/questions/13086006/how-can-i-force-entity-framework-to-insert-identity-columns
            // No thanks to EF not recognizing the SET IDENTITY_INSERT except when using raw SQL
            // There are likely more advanced ways to seed the database that are less brittle

            // Seed courses
            var transaction = context.Database.BeginTransaction();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Courses] ON;");
            foreach (var course in SeedData.Courses)
            {
                context.Database.ExecuteSqlInterpolated($"INSERT INTO Courses (CourseId, Name) VALUES ({course.CourseId}, {course.Name});");
            }
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Courses] OFF;");
            context.SaveChanges();
            transaction.Commit();

            // Seed interests
            transaction = context.Database.BeginTransaction();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Interests] ON;");
            foreach (var interest in SeedData.Interests)
            {
                context.Database.ExecuteSqlInterpolated($"INSERT INTO Interests (InterestId, Name) VALUES ({interest.InterestId}, {interest.Name});");
            }
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Interests] OFF;");
            context.SaveChanges();
            transaction.Commit();

            // Seed skills
            transaction = context.Database.BeginTransaction();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Skills] ON;");
            foreach (var skill in SeedData.Skills)
            {
                context.Database.ExecuteSqlInterpolated($"INSERT INTO Skills (SkillId, Name) VALUES ({skill.SkillId}, {skill.Name});");
            }
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Skills] OFF;");
            context.SaveChanges();
            transaction.Commit();

            // Seed opportunity search tags
            transaction = context.Database.BeginTransaction();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[SearchTags] ON;");
            foreach (var tag in SeedData.SearchTags)
            {
                context.Database.ExecuteSqlInterpolated($"INSERT INTO SearchTags (SearchTagId, Name) VALUES ({tag.SearchTagId}, {tag.Name});");
            }
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[SearchTags] OFF;");
            context.SaveChanges();
            transaction.Commit();

            // Seed extended user models
            // which do not require IDENTITY_INSERT for some unknown reason
            context.Students.AddRange(SeedData.Students);
            context.Professors.AddRange(SeedData.Professors);
            context.SaveChanges();

            // Seed opportunities
            transaction = context.Database.BeginTransaction();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Opportunities] ON;");
            foreach (var opp in SeedData.Opportunities)
            {
                // Thanks https://stackoverflow.com/questions/31764898/long-string-interpolation-lines-in-c6
                // datetime2 format https://docs.microsoft.com/en-us/sql/t-sql/data-types/datetime2-transact-sql?view=sql-server-ver15
                // format date https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings
                context.Database.ExecuteSqlInterpolated(
                    $@"INSERT INTO Opportunities (OpportunityId, ProfessorId, Name, Description, RoleDescription, Responsibilities, Mentor, PostedDate, Deadline, Pay, IsFilled) VALUES ({opp.OpportunityId}, {opp.Professor.ProfessorId}, {opp.Name}, {opp.Description}, {opp.RoleDescription}, {opp.Responsibilities}, {opp.Mentor}, {opp.PostedDate.ToShortDateString()}, {opp.Deadline.ToShortDateString()}, {opp.Pay}, {opp.IsFilled});"
                );
            }
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Opportunities] OFF;");
            context.SaveChanges();
            transaction.Commit();

            // Mappings
            context.OpportunitySearchTags.AddRange(SeedData.OpportunitySearchTagMapping);
            context.SaveChanges();

            context.OpportunityRequiredSkills.AddRange(SeedData.OpportunityRequiredSkillMapping);
            context.SaveChanges();

            context.OpportunityPreferredSkills.AddRange(SeedData.OpportunityPreferredSkillMapping);
            context.SaveChanges();


            // seeding for counter by Ping
            context.OpportunityCounters.AddRange(SeedData.OpportunityCounterMapping);
            context.SaveChanges();


            // seeding for visitor counter  by Ping
            context.GeneralInfos.AddRange(SeedData.GeneralInfo);
            context.SaveChanges();

        } // End of Initialize()

        public static void AddStudentApplications(URC_Context context, UserManager<ApplicationUser> userManager)
        {
            if (!context.Opportunities.Any() || !userManager.Users.Any())
            {
                throw new Exception("Must seed database opportunities and users before seeding student applications.");
            }

            // Early return if there are already applications
            if (context.StudentApplications.Any())
            {
                return;
            }

            // Don't need IDENTITY_INSERT because applications aren't needed for later many-to-many mappings
            context.StudentApplications.AddRange(SeedData.StudentApplications);
            context.SaveChanges();

            context.StudentCourses.AddRange(SeedData.StudentCourseMapping);
            context.SaveChanges();

            context.StudentInterests.AddRange(SeedData.StudentInterestMapping);
            context.SaveChanges();

            context.StudentSkills.AddRange(SeedData.StudentSkillMapping);
            context.SaveChanges();
        }
    }
}
