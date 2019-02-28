using System;
using Blocks.Framework.Configurations;

namespace Blocks.Framework.Data.Entity
{
    public interface IEntityConfiguration : IConfiguration
    {
        /// <summary>
        /// Entity  assembly name,it will be not to register in blocks system if it is null or empty.
        /// </summary>
        string EntityModule  { get;   }


    }
}