using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Orhedge.Helpers;
using ServiceLayer.DTO;
using ServiceLayer.Services;
using System;
using System.Threading.Tasks;

namespace Orhedge.Hubs
{
    [Authorize]
    public class TechnicalSupportHub : Hub
    {
        private readonly IChatMessageService _chatMessageService;
        private readonly IStudentService _studentService;

        public TechnicalSupportHub(IChatMessageService chatMessageService, IStudentService studentService)
        {
            _chatMessageService = chatMessageService;
            _studentService = studentService;
        }

        public async Task SendMessage(string message)
        {
            int studentId = Context.User.GetUserId();
            ChatMessageDTO chatMessage = new ChatMessageDTO()
            {
                Message = message,
                SentOn = DateTime.Now,
                StudentId = studentId
            };
            StudentDTO student = await _studentService.GetStudentById(studentId);

            await Clients.All.SendAsync("ReceiveMessage", student.Initials, student.Username, message, Context.User.IsAdministrator());

            await _chatMessageService.Add(chatMessage);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
