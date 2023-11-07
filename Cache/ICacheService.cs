using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache
{
    public interface ICacheService
    {
        void Set<Titem>(string key, object value) where Titem : class;
        object Get<TEntity>(string key) where TEntity : class;
        void Remove<TEntity>(string key) where TEntity : class;
    }
}
