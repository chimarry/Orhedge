using ServiceLayer.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Helpers
{
    public interface IServicesExecutor<TDto, TEntity> where TEntity : class
    {
        /// <summary>
        /// Adds new element in storage
        /// </summary>
        /// <param name="dto">Element to add</param>
        /// <param name="condition">Function to check if element exists</param>
        /// <returns>Status depending on success of operation</returns>
        Task<Status> Add(TDto dto, Predicate<TEntity> condition);

        /// <summary>
        /// Deletes element from storage
        /// </summary>
        /// <param name="entity">Element from storage to delete</param>
        /// <returns>Status depending on success of operation</returns>
        Task<Status> Delete(TEntity entity);

        /// <summary>
        /// Updates element in storage
        /// </summary>
        /// <param name="dto">Object that contains identifier of element to be updated and updated values on other property places
        /// </param>
        /// <param name="condition">Function to check if element exists</param>
        /// <returns>Status depending on success of operation</returns>
        Task<Status> Update(TDto dto, Predicate<TEntity> condition);

        /// <summary>
        /// Gets one element from storage based on passed condition it must satisfy
        /// </summary>
        /// <param name="condition">Function that is used to uniquely identify element</param>
        /// <returns>Found element or null</returns>
        Task<TDto> GetSingleOrDefault(Predicate<TDto> condition);


        /// <summary>
        /// Gets list of all elements of specific type in storage 
        /// </summary>
        /// <param name="condition">Function that filters elements to return</param>
        /// <returns>Found or empty list</returns>
        Task<List<TDto>> GetAll(Predicate<TEntity> condition);

        /// <summary>
        /// Gets list of all specified number of elements of specific type in storage from certain position
        /// </summary>
        /// <param name="offset">How many elements to skip</param>
        /// <param name="noItems">How many elements to take</param>
        /// <param name="condition">Function that filters elements to return</param>
        /// <returns>Found or empty list</returns>
        Task<List<TDto>> GetRange(int offset, int noItems, Predicate<TEntity> condition);

        /// <summary>
        /// Gets one element from storage based on passed condition it must satisfy
        /// </summary>
        /// <param name="condition">Function that is used to uniquely identify element</param>
        /// <returns>Found element or null</returns>
        Task<TEntity> GetOne(Predicate<TEntity> condition);

        /// <summary>
        /// Gets list of all elements of specific type in storage. It sorts elements based on given property and flag that
        /// indicates direction of sorting.
        /// </summary>
        /// <typeparam name="TKey">Type of property on which sorting is applied</typeparam>
        /// <param name="condition">Function that filters elements</param>
        /// <param name="sortKeySelector">Function that gives property on which sorting is applied</param>
        /// <param name="asc">Indicates direction of sorting. Default is ascending.</param>
        /// <returns></returns>
        Task<List<TDto>> GetAll<TKey>(Predicate<TEntity> condition, Func<TDto, TKey> sortKeySelector, bool asc = true);

        /// <summary>
        /// Gets list of all specified number of elements of specific type in storage from certain position. It sorts elements based on given property and flag that
        /// indicates direction of sorting.
        /// </summary>
        /// <typeparam name="TKey">Type of property on which sorting is applied</typeparam>
        /// <param name="offset">Number of elements to skip</param>
        /// <param name="num">Number of elements to take</param>
        /// <param name="filter">Function that filters elements</param>
        /// <param name="sortKeySelector">Function that gives property on which sorting is applied</param>
        /// <param name="asc">Indicates direction of sorting. Default is ascending</param>
        /// <returns>Found or empty list</returns>
        Task<List<TDto>> GetRange<TKey>(int offset, int num, Predicate<TEntity> filter, Func<TDto, TKey> sortKeySelector, bool asc = true);

        /// <summary>
        /// Get number of elements of specified type in storage
        /// </summary>
        /// <returns>Number of elements</returns>
        Task<int> Count();
        Task<int> Count(Predicate<TEntity> filter);
    }
}
