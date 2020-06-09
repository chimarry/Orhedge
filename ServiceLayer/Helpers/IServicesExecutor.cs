using ServiceLayer.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Helpers
{
    public interface IServicesExecutor<TDto, TEntity> where TEntity : class
    {
        /// <summary>
        /// Adds new element in storage
        /// </summary>
        /// <param name="dto">Element to add</param>
        /// <param name="condition">Function to check if element exists</param>
        /// <returns>Status depending on success of operation</returns>
        Task<ResultMessage<TDto>> Add(TDto dto, Predicate<TEntity> condition);

        /// <summary>
        /// Deletes element from storage
        /// </summary>
        /// <param name="entity">Element from storage to delete</param>
        /// <returns>Status depending on success of operation</returns>
        Task<ResultMessage<bool>> Delete(Predicate<TEntity> filter, Func<TEntity, TEntity> applyDelete);

        /// <summary>
        /// Updates element in storage
        /// </summary>
        /// <param name="dto">Object that contains identifier of element to be updated and updated values on other property places
        /// </param>
        /// <param name="condition">Function to check if element exists</param>
        /// <returns>Status depending on success of operation</returns>
        Task<ResultMessage<TDto>> Update(TDto dto, Predicate<TEntity> condition);

        /// <summary>
        /// Gets one element from storage based on passed condition it must satisfy
        /// </summary>
        /// <param name="condition">Function that is used to uniquely identify element</param>
        /// <returns>Found element or null</returns>
        Task<ResultMessage<TDto>> GetSingleOrDefault(Predicate<TDto> condition);


        /// <summary>
        /// Gets list of all elements of specific type in storage. It sorts elements based on given property and flag that
        /// indicates direction of sorting.
        /// </summary>
        /// <typeparam name="TKey">Type of property on which sorting is applied</typeparam>
        /// <param name="dtoCondition">Function that filters elements</param>
        /// <param name="sortKeySelector">Function that gives property on which sorting is applied</param>
        /// <param name="asc">Indicates direction of sorting. Default is ascending.</param>
        /// <returns></returns>
        Task<List<TDto>> GetAll<TKey>(Predicate<TDto> dtoCondition = null, Func<TDto, TKey> sortKeySelector = null, bool asc = true);

        /// <summary>
        /// Gets list of all specified number of elements of specific type in storage from certain position. It sorts elements based on given property and flag that
        /// indicates direction of sorting.
        /// </summary>
        /// <typeparam name="TKey">Type of property on which sorting is applied</typeparam>
        /// <param name="offset">Number of elements to skip</param>
        /// <param name="num">Number of elements to take</param>
        /// <param name="dtoCondition">Function that filters elements</param>
        /// <param name="sortKeySelector">Function that gives property on which sorting is applied</param>
        /// <param name="asc">Indicates direction of sorting. Default is ascending</param>
        /// <returns>Found or empty list</returns>
        Task<List<TDto>> GetRange<TKey>(int offset, int num, Predicate<TDto> dtoCondition = null, Func<TDto, TKey> sortKeySelector = null, bool asc = true);

        /// <summary>
        /// Gets one element from storage based on passed condition it must satisfy
        /// </summary>
        /// <param name="condition">Function that is used to uniquely identify element</param>
        /// <returns>Found element or null</returns>
        Task<TEntity> GetSingleOrDefault(Predicate<TEntity> condition);

        /// <summary>
        /// Get number of elements of specified type in storage
        /// </summary>
        /// <returns>Number of elements</returns>
        Task<int> Count(Predicate<TDto> filter = null);
    }
}
