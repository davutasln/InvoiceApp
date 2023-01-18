using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly InvoiceAppDbContext _context;

        DbSet<TEntity> _object;

        public GenericRepository(InvoiceAppDbContext context)
        {
            this._context = context;
            _object = _context.Set<TEntity>();
        }

        public void Delete(TEntity content)
        {
            _object.Remove(content);
            _context.SaveChanges();
        }

        public List<TEntity> List()
        {
            return _object.ToList();
        }

        public void Insert(TEntity content)
        {
            _object.Add(content);
            _context.SaveChanges();
        }

        public void Update(TEntity content)
        {
            _context.SaveChanges();
        }
    }
}
