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
 * SignalR hub for handling chat
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using URC.Data;
using URC.Models;
using Microsoft.AspNetCore.SignalR;
using System.Web;
using URC.Areas.Identity.Data;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly static Dictionary<string, string> _ConnectionMap = new Dictionary<string, string>();

        private readonly URC_Context _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatHub(URC_Context context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task JoinRoom(string user, string professor)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, user+professor);
        }
        
        public async Task LeaveRoom(string user, string professor)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, user+professor);
        }

        public async Task LoadMessage(string userId, string professorId)
        {
            var room = userId + professorId;
            var messages = await _context.Messages
                    .Where(m => m.ChatRoomID == room)
                    .OrderBy(m => m.TimeStamp)
                    .ToListAsync();

            foreach (var message in messages)
            {
                var user = await _userManager.FindByIdAsync(message.UserID);
                var username = user.Name;
                var time = message.TimeStamp;
                var msg = message.Message;

                await Clients.Group(room).SendAsync("ReceiveMessage", time.ToString("MM/dd/yyyy HH:mm"), username, msg);
            }
        }

        public async Task SendMessage(string userId, string message, string professorId, bool isSenderStudent)
        {
            var room = userId + professorId;
            if(!_context.Rooms.Any(r => r.ChatRoomID == room))
            {
                await _context.Rooms.AddAsync(new ChatRoom() { ChatRoomID = room });
                _context.SaveChanges();
            }
            var time = DateTime.Now;

            // More Secure: 
            if(string.IsNullOrEmpty(message))
            {
                return;
            }
            else
            {
                message = HttpUtility.HtmlEncode(message);
            }

            var res = _context.Messages.Add(new ChatMessage {
                Message = message,
                Name = userId,
                TimeStamp = DateTime.Now,
                ChatRoomID = room,
                UserID = isSenderStudent ? userId : professorId
            }).Entity;
            _context.SaveChanges();

            var user = await _userManager.FindByIdAsync(isSenderStudent ? userId : professorId);
            var username = user.Name;

            await Clients.Group(room).SendAsync("ReceiveMessage", time.ToString("MM/dd/yyyy HH:mm"), username, message, res.ChatMessageID);
            
        }
    }
}