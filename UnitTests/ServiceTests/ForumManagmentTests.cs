using DatabaseLayer;
using DatabaseLayer.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLayer.DTO;
using ServiceLayer.DTO.Forum;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using ServiceLayer.Services.Forum;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnitTests.Common;

namespace UnitTests.ServiceTests
{
    [TestClass]
    public class ForumManagmentTests
    {

        private OrhedgeContext _context;
        private Mock<IErrorHandler> _errHandlerMock;
        private ForumManagmentService _forumMngService;

        [TestInitialize]
        public void TestSetup()
        {
            _context = Utilities.CreateNewContext();
            _errHandlerMock = new Mock<IErrorHandler>();
            IServicesExecutor<DiscussionDTO, Discussion> discExecutor
                    = new ServiceExecutor<DiscussionDTO, Discussion>(_context, _errHandlerMock.Object);
            IServicesExecutor<QuestionDTO, Question> questExecutor
                = new ServiceExecutor<QuestionDTO, Question>(_context, _errHandlerMock.Object);
            IServicesExecutor<ForumCategoryDTO, ForumCategory> forumExecutor
                = new ServiceExecutor<ForumCategoryDTO, ForumCategory>(_context, _errHandlerMock.Object);
            IServicesExecutor<StudentDTO, Student> studentExecutor
                = new ServiceExecutor<StudentDTO, Student>(_context, _errHandlerMock.Object);
            IServicesExecutor<AnswerDTO, Answer> answerExecutor =
                new ServiceExecutor<AnswerDTO, Answer>(_context, _errHandlerMock.Object);
            IServicesExecutor<DiscussionPostDTO, DiscussionPost> postExecutor =
                new ServiceExecutor<DiscussionPostDTO, DiscussionPost>(_context, _errHandlerMock.Object);

            IDiscussionService discussionService = new DiscussionService(discExecutor);
            IQuestionService questService = new QuestionService(questExecutor);
            IForumCategoryService forumService = new ForumCategoryService(forumExecutor);
            IStudentService studentService = new StudentService(studentExecutor);
            IAnswerService answerService = new AnswerService(answerExecutor);
            IDiscussionPostService discussionPostService = new DiscussionPostService(postExecutor);

            _forumMngService =
                new ForumManagmentService(discussionService, studentService, questService,
                forumService, answerService, discussionPostService);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Dispose();
        }


        [DataTestMethod]
        [DataRow(11, 10, 1, 10, 1, 2)]
        [DataRow(11, 10, 2, 1, 2, 2)]
        [DataRow(11, 10, 0, 10, 1, 2)]
        [DataRow(9, 10, 0, 9, 1, 1)]
        [DataRow(9, 10, 1, 9, 1, 1)]
        [DataRow(9, 10, 2, 9, 1, 1)]
        [DataRow(0, 10, 1, 0, 1, 0)]
        public async Task GetDiscussions(int discCount, int itemsPerPage, int page, int expectedTopicsPP, int expectedPage, int expectedPageCount)
        {
            const int catOrder = 1;
            await DataGenerator.AddCategory(_context, "Category", catOrder);
            await DataGenerator.AddDiscussions(_context, "Category", discCount);
            ForumCategory category = _context.ForumCategories.First(cat => cat.Name == "Category");

            TopicListDTO topicListDTO = await _forumMngService.GetDiscussions(category.ForumCategoryId, page, itemsPerPage);
            Assert.AreEqual(expectedTopicsPP, topicListDTO.Topics.Length);
            Assert.AreEqual(expectedTopicsPP, topicListDTO.Authors.Length);
            Assert.AreEqual(expectedTopicsPP, topicListDTO.PostCounts.Length);
            Assert.AreEqual(expectedPage, topicListDTO.Page);
            Assert.AreEqual(expectedPageCount, topicListDTO.PageCount);
            TopicDTO[] questions = topicListDTO.Topics;
            Assert.IsTrue(questions.IsSorted(dto => dto.LastPost, true), "Discussions are not sorted");
            _errHandlerMock.Verify(obj => obj.Handle(It.IsAny<Exception>()), Times.Never);
        }

        [DataTestMethod]
        [DataRow(11, 10, 1, 10, 1, 2)]
        [DataRow(11, 10, 2, 1, 2, 2)]
        [DataRow(11, 10, 0, 10, 1, 2)]
        [DataRow(9, 10, 0, 9, 1, 1)]
        [DataRow(9, 10, 1, 9, 1, 1)]
        [DataRow(9, 10, 2, 9, 1, 1)]
        [DataRow(0, 10, 1, 0, 1, 0)]
        public async Task GetQuestions(int discCount, int itemsPerPage, int page, int expectedTopicsPP, int expectedPage, int expectedPageCount)
        {
            const int catOrder = 1;
            await DataGenerator.AddCategory(_context, "Category", catOrder);
            await DataGenerator.AddQuestions(_context, "Category", discCount);
            ForumCategory category = _context.ForumCategories.First(cat => cat.Name == "Category");

            TopicListDTO topicListDTO = await _forumMngService.GetQuestions(category.ForumCategoryId, page, itemsPerPage);
            Assert.AreEqual(expectedTopicsPP, topicListDTO.Topics.Length);
            Assert.AreEqual(expectedTopicsPP, topicListDTO.Authors.Length);
            Assert.AreEqual(expectedTopicsPP, topicListDTO.PostCounts.Length);
            Assert.AreEqual(expectedPage, topicListDTO.Page);
            Assert.AreEqual(expectedPageCount, topicListDTO.PageCount);
            TopicDTO[] questions = topicListDTO.Topics;
            Assert.IsTrue(questions.IsSorted(dto => dto.LastPost, true), "Questions are not sorted");
            _errHandlerMock.Verify(obj => obj.Handle(It.IsAny<Exception>()), Times.Never);
        }


    }
}
