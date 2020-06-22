using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Orhedge.Helpers;
using Orhedge.ViewModels.TechnicalSupport;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Services;

namespace Orhedge.Controllers
{
    public class TechnicalSupportController : Controller
    {
        private readonly IChatMessageService _chatMessageService;
        private readonly IMapper _mapper;

        public const int MaxNumberOfItemsPerPage = 6;

        public TechnicalSupportController(IChatMessageService chatMessageService, IMapper mapper)
        {
            _chatMessageService = chatMessageService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Chat()
        {
            // TODO: Get user information
            int totalNumberOfItems = await _chatMessageService.Count(x => !x.Deleted);
            PageInformation pageInformation = new PageInformation(0, totalNumberOfItems, MaxNumberOfItemsPerPage);
            TechnicalSupportViewModel technicalSupportViewModel = new TechnicalSupportViewModel(pageInformation)
            {
                ChatMessages = _mapper.Map<List<ChatMessageDTO>, List<ChatMessageViewModel>>(await _chatMessageService.GetWithDetails(0, MaxNumberOfItemsPerPage))
            };
            return View(technicalSupportViewModel);
        }

        public async Task<IActionResult> Page(int pageNumber)
            => await RedirectToChat(pageNumber);

        public async Task<IActionResult> DeleteMessage(int chatMessageId)
        {
            ResultMessage<bool> deleted = await _chatMessageService.Delete(chatMessageId);
            return await RedirectToChat(0);
        }

        public async Task<IActionResult> RedirectToChat(int pageNumber)
        {
            int totalNumberOfItems = await _chatMessageService.Count(x => !x.Deleted);
            PageInformation pageInformation = new PageInformation(pageNumber, totalNumberOfItems, MaxNumberOfItemsPerPage);
            TechnicalSupportViewModel technicalSupportViewModel = new TechnicalSupportViewModel(pageInformation)
            {
                ChatMessages = _mapper.Map<List<ChatMessageDTO>, List<ChatMessageViewModel>>
                (await _chatMessageService.GetWithDetails(pageNumber * MaxNumberOfItemsPerPage, MaxNumberOfItemsPerPage))
            };
            return View("Chat", technicalSupportViewModel);
        }
    }
}