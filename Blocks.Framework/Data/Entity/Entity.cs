﻿using System;
using System.Collections.Generic;

namespace Blocks.Framework.Data.Entity
{
    /// <summary>
    /// A shortcut of <see cref="Entity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public abstract class Entity : Entity<string> 
    {
        public long DATAVERSION { set; get; }
        public DateTime? CREATEDATE { set; get; }
        public string CREATER { set; get; }
        public DateTime? UPDATEDATE { set; get; }
        public string UPDATER { set; get; }
    }
    
    
    /// <summary>
    /// A shortcut of <see cref="Entity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public abstract class Entity<TPrimaryKey> : Abp.Domain.Entities.Entity<TPrimaryKey> 
    {
        
    }
}