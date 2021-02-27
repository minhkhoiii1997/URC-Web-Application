/**
 * Author:    Kevin Tran
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
 * Object model representing a chat room
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URC.Models
{
    public class ChatRoom
    {
        public string ChatRoomID { get; set; }
        public ICollection<ChatMessage> Messages { get; set; }
    }
}
