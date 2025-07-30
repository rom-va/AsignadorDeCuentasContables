using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACuyitoApi.Repository
{
     public interface IRepository<T, TKey>
    {
        bool Create(T entity);
        T? GetByID(TKey id);
        bool Update(T entity);
        bool DeleteById(TKey id);
    }
}
