using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DatabaseLayer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Localization;
using Orhedge.Attributes;
using Orhedge.Enums;
using Orhedge.Helpers;
using Orhedge.ViewModels.TechnicalSupport;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Services;

namespace Orhedge.Controllers
{
    [Authorize]
    public class TechnicalSupportController : Controller
    {
        private readonly IChatMessageService _chatMessageService;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IMapper _mapper;

        public TechnicalSupportController(IChatMessageService chatMessageService, IStringLocalizer<SharedResource> stringLocalizer, IMapper mapper)
        {
            _stringLocalizer = stringLocalizer;
            _chatMessageService = chatMessageService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            // TODO: Get user information
            int totalNumberOfItems = await _chatMessageService.Count(x => !x.Deleted);
            PageInformation pageInformation = new PageInformation(0, totalNumberOfItems, WebConstants.MAX_NUMBER_OF_CHAT_MESSAGES_PER_PAGE);
            TechnicalSupportViewModel technicalSupportViewModel = new TechnicalSupportViewModel(pageInformation)
            {
                ChatMessages = _mapper.Map<List<ChatMessageDTO>, List<ChatMessageViewModel>>(await _chatMessageService.GetWithDetails(0, WebConstants.MAX_NUMBER_OF_CHAT_MESSAGES_PER_PAGE))
            };
            return View(technicalSupportViewModel);
        }

        public async Task<IActionResult> Page(int pageNumber)
            => await RedirectToChat(pageNumber);

        [AuthorizePrivilege(StudentPrivilege.JuniorAdmin, StudentPrivilege.SeniorAdmin)]
        public async Task<IActionResult> DeleteMessage(int chatMessageId)
        {
            ResultMessage<bool> deleted = await _chatMessageService.Delete(chatMessageId);
            return await RedirectToChat(0, deleted.Status.Map());
        }

        public async Task<IActionResult> RedirectToChat(int pageNumber, HttpReponseStatusCode statusCode = HttpReponseStatusCode.NoStatus)
        {
            int totalNumberOfItems = await _chatMessageService.Count(x => !x.Deleted);
            PageInformation pageInformation = new PageInformation(pageNumber, totalNumberOfItems, WebConstants.MAX_NUMBER_OF_CHAT_MESSAGES_PER_PAGE);
            TechnicalSupportViewModel technicalSupportViewModel = new TechnicalSupportViewModel(pageInformation)
            {
                ChatMessages = _mapper.Map<List<ChatMessageDTO>, List<ChatMessageViewModel>>
                (await _chatMessageService.GetWithDetails(pageNumber * WebConstants.MAX_NUMBER_OF_CHAT_MESSAGES_PER_PAGE, WebConstants.MAX_NUMBER_OF_CHAT_MESSAGES_PER_PAGE))
            };
            ViewBag.InfoMessage = new InfoMessage(_stringLocalizer, statusCode);
            return View("Index", technicalSupportViewModel);
        }
    }
}