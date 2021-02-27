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
 * Object model representing a student interest (e.g. visualization, chess, ultimate frisbee, etc)
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace URC.Models
{
    public class Interest
    {
        public int InterestId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public StudentApplication Application { get; set; }

        public virtual ICollection<StudentInterest> StudentInterests { get; set; }
    }
}
