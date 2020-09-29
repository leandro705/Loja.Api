using Loja.Domain.Interfaces.Repository;
using Loja.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Loja.Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly LojaDbContext _context;

        public Repository(LojaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public void Create(T entity)
        {
            _context.Add(entity);           
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);            
        } 

        public int SaveChanges()
        {           
            return _context.SaveChanges();           
        }
    }
}
