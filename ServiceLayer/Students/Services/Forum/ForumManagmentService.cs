using ServiceLayer.DTO;
using ServiceLayer.DTO.Forum;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Exceptions;
using ServiceLayer.Students.Interfaces;
using ServiceLayer.Students.Interfaces.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Services.Forum
{
    public class ForumManagmentService : IForumManagmentService
    {

        private readonly IDiscussionService _discussionService;
        private readonly IStudentService _studentService;
        private readonly IForumCategoryService _forumCategoryService;
        private readonly IQuestionService _questionService;
        private readonly IAnswerService _answerService;
        private readonly IDiscussionPostService _discussionPostService;

        public ForumManagmentService(IDiscussionService discussionService, 
            IStudentService studentService, 
            IQuestionService questionService,
            IForumCategoryService forumCategoryService,
            IAnswerService answerService,
            IDiscussionPostService discussionPostService)
            => (_discussionService, _studentService, _questionService,
            _forumCategoryService, _answerService, _discussionPostService)
            = (discussionService, studentService, questionService, 
            forumCategoryService, answerService, discussionPostService);

        // TODO: Retrieve PostCount
        public async Task<TopicListDTO> GetDiscussions(int categoryId, int page, int itemsPerPage)

        {
            TopicListDTO topicList = await GetTopics(_discussionService, categoryId, page, itemsPerPage);
            List<int> postCount = await GetReplyCount(topicList, false);
            topicList.PostCounts = postCount.ToArray();
            return topicList;
        }


        public async Task<TopicListDTO> GetQuestions(int categoryId, int page, int itemsPerPage)
        {
            TopicListDTO topicList = await GetTopics(_questionService, categoryId, page, itemsPerPage);
            List<int> postCount = await GetReplyCount(topicList, true);
            topicList.PostCounts = postCount.ToArray();
            return topicList;
        }


        /// <summary>
        /// Retrieve number of posts or answers for given topics
        /// </summary>
        /// <param name="topicList">List of topics</param>
        /// <param name="answers">If true return answer count, otherwise return post count</param>
        /// <returns></returns>
        private async Task<List<int>> GetReplyCount(TopicListDTO topicList, bool answers)
        {
            List<int> postCountPerTopic = new List<int>();
            foreach (TopicDTO topic in topicList.Topics)
            {
                int postCount;
                if (answers)
                    postCount = await _answerService.Count(post => post.TopicId == topic.TopicId && !post.Deleted);
                else
                    postCount = await _discussionPostService.Count(post => post.TopicId == topic.TopicId && !post.Deleted);
                postCountPerTopic.Add(postCount);
            }
            return postCountPerTopic;
        }

        private async Task<(int, int)> CheckPage<T>(IServiceTemplate<T> service, int page, int categoryId, int itemsPerPage) where T : TopicDTO
        {
            int topicCount = await service.Count(x => !x.Deleted && x.ForumCategoryId == categoryId);
            int pageCount = (int)Math.Ceiling(((double)topicCount) / itemsPerPage);
            return (page < 1 || page > pageCount ? 1 : page, pageCount);
        }

        private async Task<TopicListDTO> GetTopics<T>(IServiceTemplate<T> service, int categoryId, int page, int itemsPerPage) 
            where T : TopicDTO
        {
            // Return default category id if given does not exists
            categoryId = await CheckCategoryId(categoryId);
            // Return first page if given page does not exists
            int pageCount;
            (page, pageCount) = await CheckPage(service, page, categoryId, itemsPerPage);

            List<T> topics = await service.GetRange((page - 1) * itemsPerPage,
                itemsPerPage, d => d.ForumCategoryId == categoryId, d => d.LastPost, false);

            List<StudentDTO> authors = await RetrieveAuthors(topics);
            return new TopicListDTO
            {
                Page = page,
                Topics = topics.ToArray(),
                Authors = authors.ToArray(),
                PageCount = pageCount,
                Category = categoryId, 
            };
        }

        private async Task<List<StudentDTO>> RetrieveAuthors<T>(List<T> topics) where T : TopicDTO
        {
            List<StudentDTO> authors = new List<StudentDTO>();

            foreach (TopicDTO topic in topics)
            {
                StudentDTO student = await _studentService.GetById(topic.StudentId);
                authors.Add(student);
            }

            return authors;
        }


        private async Task<int> CheckCategoryId(int categoryId)
        {
            List<ForumCategoryDTO> allCategories = await _forumCategoryService
                .GetAll();

            IEnumerable<ForumCategoryDTO> orderedCategories = allCategories.OrderBy(cat => cat.Order);
            ForumCategoryDTO catDTO = orderedCategories.FirstOrDefault(cat => cat.ForumCategoryId == categoryId);
            return catDTO == null ? orderedCategories.First().ForumCategoryId : catDTO.ForumCategoryId;
        }

        public async Task<Status> AddDiscussion(int forumCategoryId, int studentId, string title, string content)
        {
            if (await _forumCategoryService.GetSingleOrDefault(cat => cat.ForumCategoryId == forumCategoryId) == null) 
            {
                throw new ForumException("Category does not exist");
            }

            DiscussionDTO newDiscussion = new DiscussionDTO
            {
                ForumCategoryId = forumCategoryId,
                Title = title,
                Content = content,
                StudentId = studentId,
                Created = DateTime.UtcNow,
                LastPost = DateTime.UtcNow
            };

            return await _discussionService.Add(newDiscussion);
        }

        public async Task<Status> AddQuestion(int forumCategoryId, int studentId, string title, string content)
        {
            if(await _forumCategoryService.GetSingleOrDefault(cat => cat.ForumCategoryId == forumCategoryId) == null)
            {
                throw new ForumException("Category does not exist");
            }

            QuestionDTO newQuestion = new QuestionDTO
            {
                ForumCategoryId = forumCategoryId,
                Title = title,
                Content = content,
                StudentId = studentId,
                Created = DateTime.UtcNow,
                LastPost = DateTime.UtcNow
            };

            return await _questionService.Add(newQuestion);
        }

        public async Task<DiscussionDTO> GetDiscussion(int discussionId)
        {
            return await _discussionService.GetById(discussionId);
        }

        public async Task<DiscussionPostsDTO> GetDiscussionPosts(int discussionId)
        {
            List<DiscussionPostDTO> discussionPostList = await _discussionPostService.GetAll<bool>(x => x.TopicId == discussionId && !x.Deleted);

            List<StudentDTO> authors = new List<StudentDTO>();
            foreach (DiscussionPostDTO discussionPost in discussionPostList)
            {
                StudentDTO student = await _studentService.GetById(discussionPost.StudentId);
                authors.Add(student);
            }

            return new DiscussionPostsDTO
            {
                DiscussionPosts = discussionPostList.ToArray(),
                Authors = authors.ToArray()
            };
        }

        public async Task<StudentDTO> GetAuthor(int studentId)
        {
            return await _studentService.GetById(studentId);
        }
    }
}
