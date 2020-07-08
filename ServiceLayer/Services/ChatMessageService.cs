using DatabaseLayer;
using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ChatMessageService : BaseService<ChatMessageDTO, ChatMessage>, IChatMessageService
    {
        private readonly OrhedgeContext _context;
        private readonly IStudentService _studentService;

        public ChatMessageService(IServicesExecutor<ChatMessageDTO, ChatMessage> servicesExecutor, IStudentService studentService, OrhedgeContext orhedgeContext) :
            base(servicesExecutor)
        {
            _context = orhedgeContext;
            _studentService = studentService;
        }

        /// <summary>
        /// Retuns list of chat messages with specific details.
        /// </summary>
        /// <param name="offset">Number of elements to skip</param>
        /// <param name="limit">Number of elements to take</param>
        public async Task<List<ChatMessageDTO>> GetWithDetails(int offset, int limit)
        {
            List<ChatMessageDTO> messages = await GetRange(offset, limit, condition: x => !x.Deleted, sortKeySelector: x => x.SentOn);
            foreach (ChatMessageDTO message in messages)
            {
                StudentDTO student = await _studentService.GetSingleOrDefault(x => x.StudentId == message.StudentId);
                message.Username = student?.Username ?? "NA";
                message.StudentInitials = string.Format("{0}{1}", student?.Name.First(), student?.LastName.First());
            }
            return messages;
        }

        /// <summary>
        /// Saves chat message in database, only if user has not exceeded daily limit <see cref="MaxNumberOfMessagesPerDay"/>.
        /// </summary>
        public async Task<ResultMessage<ChatMessageDTO>> Add(ChatMessageDTO dto)
        {
            int numberOfMessagePerDay = _context.ChatMessages.Count(x => x.StudentId == dto.StudentId && x.SentOn.Date == dto.SentOn.Date);
            if (numberOfMessagePerDay >= Constants.MAX_NUMBER_OF_MESSAGES_PER_DAY)
                return new ResultMessage<ChatMessageDTO>(OperationStatus.NotSupported);
            return await _servicesExecutor.Add(dto, x => x.StudentId != dto.StudentId && x.Message != dto.Message && x.SentOn != dto.SentOn);
        }

        public async Task<ResultMessage<bool>> Delete(int id)
           => await _servicesExecutor.Delete((ChatMessage x) => x.ChatMessageId == id && !x.Deleted, x => { x.Deleted = true; return x; });


        public async Task<ResultMessage<ChatMessageDTO>> GetSingleOrDefault(Predicate<ChatMessageDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);

        public async Task<ResultMessage<ChatMessageDTO>> Update(ChatMessageDTO dto)
           => await _servicesExecutor.Update(dto, x => x.ChatMessageId == dto.ChatMessageId);
    }
}
