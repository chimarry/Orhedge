using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Orhedge.ViewModels.Forum;
using ServiceLayer.DTO.Forum;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Interfaces.Forum;

namespace Orhedge.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumManagmentService _forumService;
        private readonly IMapper _mapper;
        private readonly int _itemsPerPage;

        public ForumController(IForumManagmentService forumService, IMapper mapper, IConfiguration config)
        {
            (_forumService, _mapper) = (forumService, mapper);
            _itemsPerPage = int.Parse(config["Forum:QuestionsDiscussionsPerPage"]);
        }

        // TODO: Add Authorize attribute
        public async Task<IActionResult> Index(int catId, int dPage = 1, int qPage = 1)
        {
            TopicListDTO discussions = await _forumService.GetDiscussions(catId, dPage, _itemsPerPage);
            TopicListDTO questions = await _forumService.GetQuestions(catId, qPage, _itemsPerPage);
            ForumViewModel forumVm = new ForumViewModel
            {
                Discussions = _mapper.Map<TopicListDTO, TopicSelectionViewModel[]>(discussions),
                Questions = _mapper.Map<TopicListDTO, TopicSelectionViewModel[]>(questions),
                DiscussionSelectedPage = discussions.Page,
                QuestionSelectedPage = questions.Page,
                DiscussionPageCount = discussions.PageCount,
                QuestionPageCount = questions.PageCount
            };

            return View(forumVm);
        }

        public IActionResult CreateDiscussion()
        {
            return View();
        }

    }
}