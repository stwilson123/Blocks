using System;
using Abp.Domain.Entities;

namespace Blocks.Framework.Data
{
    /// <summary>
    /// A shortcut of <see cref="IRepository{TEntity,TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepository<TEntity> : Abp.Domain.Repositories.IRepository<TEntity, Guid> where TEntity : class, IEntity<Guid>
    {

    }
}