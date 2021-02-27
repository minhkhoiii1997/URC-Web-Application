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
 * Database context for URC application data
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using URC.Models;

namespace URC.Data
{
    public class URC_Context : DbContext
    {
        public URC_Context(DbContextOptions<URC_Context> options)
            : base(options)
        {
        }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<SearchTag> SearchTags { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<StudentApplication> StudentApplications { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<ChatMessage> Messages { get; set; }
        public DbSet<ChatRoom> Rooms { get; set; }

        // Join tables
        public DbSet<OpportunitySearchTag> OpportunitySearchTags { get; set; }
        public DbSet<OpportunityRequiredSkill> OpportunityRequiredSkills { get; set; }
        public DbSet<OpportunityPreferredSkill> OpportunityPreferredSkills { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentInterest> StudentInterests { get; set; }
        public DbSet<StudentSkill> StudentSkills { get; set; }


        // dashborad dat
        public DbSet<OpportunityCounter> OpportunityCounters { get; set; }
        public DbSet<GeneralInfo> GeneralInfos { get; set; }
       



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Create normal tables
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Interest>().ToTable("Interests");
            modelBuilder.Entity<Skill>().ToTable("Skills");
            modelBuilder.Entity<SearchTag>().ToTable("SearchTags");
            modelBuilder.Entity<Opportunity>().ToTable("Opportunities");
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Professor>().ToTable("Professors");
            modelBuilder.Entity<ChatMessage>().ToTable("ChatMessage");
            modelBuilder.Entity<ChatRoom>().ToTable("ChatRoom");

            // Create join tables
            modelBuilder.Entity<OpportunitySearchTag>().ToTable("Opportunities_SearchTags");
            modelBuilder.Entity<OpportunityRequiredSkill>().ToTable("Opportunities_RequiredSkills");
            modelBuilder.Entity<OpportunityPreferredSkill>().ToTable("Opportunities_PreferredSkills");
            modelBuilder.Entity<StudentCourse>().ToTable("Student_Courses");
            modelBuilder.Entity<StudentInterest>().ToTable("Student_Interests");
            modelBuilder.Entity<StudentSkill>().ToTable("Student_Skills");
            modelBuilder.Entity<OpportunityCounter>().ToTable("OpportunityCounters");
            modelBuilder.Entity<GeneralInfo>().ToTable("GeneralInfos");

            // Thanks to https://stackoverflow.com/questions/51993903/how-to-specify-a-composite-primary-key-using-efcore-code-first-migrations
            // For how to set up composite keys
            modelBuilder.Entity<OpportunitySearchTag>()
                .HasKey(ost => new { ost.OpportunityId, ost.SearchTagId });

            modelBuilder.Entity<OpportunityRequiredSkill>()
                .HasKey(ors => new { ors.OpportunityId, ors.SkillId });

            modelBuilder.Entity<OpportunityPreferredSkill>()
                .HasKey(ops => new { ops.OpportunityId, ops.SkillId });

            modelBuilder.Entity<StudentCourse>()
                .HasKey(sac => new { sac.StudentId, sac.CourseId });

            modelBuilder.Entity<StudentInterest>()
                .HasKey(sai => new { sai.StudentId, sai.InterestId });

            modelBuilder.Entity<StudentSkill>()
                .HasKey(sas => new { sas.StudentId, sas.SkillId });

            //// Thanks to https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#required-and-optional-relationships
            //modelBuilder.Entity<Course>()
            //    .HasMany(course => course.Application)
            //    .WithMany(app => app.CompletedCourses)
            //    .OnDelete(DeleteBehavior.SetNull);

            //modelBuilder.Entity<Interest>()
            //    .HasOne(interest => interest.Application)
            //    .WithMany(app => app.Interests)
            //    .OnDelete(DeleteBehavior.SetNull);

            //modelBuilder.Entity<Skill>()
            //    .HasOne(skill => skill.Application)
            //    .WithMany(app => app.Skills)
            //    .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<StudentApplication>()
                .Property(app => app.ApplicationDate)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<StudentApplication>()
                .Property(app => app.TimeModified)
                .ValueGeneratedOnAdd();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var item in ChangeTracker.Entries<StudentApplication>().Where(e => e.State == EntityState.Added))
            {
                item.Property("ApplicationDate").CurrentValue = DateTime.UtcNow;
            }

            foreach(var item in ChangeTracker.Entries<StudentApplication>().Where(e => e.State == EntityState.Modified || e.State == EntityState.Modified))
            {
                item.Property("TimeModified").CurrentValue = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

