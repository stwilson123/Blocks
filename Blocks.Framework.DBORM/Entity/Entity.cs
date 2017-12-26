using System;
using Abp.Domain.Entities;

namespace Blocks.Framework.DBORM.Entity
{
    /// <summary>
    /// A shortcut of <see cref="Entity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public abstract class Entity : Abp.Domain.Entities.Entity<Guid> 
    {
        
    }
}