using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsapp.Models;

namespace Whatsapp.Interface
{
    public interface IRepository<T> 
    {
        IEnumerable<T> GetAll();
        T Get(long id);
        Response Insert(T entity);
        //  void Update(T entity);
        Response Delete(T entity);
    }
}
