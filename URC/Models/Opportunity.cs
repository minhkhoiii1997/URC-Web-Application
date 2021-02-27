/**
 * Author:    Kevin Tran
 * Partner:   Calvin Tu
 * Date:      9/23/2020
 * Course:    CS 4540, University of Utah, School of Computing
 * Copyright: CS 4540 and Kevin Tran and Calvin Tu - This work may not be copied for use in Academic Coursework.
 *
 * We, Kevin Tran, Calvin Tu certify that we wrote this code from scratch and did 
 * not copy it in part or whole from another source.  Any references used 
 * in the completion of the assignment are cited in my README file and in
 * the appropriate method header.
 *
 * File Contents
 * Object model for a research opportunity
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace URC.Models
{
    public class Opportunity
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int OpportunityId { get; set; }

        /// <summary>
        /// The professor presiding over this opportunity
        /// </summary>
        public Professor Professor { get; set; }

        /// <summary>
        /// Equivalent to this.Professor.ProfessorId
        /// </summary>
        public string ProfessorId { get; set; }

        /// <summary>
        /// Research project name
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// Short description of the research opportunity
        /// </summary>
        [Required]
        [MaxLength(5000)]
        public string Description { get; set; }

        /// <summary>
        /// Short description of the role of the student
        /// </summary>
        [Required]
        [MaxLength(5000)]
        [Display(Name = "Role Description")]
        public string RoleDescription { get; set; }

        /// <summary>
        /// Similar to role description, but in list form
        /// It is more skimmable than the role description
        /// </summary>
        [MaxLength(3000)]
        public string Responsibilities { get; set; }

        /// <summary>
        /// Associated image filename
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Student mentor, e.g., professor/TA/RA/supervisor
        /// </summary>
        [MaxLength(50)]
        public string Mentor { get; set; }

        // https://stackoverflow.com/questions/5658216/entity-framework-code-first-date-field-creation
        /// <summary>
        /// "Beginning Date" - the date this opportunity was posted
        /// </summary>
        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        public DateTime PostedDate { get; set; }

        /// <summary>
        /// "End Date" - the application deadline for this opportunity
        /// </summary>
        [Required]
        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        /// <summary>
        /// Any pay for research
        /// </summary>
        [Required]
        public double Pay { get; set; }

        /// <summary>
        /// If this opportunity has been filled (no more space) but not yet delisted (still visible on URC)
        /// </summary>
        public bool IsFilled { get; set; }

        /// <summary>
        /// Applications to this opportunity
        /// </summary>
        public virtual ICollection<StudentApplication> Applications { get; set; }

        /// <summary>
        /// Required skills for applicants
        /// </summary>
        public virtual ICollection<OpportunityRequiredSkill> RequiredSkills { get; set; }

        /// <summary>
        /// Preferred skills for applicants
        /// </summary>
        public virtual ICollection<OpportunityPreferredSkill> PreferredSkills { get; set; }

        /// <summary>
        /// Search tags for this research opportunity
        /// </summary>
        public virtual ICollection<OpportunitySearchTag> Tags { get; set; }


        public virtual OpportunityCounter OpportunityCounter { get; set; }
    }
}
