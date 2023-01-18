using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IRepository<TEntity>
    {
        List<TEntity> List();
        
        void Insert(TEntity content);

        void Update(TEntity content);

        void Delete(TEntity content);
    }
}
