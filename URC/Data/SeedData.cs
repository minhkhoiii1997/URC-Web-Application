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
 * Data for database seeding
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URC.Models;

namespace URC.Data
{
    public class SeedData
    {
        // ========== Simple models ==========

        public static readonly Course[] Courses = new Course[]
        {
            new Course { CourseId = 1, Name = "CS2420" },
            new Course { CourseId = 2, Name = "CS3500" },
            new Course { CourseId = 3, Name = "CS3505" },
            new Course { CourseId = 4, Name = "MATH1220" },
            new Course { CourseId = 5, Name = "MATH3210" },
            new Course { CourseId = 6, Name = "ECE3530" },
            new Course { CourseId = 7, Name = "CS4150" },
            new Course { CourseId = 8, Name = "CS4540" }
        };

        public static readonly Interest[] Interests = new Interest[]
        {
            new Interest { InterestId = 1, Name="Data Visualization" },
            new Interest { InterestId = 2, Name="Big Data" },
            new Interest { InterestId = 3, Name="AI" },
            new Interest { InterestId = 4, Name="Machine Learning" },
            new Interest { InterestId = 5, Name="Algorithms" },
            new Interest { InterestId = 6, Name="Medical" },
            new Interest { InterestId = 7, Name="Writing" },
        };

        public static readonly Skill[] Skills = new Skill[] {
            new Skill{Name="C#", SkillId=1},
            new Skill{Name="Haskell", SkillId=2},
            new Skill{Name="C", SkillId=3},
            new Skill{Name="C++", SkillId=4},
            new Skill{Name="Git", SkillId=5},
            new Skill{Name="Linux", SkillId=6},
            new Skill{Name="Java", SkillId=7},
            new Skill{Name="JavaScript", SkillId=8},
            new Skill{Name="HTML/CSS", SkillId=9},
            new Skill{Name="Agile", SkillId=10},
            new Skill{Name="Waterfall", SkillId=11},
            new Skill{Name="Rust", SkillId=12},
            new Skill{Name="Writing", SkillId=13},
            new Skill{Name="Medical knowledge", SkillId=14},
            new Skill{Name="AWS", SkillId=15}
        };

        public static readonly SearchTag[] SearchTags = new SearchTag[]
        {
            new SearchTag{Name="On site", SearchTagId=1},
            new SearchTag{Name="Remote work", SearchTagId=2},
            new SearchTag{Name="Paid", SearchTagId=3},
            new SearchTag{Name="Unpaid", SearchTagId=4},
            new SearchTag{Name="Graduate", SearchTagId=5},
            new SearchTag{Name="Undergraduate", SearchTagId=6},
            new SearchTag{Name="Computer science", SearchTagId=7},
            new SearchTag{Name="Medical", SearchTagId=8},
            new SearchTag{Name="Industry sponsors", SearchTagId=9},
            new SearchTag{Name="Data visualization", SearchTagId=10},
            new SearchTag{Name="Cloud computing", SearchTagId=11}
        };

        // ========== User extension models ========== 
        // Extends ApplicationUsers with role-specific data
        // MUST ensure corresponding IDs exist in UsersRolesDB manually
        // because this data is stored in a different database, URC_Context!
        // See SeedUsersRolesDB

        public static readonly Student[] Students = new Student[]
        {
            new Student { StudentId = "8" }, // username 's'
            new Student { StudentId = "9" }, // username 'u0000001'
        };

        public static readonly Professor[] Professors = new Professor[]
        {
            new Professor { ProfessorId = "2", Name = "Prof. Test" }, // username 'professor'
            new Professor { ProfessorId = "3", Name = "Mary" }, // username 'professormary'
            new Professor { ProfessorId = "7", Name = "p" }, // username 'p'
        };

        // ========== Complex models ==========

        public static readonly Opportunity[] Opportunities = new Opportunity[]
        {
            new Opportunity{
                OpportunityId=1,
                Professor=Professors[0],
                Name="URC",
                Description="A web application built to connect students to faculty to make finding research work frictionless. Professors can post available research opportunities along with any required skills, pictures, related tags, etc. Students can quickly find and sort through these opportunities and find the best one they can apply their skills to. There will also be an admin section to allow admins to visualize URC usage as well as other monitoring/maintenance tasks.",
                RoleDescription="Gathers, organizes and analyzes data through interviews, literature searches, medical record reviews and related non-lab methods for use in studies, publications and other research related uses.",
                Responsibilities="Develop code, Document code",
                Mentor="Nathan Davis",
                PostedDate=new DateTime(2020, 09, 23),
                Deadline=new DateTime(2020, 10, 7),
                Pay=0.0,
                IsFilled=false
            },
            new Opportunity{
                OpportunityId=2,
                Professor=Professors[0],
                Name="Medical Research",
                Description="Granger Medical Clinic is one of the largest independent, physician-owned medical clinic groups in Utah. Care is our mission. Its our shared vision. Our Family Medicine clinic in West Valley has a Patient Service representative (medical receptionist) opening. We are looking for a friendly, pleasant, professional Patient Service Representative to join our team. Our Patient Service Representatives assist in the front desk as medical receptionist with the scheduling of appointments, checking patients in/out, etc. At Granger Medical Clinic, exceptional care comes from all of us; and we are in need of a patient service representative to join us in our mission of providing excellent patient care.\nAre you looking for an opportunity to continue on progressing in the medical field? If so, this might be what you have been looking for. Your experience in the medical field as a medical receptionist, front office, or patient service rep will be a great asset to our Family Medicine Clinic.",
                RoleDescription="Medical intern",
                Responsibilities="Help patients, Sterilize equipment, do medical things",
                Mentor="Nathan Davis",
                PostedDate=new DateTime(2020, 09, 23),
                Deadline=new DateTime(2020, 10, 7),
                Pay=0.0,
                IsFilled=false
            },
            new Opportunity{
                OpportunityId=3,
                Professor=Professors[1],
                Name="Cloud Computing at Scale",
                Description="Amazon Web Services (AWS) is the world’s most comprehensive and broadly adopted cloud platform, offering over 175 fully featured services from data centers globally. Millions of customers—including the fastest-growing startups, largest enterprises, and leading government agencies—are using AWS to lower costs, become more agile, and innovate faster.",
                RoleDescription="C# Developer",
                Responsibilities="Develop code, Document code, Work on cloud computing, manage cloud servers",
                Mentor="Nathan Davis",
                PostedDate=new DateTime(2020, 09, 23),
                Deadline=new DateTime(2020, 10, 7),
                Pay=500.0,
                IsFilled=false
            },
            new Opportunity{
                OpportunityId=4,
                Professor=Professors[2],
                Name="Flavor Text Writer",
                Description="Write up various flavor text placeholder things that look neat and in-place at a glance, but upon further inspection, are witty and clever. Flavor text should not be lorem ipsum and may be mildly amusing.",
                RoleDescription="Writer",
                Responsibilities="Write up text",
                Mentor="Nathan Davis",
                PostedDate=new DateTime(2020, 09, 23),
                Deadline=new DateTime(2020, 10, 7),
                Pay=1000.0,
                IsFilled=false
            },
            new Opportunity{
                OpportunityId=5,
                Professor=Professors[2],
                Name="Fifth Opportunity",
                Description="This is the fifth opportunity with some placeholder text.",
                RoleDescription="Take up space and at a glance appear like a full opportunity but upon further inspection it is actually just a placeholder with no other details.",
                Responsibilities="Take up space, look like a real research opportunity at a glance",
                Mentor="Nathan Davis",
                PostedDate=new DateTime(2020, 09, 23),
                Deadline=new DateTime(2020, 10, 7),
                Pay=8000.0,
                IsFilled=false
            }
        };

        public static readonly StudentApplication[] StudentApplications = new StudentApplication[]
        {
            new StudentApplication
            {
                Student = Students[1],
                OpportunityId = Opportunities[0].OpportunityId,
                ResumeFilename = "",
                ExpectedGraduation = new DateTime(2021, 5, 10),
                DegreePlan = "CS",
                GPA = 3.56,
                UId = "u0000001",
                Availability = 20,
                PersonalStatement = "I am user 1.",
                IsLookingForPosition = true,
                ApplicationDate = DateTime.Now,
                TimeModified = DateTime.Now
            },
            new StudentApplication
            {
                Student = Students[0],
                OpportunityId = Opportunities[0].OpportunityId,
                ResumeFilename = "",
                ExpectedGraduation = new DateTime(2022, 5, 10),
                DegreePlan = "ECE",
                GPA = 3.93,
                UId = "u0000002",
                Availability = 19,
                PersonalStatement = "I am user 2.",
                IsLookingForPosition = true,
                ApplicationDate = DateTime.Now,
                TimeModified = DateTime.Now
            }
        };

        // ========== Mappings ==========

        public static readonly OpportunitySearchTag[] OpportunitySearchTagMapping = new OpportunitySearchTag[]
        {
            new OpportunitySearchTag {OpportunityId=1, SearchTagId=1},
            new OpportunitySearchTag {OpportunityId=1, SearchTagId=4},
            new OpportunitySearchTag {OpportunityId=1, SearchTagId=6},
            new OpportunitySearchTag {OpportunityId=1, SearchTagId=7},
            new OpportunitySearchTag {OpportunityId=2, SearchTagId=1},
            new OpportunitySearchTag {OpportunityId=2, SearchTagId=3},
            new OpportunitySearchTag {OpportunityId=2, SearchTagId=8},
            new OpportunitySearchTag {OpportunityId=3, SearchTagId=2},
            new OpportunitySearchTag {OpportunityId=3, SearchTagId=3},
            new OpportunitySearchTag {OpportunityId=3, SearchTagId=7},
            new OpportunitySearchTag {OpportunityId=3, SearchTagId=9},
            new OpportunitySearchTag {OpportunityId=3, SearchTagId=11},
            new OpportunitySearchTag {OpportunityId=5, SearchTagId=2},
            new OpportunitySearchTag {OpportunityId=5, SearchTagId=4},
            new OpportunitySearchTag {OpportunityId=5, SearchTagId=6}
        };

        public static readonly OpportunityRequiredSkill[] OpportunityRequiredSkillMapping = new OpportunityRequiredSkill[]
        {
            new OpportunityRequiredSkill {OpportunityId=1, SkillId=1},
            new OpportunityRequiredSkill {OpportunityId=1, SkillId=5},
            new OpportunityRequiredSkill {OpportunityId=2, SkillId=14},
            new OpportunityRequiredSkill {OpportunityId=3, SkillId=1},
            new OpportunityRequiredSkill {OpportunityId=3, SkillId=5},
            new OpportunityRequiredSkill {OpportunityId=3, SkillId=6},
            new OpportunityRequiredSkill {OpportunityId=3, SkillId=15},
            new OpportunityRequiredSkill {OpportunityId=4, SkillId=13},
            new OpportunityRequiredSkill {OpportunityId=5, SkillId=1},
            new OpportunityRequiredSkill {OpportunityId=5, SkillId=3},
            new OpportunityRequiredSkill {OpportunityId=5, SkillId=5},
            new OpportunityRequiredSkill {OpportunityId=5, SkillId=7},
            new OpportunityRequiredSkill {OpportunityId=5, SkillId=9},
            new OpportunityRequiredSkill {OpportunityId=5, SkillId=11},
        };

        public static readonly OpportunityPreferredSkill[] OpportunityPreferredSkillMapping = new OpportunityPreferredSkill[]
        {
            new OpportunityPreferredSkill {OpportunityId=1, SkillId=10},
            new OpportunityPreferredSkill {OpportunityId=1, SkillId=6},
            new OpportunityPreferredSkill {OpportunityId=3, SkillId=10},
            new OpportunityPreferredSkill {OpportunityId=3, SkillId=11},
            new OpportunityPreferredSkill {OpportunityId=4, SkillId=1},
            new OpportunityPreferredSkill {OpportunityId=4, SkillId=3},
            new OpportunityPreferredSkill {OpportunityId=4, SkillId=5},
            new OpportunityPreferredSkill {OpportunityId=4, SkillId=6},
            new OpportunityPreferredSkill {OpportunityId=4, SkillId=7},
            new OpportunityPreferredSkill {OpportunityId=4, SkillId=10},
            new OpportunityPreferredSkill {OpportunityId=5, SkillId=2},
            new OpportunityPreferredSkill {OpportunityId=5, SkillId=4},
            new OpportunityPreferredSkill {OpportunityId=5, SkillId=6},
            new OpportunityPreferredSkill {OpportunityId=5, SkillId=8},
            new OpportunityPreferredSkill {OpportunityId=5, SkillId=10},
            new OpportunityPreferredSkill {OpportunityId=5, SkillId=12}
        };

        public static readonly OpportunityCounter[] OpportunityCounterMapping = new OpportunityCounter[]
        {
            new OpportunityCounter{ OpportunityId=Opportunities.Single(i=> i.Name =="URC").OpportunityId,                      counter=91 },
            new OpportunityCounter{ OpportunityId=Opportunities.Single(i=> i.Name =="Medical Research").OpportunityId,         counter=29 },
            new OpportunityCounter{ OpportunityId=Opportunities.Single(i=> i.Name =="Cloud Computing at Scale").OpportunityId, counter=30 },
            new OpportunityCounter{ OpportunityId=Opportunities.Single(i=> i.Name =="Flavor Text Writer").OpportunityId,       counter=64 },
            new OpportunityCounter{ OpportunityId=Opportunities.Single(i=> i.Name =="Fifth Opportunity").OpportunityId,        counter=85 }
        };

        public static readonly GeneralInfo[] GeneralInfo = new GeneralInfo[]
        {
            new GeneralInfo{ ID=1, name="visitor", number=15 },
            new GeneralInfo{ ID=2, name="online_members", number=0 }
        };

        public static readonly StudentCourse[] StudentCourseMapping = new StudentCourse[]
        {
                new StudentCourse { StudentId = "8", CourseId = 1 },
                new StudentCourse { StudentId = "8", CourseId = 2 },
                new StudentCourse { StudentId = "8", CourseId = 3 },
                new StudentCourse { StudentId = "8", CourseId = 4 },
                new StudentCourse { StudentId = "8", CourseId = 5 },
                new StudentCourse { StudentId = "8", CourseId = 6 },
                new StudentCourse { StudentId = "8", CourseId = 7 },
                new StudentCourse { StudentId = "9", CourseId = 1 },
                new StudentCourse { StudentId = "9", CourseId = 2 },
                new StudentCourse { StudentId = "9", CourseId = 3 },
                new StudentCourse { StudentId = "9", CourseId = 7 },
                new StudentCourse { StudentId = "9", CourseId = 8 }
        };

        public static readonly StudentInterest[] StudentInterestMapping = new StudentInterest[]
        {
            new StudentInterest { StudentId = "8", InterestId = 1 },
            new StudentInterest { StudentId = "8", InterestId = 2 },
            new StudentInterest { StudentId = "8", InterestId = 3 },
            new StudentInterest { StudentId = "8", InterestId = 4 },
            new StudentInterest { StudentId = "9", InterestId = 2 },
            new StudentInterest { StudentId = "9", InterestId = 4 },
            new StudentInterest { StudentId = "9", InterestId = 7 }
        };

        public static readonly StudentSkill[] StudentSkillMapping = new StudentSkill[]
        {
            new StudentSkill { StudentId = Students[0].StudentId, SkillId = Skills[0].SkillId },
            new StudentSkill { StudentId = Students[0].StudentId, SkillId = Skills[4].SkillId },
            new StudentSkill { StudentId = Students[0].StudentId, SkillId = Skills[5].SkillId },
            new StudentSkill { StudentId = Students[0].StudentId, SkillId = Skills[7].SkillId },
            new StudentSkill { StudentId = Students[1].StudentId, SkillId = Skills[0].SkillId },
            new StudentSkill { StudentId = Students[1].StudentId, SkillId = Skills[2].SkillId },
            new StudentSkill { StudentId = Students[1].StudentId, SkillId = Skills[5].SkillId }
        };
    }
}
