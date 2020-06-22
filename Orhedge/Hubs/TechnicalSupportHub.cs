using Microsoft.AspNetCore.SignalR;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orhedge.Hubs
{
    public class TechnicalSupportHub : Hub
    {
        private readonly IChatMessageService _chatMessageService;

        public TechnicalSupportHub(IChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }

        public async Task SendMessage(string user, string message)
        {
            int studentId = 1;
            ChatMessageDTO chatMessage = new ChatMessageDTO()
            {
                Message = message,
                SentOn = DateTime.Now,
                StudentId = studentId
            };

            await Clients.All.SendAsync("ReceiveMessage", user, message);

            ResultMessage<ChatMessageDTO> addedResult = await _chatMessageService.Add(chatMessage);
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
