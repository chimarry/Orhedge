using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface IChatMessageService : ICRUDServiceTemplate<ChatMessageDTO>, ISelectableServiceTemplate<ChatMessageDTO>
    {
        Task<List<ChatMessageDTO>> GetWithDetails(int offset, int limit);
    }
}
