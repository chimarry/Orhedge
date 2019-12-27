using ServiceLayer.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Interfaces
{
    public interface IServiceTemplate<T> : ICRUDServiceTemplate<T>
    {

        /// <summary>
        /// Gets list of all elements of specific type in storage 
        /// </summary>
        /// <returns>Found or empty list</returns>
        Task<List<T>> GetAll();

        /// <summary>
        /// Gets list of all specified number of elements of specific type in storage from certain position
        /// </summary>
        /// <param name="startPosition">How many elements to skip</param>
        /// <param name="numberOfItems">How many elements to take</param>
        /// <returns>Found or empty list</returns>
        Task<List<T>> GetRange(int startPosition, int numberOfItems);


        /// <summary>
        /// Gets list of all elements of specific type in storage. It sorts elements based on given property and flag that
        /// indicates direction of sorting.
        /// </summary>
        /// <typeparam name="TKey">Type of property on which sorting is applied</typeparam>
        /// <param name="sortKeySelector">Function that gives property on which sorting is applied</param>
        /// <param name="asc">Indicates direction of sorting. Default is ascending.</param>
        /// <returns></returns>
        Task<List<T>> GetAll<TKey>(Func<T, TKey> sortKeySelector, bool asc = true);

        /// <summary>
        /// Gets list of all specified number of elements of specific type in storage from certain position. It sorts elements based on given property and flag that
        /// indicates direction of sorting.
        /// </summary>
        /// <typeparam name="TKey">Type of property on which sorting is applied</typeparam>
        /// <param name="offset">Number of elements to skip</param>
        /// <param name="num">Number of elements to take</param>
        /// <param name="sortKeySelector">Function that gives property on which sorting is applied</param>
        /// <param name="asc">Indicates direction of sorting. Default is ascending</param>
        /// <returns>Found or empty list</returns>
        Task<List<T>> GetRange<TKey>(int offset, int num, Func<T, TKey> sortKeySelector, bool asc = true);

        /// <summary>
        /// Get number of elements in storage
        /// </summary>
        /// <returns>Number of elements</returns>
        Task<int> Count();

        Task<int> Count(Predicate<T> filter);
    }
}
