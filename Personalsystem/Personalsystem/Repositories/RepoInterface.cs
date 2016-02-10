using Personalsystem.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Personalsystem.Repositories
{

    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Find(int id);
        void Save(T Entity);
        void Edit(T Entity);
        void Delete(T Entity);


    }

} 