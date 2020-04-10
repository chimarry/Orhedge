using ServiceLayer.ErrorHandling;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface ICRUDServiceTemplate<T>
    {
        /// <summary>
        /// Adds new element in storage
        /// </summary>
        /// <param name="dto">Element to add in storage</param>
        /// <returns>Status depending on success of operation</returns>
        Task<ResultMessage<T>> Add(T dto);

        /// <summary>
        /// Deletes element from storage
        /// </summary>
        /// <param name="id">Unique identifier of element to be deleted</param>
        /// <returns>Status depending on success of operation</returns>
        Task<ResultMessage<bool>> Delete(int id);

        /// <summary>
        /// Updates element in storage
        /// </summary>
        /// <param name="dto">Object that contains identifier of element to be updated and updated values on other property places
        /// </param>
        /// <returns>Status depending on success of operation</returns>
        Task<ResultMessage<T>> Update(T dto);

        /// <summary>
        /// Finds element in list based on certain condition
        /// </summary>
        /// <param name="condition">Function that gives specification of what element need to satisfy</param>
        /// <returns>Found element or null</returns>
        Task<ResultMessage<T>> GetSingleOrDefault(Predicate<T> condition);
    }
}
