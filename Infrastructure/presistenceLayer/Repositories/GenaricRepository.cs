using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using PresistenceLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresistenceLayer.Repositories
{
    public class GenaricRepository<TEntity, TKey>(StoreDbContext _context) : IGenaricRepository<TEntity, TKey>
           where TEntity : BaseEntity<TKey>, new()
    {
        public async Task AddAsync(TEntity entity)
           => await _context.Set<TEntity>().AddAsync(entity);
        
        

        public void Delete(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);
        
        

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _context.Set<TEntity>().ToListAsync();



        public async Task<TEntity?> GetByIdAsync(TKey id)
            => await _context.Set<TEntity>().FindAsync(id) ;
        
        

        public void Update(TEntity entity)
            => _context.Set<TEntity>().Update(entity);
    }
}
