using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IGenaricRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>, new()
    {
        Task<TEntity?> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        #region With Specification
        Task<TEntity?> GetByIdAsync(ISpecififcations<TEntity,TKey> specififcations);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecififcations<TEntity, TKey> specififcations);

        Task<int> CountAsync(ISpecififcations<TEntity, TKey> specififcations);
        #endregion
    }
}
