using System;

namespace Blocks.Framework.Data.Entity
{
    /// <summary>
    /// A shortcut of <see cref="Entity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public abstract class Entity : Entity<string> 
    {
        
    }
    
    
    /// <summary>
    /// A shortcut of <see cref="Entity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public abstract class Entity<TPrimaryKey> : Abp.Domain.Entities.Entity<TPrimaryKey> 
    {
        
    }
}