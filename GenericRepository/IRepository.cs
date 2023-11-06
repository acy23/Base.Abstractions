using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepository
{
    public interface IRepository
    {
        Task<T?> Get<T>(int id) where T : class;
        Task<T> Create<T>(T entity) where T : class;
        Task<T> Update<T>(T entity, int id) where T : class;
        Task<List<T>> GetList<T>() where T : class;
        Task<List<T>> GetListByExpression<T>(Expression<Func<T, bool>> predicate) where T : class;
    }
}
