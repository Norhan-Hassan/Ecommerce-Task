using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.Repo_Interfaces
{
    public interface IGenericRepo<T>where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null, string? include = null);
        T GetFirstOrDefault(Expression<Func<T, bool>>? predicate = null, string? include = null);
        void Add(T item);
        void Remove(T item);
        void RemoveRange(IEnumerable<T> items);
    }
}
