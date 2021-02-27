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
 * Object model for a join table between an Opportunity and a Required Skill
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URC.Models
{
    public class OpportunityRequiredSkill
    {
        public int OpportunityId { get; set; }
        public Opportunity Opportunity { get; set; }

        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
