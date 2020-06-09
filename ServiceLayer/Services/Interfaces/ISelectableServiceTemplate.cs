using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface ISelectableServiceTemplate<TDto>
    {
        /// <summary>
        /// Gets list of all elements of specific type in storage. It sorts elements based on given property and flag that
        /// indicates direction of sorting.
        /// </summary>
        /// <typeparam name="TKey">Type of property on which sorting is applied</typeparam>
        /// <param name="condition">Condition elements must satisfy</param>
        /// <param name="sortKeySelector">Function that gives property on which sorting is applied</param>
        /// <param name="asc">Indicates direction of sorting. Default is ascending.</param>
        /// <returns></returns>
        Task<List<TDto>> GetAll<TKey>(Predicate<TDto> condition = null, Func<TDto, TKey> sortKeySelector = null, bool asc = true);


        /// <summary>
        /// Gets list of all specified number of elements of specific type in storage from certain position. It sorts elements based on given property and flag that
        /// indicates direction of sorting.
        /// </summary>
        /// <typeparam name="TKey">Type of property on which sorting is applied</typeparam>
        /// <param name="offset">Number of elements to skip</param>
        /// <param name="num">Number of elements to take</param>
        /// <param name="condition">Condition elements must satisfy</param>
        /// <param name="sortKeySelector">Function that gives property on which sorting is applied</param>
        /// <param name="asc">Indicates direction of sorting. Default is ascending</param>
        /// <returns>Found or empty list</returns>
        Task<List<TDto>> GetRange<TKey>(int offset, int num, Predicate<TDto> condition = null, Func<TDto, TKey> sortKeySelector = null, bool asc = true);

        /// <summary>
        /// Counts all records in database correspoding DTO class, that satisfy condition.
        /// </summary>
        /// <param name="filter">Condition elements must satisfy</param>
        /// <returns>Number of records</returns>
        Task<int> Count(Predicate<TDto> filter = null);
    }
}
