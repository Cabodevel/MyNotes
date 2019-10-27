using MyNotesCore.Common;
using MyNotesCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotesCore.Abstract
{
    public interface IRepository<T> where T: BaseEntity, new ()
    {
        Task<ICollection<T>> GetAll();
        Task<Result<T>> GetById(int i);
        Task<Result<T>> Create(T item);
        Task<Result<T>> Update(T item);
        Task<Result<T>> Delete(int id);
    }
}
