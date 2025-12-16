using DomainLayer.Models;


namespace DomainLayer.Contracts
{
    public interface IUnitOfWork
    {
        public IGenaricRepository<TEntity, TKey> GetRepository<TEntity, TKey>() 
            where TEntity : BaseEntity<TKey> , new();
        public Task<int> SaveChangesAsync();
    }
}
