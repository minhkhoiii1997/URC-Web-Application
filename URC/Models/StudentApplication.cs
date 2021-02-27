/**
 * Author:    Kevin Tran
 * Partner:   Calvin Tu
 * Date:      10/20/2020
 * Course:    CS 4540, University of Utah, School of Computing
 * Copyright: CS 4540 and Kevin Tran and Calvin Tu - This work may not be copied for use in Academic Coursework.
 *
 * We, Kevin Tran, Calvin Tu certify that we wrote this code from scratch and did 
 * not copy it in part or whole from another source.  Any references used 
 * in the completion of the assignment are cited in my README file and in
 * the appropriate method header.
 *
 * File Contents
 * Object model representing a Student application
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace URC.Models
{
    public class StudentApplication
    {
        /// <summary>
        /// Foreign key of the student who owns this application.
        /// </summary>
        public Student Student { get; set; }

        /// <summary>
        /// The primary key; id of this application.
        /// </summary>
        public int StudentApplicationId { get; set; }

        /// <summary>
        /// The opportunity this application is for
        /// </summary>
        public Opportunity Opportunity { get; set; }

        /// <summary>
        /// Equivalent to this.Opportunity.OpportunityId
        /// </summary>
        public int OpportunityId { get; set; }

        /// <summary>
        /// Path to any resume uploaded to the server.
        /// </summary>
        public string ResumeFilename { get; set; }

        /// <summary>
        /// Expected graduation date.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime ExpectedGraduation { get; set; }

        /// <summary>
        /// Student's degree plan (e.g. CS, ECE, etc).
        /// </summary>
        [MaxLength(10)]
        public string DegreePlan { get; set; }

        /// <summary>
        /// GPA of the student.
        /// </summary>
        [Required]
        [Range(0,4)]
        public double GPA { get; set; }

        /// <summary>
        /// uID of the student.
        /// </summary>
        [RegularExpression(@"^u[0-9]{7}$", ErrorMessage = "Please use the format: u1234567")]
        public string UId { get; set; }

        /// <summary>
        /// Hours the student is available.
        /// </summary>
        [Range(0,168, ErrorMessage = "You must be available for 0 - 168 hours per week.")] // No negative availability and cannot surpass the number of hours in a week.
        public int Availability { get; set; }

        /// <summary>
        /// Student's personal statement.
        /// </summary>
        [MaxLength(1000, ErrorMessage = "Please limit your Statement to 1000 characters"), MinLength(10)]
        public string PersonalStatement { get; set; }

        /// <summary>
        /// If the student is currently looking for a research position.
        /// </summary>
        public bool IsLookingForPosition { get; set; }

        // Below are auto-generated columns

        /// <summary>
        /// The date this application was submitted (i.e. created)
        /// </summary>
        [ScaffoldColumn(false)]
        public DateTime ApplicationDate { get; set; }

        /// <summary>
        /// The last time this application was modified
        /// </summary>
        [ScaffoldColumn(false)]
        public DateTime TimeModified { get; set; }
    }
}
