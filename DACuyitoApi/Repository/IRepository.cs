using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACuyitoApi.Repository
{
     public interface IRepository<T>
    {
        public bool Create(T entity);
        public T? GetByID(int id);
        public bool Update(T entity);
        public bool DeleteById(int id);
    }
}
