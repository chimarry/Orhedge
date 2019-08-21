using ServiceLayer.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Interfaces
{
    public interface IServiceTemplate<T>
    {
        Task<DbStatus> Add(T dto);
        Task<DbStatus> Delete(int id);
        Task<DbStatus> Update(T dto);

        Task<T> GetById(int id);
        Task<IList<T>> GetAll();
        Task<IList<T>> GetRange(int startPosition, int numberOfItems);
    }
}
