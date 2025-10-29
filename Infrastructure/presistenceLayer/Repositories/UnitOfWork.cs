using DomainLayer.Contracts;
using DomainLayer.Models;
using PresistenceLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresistenceLayer.Repositories
{
    public class UnitOfWork(StoreDbContext _context) : IUnitOfWork
    {

        private readonly Dictionary<string, object> _repositories = [];
        public IGenaricRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>, new()
        {
            var TypeName = typeof(TEntity).Name;

            if (_repositories.ContainsKey(TypeName))
                return (IGenaricRepository<TEntity, TKey>)_repositories[TypeName];
            else
            {
                var repo = new GenaricRepository<TEntity, TKey>(_context);
                _repositories.Add(TypeName, repo);
                return repo;
            }
        }


        public async Task<int> SaveChangesAsync()
           => await _context.SaveChangesAsync();
        
        
    }
}
