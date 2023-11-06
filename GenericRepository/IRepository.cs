using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepository
{
    public interface IRepository
    {
        Task<T?> Get<T>(int id) where T : class;
        Task<T> Create<T>(T entity) where T : class;
    }
}
