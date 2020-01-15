using DatabaseLayer.Entity;
using ServiceLayer.AutoMapper;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Helpers;
using ServiceLayer.Students.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Services
{
    public class DiscussionService : BaseService<DiscussionDTO, Discussion>, IDiscussionService
    {
        public DiscussionService(IServicesExecutor<DiscussionDTO, Discussion> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<Status> Add(DiscussionDTO topic)
            => await _servicesExecutor.Add(topic, x => false);


        public async Task<Status> Delete(int id)
        {
            Discussion discussion = await _servicesExecutor.GetOne(x => x.TopicId == id && !x.Deleted);
            discussion.Deleted = true;
            return await _servicesExecutor.Delete(discussion);
        }

        public async Task<List<DiscussionDTO>> GetAll()
            => await _servicesExecutor.GetAll(x => !x.Deleted);


        public async Task<List<DiscussionDTO>> GetAll<TKey>(Func<DiscussionDTO, TKey> sortKeySelector, bool asc = true)
            => await _servicesExecutor.GetAll(x => !x.Deleted, sortKeySelector, asc);


        public async Task<DiscussionDTO> GetById(int id)
            => await _servicesExecutor.GetSingleOrDefault(x => x.TopicId == id && !x.Deleted);


        public async Task<List<DiscussionDTO>> GetRange(int startPosition, int numberOfItems)
            => throw new NotImplementedException();


        public async Task<List<DiscussionDTO>> GetRange<TKey>(int offset, int num, Func<DiscussionDTO, TKey> sortKeySelector, bool asc = true)
            => await _servicesExecutor.GetRange(offset, num, x => !x.Deleted, sortKeySelector, asc);

        public async Task<List<DiscussionDTO>> GetRange<TKey>(int offset, int num, Predicate<DiscussionDTO> filter, Func<DiscussionDTO, TKey> sortKeySelector, bool asc = true)
            => await _servicesExecutor.GetRange(offset, num, x => filter(Mapping.Mapper.Map<DiscussionDTO>(x)), sortKeySelector, asc);
        

        public async Task<DiscussionDTO> GetSingleOrDefault(Predicate<DiscussionDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);


        public Task<Status> Update(DiscussionDTO discussion)
            => _servicesExecutor.Update(discussion, x => x.TopicId == discussion.TopicId && !x.Deleted);

    }
}
