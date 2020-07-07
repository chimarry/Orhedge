using ServiceLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface IChatMessageService : ICRUDServiceTemplate<ChatMessageDTO>, ISelectableServiceTemplate<ChatMessageDTO>
    {
        /// <summary>
        /// Retuns list of chat messages with specific details.
        /// </summary>
        /// <param name="offset">Number of elements to skip</param>
        /// <param name="limit">Number of elements to take</param>
        Task<List<ChatMessageDTO>> GetWithDetails(int offset, int limit);
    }
}
