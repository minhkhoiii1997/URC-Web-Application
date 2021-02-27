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
 * Object model for a student
 * "Extends" ApplicationUser with student-specific data
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URC.Models
{
    public class Student
    {
        public string StudentId { get; set; }

        /// <summary>
        /// The courses the student has completed.
        /// </summary>
        public virtual ICollection<StudentCourse> Courses { get; set; }

        /// <summary>
        /// Any research/general interests the student has.
        /// </summary>
        public virtual ICollection<StudentInterest> Interests { get; set; }

        /// <summary>
        /// Any specific skills the student has.
        /// </summary>
        public virtual ICollection<StudentSkill> Skills { get; set; }
    }
}
