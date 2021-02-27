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
 * Code to seed Identity database with default users and roles.
 */

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using URC.Data;

namespace URC.Areas.Identity.Data
{
    public class SeedUsersRolesDB
    {
        async public static Task Initialize(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> rolesManager, UsersRolesDB context)
        {
            context.Database.EnsureCreated();

            if(rolesManager.Roles.ToArray().Count() > 0)
            {
                // Database already has roles, skip seeding
                return;
            }

            var users = new ApplicationUser[]
            {
                // PS5 required
                new ApplicationUser {
                    Id = "1",
                    UserName = "admin",
                    Name = "Admin",
                    Email = "admin@utah.edu",
                    EmailConfirmed = true
                },
                new ApplicationUser {
                    Id = "2",
                    UserName = "professor",
                    Name = "Professor",
                    Email = "professor@utah.edu",
                    EmailConfirmed = true
                },
                new ApplicationUser {
                    Id = "3",
                    UserName = "professormary",
                    Name = "Professor Mary",
                    Email = "professor_mary@utah.edu",
                    EmailConfirmed = true
                },
                new ApplicationUser {
                    Id = "4",
                    UserName = "u0000000",
                    Name = "User Zero",
                    Email = "u0000000@utah.edu",
                    EmailConfirmed = true
                },
                new ApplicationUser {
                    Id = "5",
                    UserName = "u1234567",
                    Name = "Natural Numbers",
                    Email = "u1234567@utah.edu",
                    EmailConfirmed = true
                },
                // Quick test accounts
                new ApplicationUser {
                    Id = "6",
                    UserName = "a",
                    Name = "a",
                    Email = "a@a.a",
                    EmailConfirmed = true
                },
                new ApplicationUser {
                    Id = "7",
                    UserName = "p",
                    Name = "p",
                    Email = "p@p.p",
                    EmailConfirmed = true
                },
                new ApplicationUser {
                    Id = "8",
                    UserName = "s",
                    Name = "s",
                    Email = "s@s.s",
                    EmailConfirmed = true
                },
                // PS6 required
                new ApplicationUser {
                    Id = "9",
                    UserName = "u0000001",
                    Name = "User One",
                    Email = "u0000001@utah.edu",
                    EmailConfirmed = true
                },
                new ApplicationUser {
                    Id = "10",
                    UserName = "u0000002",
                    Name = "User Two",
                    Email = "u0000002@utah.edu",
                    EmailConfirmed = true
                },
                new ApplicationUser {
                    Id = "11",
                    UserName = "u0000003",
                    Name = "User Three",
                    Email = "u0000003@utah.edu",
                    EmailConfirmed = true
                }
            };

            foreach(var user in users)
            {
                if(user.UserName.Length > 1)
                {
                    await userManager.CreateAsync(user, "123ABC!@#def");
                } else
                {
                    await userManager.CreateAsync(user, "abcdef"); // Shorter password for test accounts
                }
            }

            var roles = new IdentityRole[]
            {
                new IdentityRole
                { 
                    Id = "1",
                    Name = "Admin"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "Professor"
                },
                new IdentityRole
                {
                    Id = "3",
                    Name = "Student"
                },
            };

            foreach(var role in roles)
            {
                await rolesManager.CreateAsync(role);
            }

            var userRoles = new UserRoleMapping[]
            {
                // PS5 users
                new UserRoleMapping
                {
                    UserName = "admin",
                    Role = "Admin"
                },
                new UserRoleMapping
                {
                    UserName = "professor",
                    Role = "Professor"
                },
                new UserRoleMapping
                {
                    UserName = "professormary",
                    Role = "Professor"
                },
                new UserRoleMapping
                {
                    UserName = "u0000000",
                    Role = "Student"
                },
                new UserRoleMapping
                {
                    UserName = "u1234567",
                    Role = "Student"
                },
                // Quick test users
                new UserRoleMapping
                {
                    UserName = "a",
                    Role = "Admin"
                },
                new UserRoleMapping
                {
                    UserName = "p",
                    Role = "Professor"
                },
                new UserRoleMapping
                {
                    UserName = "s",
                    Role = "Student"
                },
                // PS6 Users
                new UserRoleMapping
                {
                    UserName = "u0000001",
                    Role = "Student"
                },
                new UserRoleMapping
                {
                    UserName = "u0000002",
                    Role = "Student"
                },
                new UserRoleMapping
                {
                    UserName = "u0000003",
                    Role = "Student"
                }
            };

            foreach(var ur in userRoles)
            {
                var user = await userManager.FindByNameAsync(ur.UserName);
                await userManager.AddToRoleAsync(user, ur.Role);
            }
        }
        struct UserRoleMapping
        {
            public string UserName { get; set; }
            public string Role { get; set; }
        }
    }
}
