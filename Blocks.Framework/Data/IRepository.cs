using System;
using System.Collections.Generic;
using Abp.Domain.Entities;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blocks.Framework.Data
{
    /// <summary>
    /// A shortcut of <see cref="IRepository{TEntity,TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepository<TEntity> : Abp.Domain.Repositories.IRepository<TEntity, string> where TEntity : Blocks.Framework.Data.Entity.Entity
    {
        /// <summary>
        /// Updates entity by expressions.
        /// </summary>
        /// <param name="wherePredicate"></param>
        /// <param name="updateFactory"></param>
        /// <returns>Updated numbers</returns>
        Int32 Update(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, TEntity>> updateFactory);


        /// <summary>
        /// Updates entity async by expressions.
        /// </summary>
        /// <param name="wherePredicate"></param>
        /// <param name="updateFactory"></param>
        /// <returns>Updated numbers</returns>
        Task<Int32> UpdateAsync(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, TEntity>> updateFactory);
        
        
        /// <summary>
        /// Inserts new entitites.
        /// </summary>
        /// <param name="entitites">Inserted entitites</param>
        IList<TEntity> Insert(IList<TEntity> entitites);

        /// <summary>
        /// Inserts new entitites.
        /// </summary>
        /// <param name="entitites">Inserted entitites</param>
        Task<IList<TEntity>> InsertAsync(IList<TEntity> entity);
    }
}