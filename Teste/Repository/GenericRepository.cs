using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;
using Teste.Repository.Interface;
using System.Linq;
using Teste.Context;

namespace Teste.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly Contexto _contexto; 
         
        public GenericRepository(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _contexto.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _contexto.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _contexto.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _contexto.Set<T>().AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _contexto.Set<T>().Update(entity);
            await SaveChangesAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            _contexto.Set<T>().Remove(entity);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}