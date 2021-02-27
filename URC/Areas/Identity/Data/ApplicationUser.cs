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
 * Object model for a URC user
 */
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace URC.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        // Thanks https://docs.microsoft.com/en-us/aspnet/core/security/authentication/add-user-data?view=aspnetcore-3.1&tabs=visual-studio#add-custom-user-data-to-the-identity-db
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

        [ProtectedPersonalData]
        [EnumDataType(typeof(Models.Departments))]
        public string Department { get; set; }

        [PersonalData]
        [MaxLength(1000)]
        public string Description { get; set; }

        [PersonalData]
        [RegularExpression(@"^[Uu]\d{7}$", ErrorMessage = "uID must be in the form uXXXXXXX where X is any number")]
        [Display(Name = "uID")]
        public string UId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Username")]
        [PersonalData]
        public override string UserName { get; set; }
    }
}
