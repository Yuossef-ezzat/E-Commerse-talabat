using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresistenceLayer
{
    public static class SpecificationEvaluator
    {
        // Create Query

        public static  IQueryable<TEntity> CreateQuery<TEntity,TKey> (IQueryable<TEntity> inputQuery, ISpecififcations<TEntity,TKey> specififcations)
                                                                where TEntity : BaseEntity<TKey> , new()
        {
            var query = inputQuery;
            // Apply Criteria
            if (specififcations.Criteria is not null)
            {
                query = query.Where(specififcations.Criteria);
            }
            else if (specififcations.IncludeExpressions is not null && specififcations.IncludeExpressions.Count > 0 )
            {
                //foreach (var includeExpression in specififcations.IncludeExpressions)
                //{
                //    query = query.Include(includeExpression);
                //}
                query = specififcations.IncludeExpressions
                        .Aggregate(query, (current, includeExpression) => current.Include(includeExpression));
            }

            return query;
        }




    }
}
