using Blocks.Framework.Configurations;

namespace Blocks.Framework.Web.Configuartions
{
    public interface IWebFrameworkConfiguration : IConfiguration
    {
        /// <summary>
        /// Application Service assembly name,it will be not to register in blocks system if it is null or empty.
        /// </summary>
        string AppModule { get;  }
        
        /// <summary>
        /// Respository  assembly name,it will be not to register in blocks system if it is null or empty.
        /// </summary>
        string RespositoryModule  { get;  }
        
        /// <summary>
        /// Respository  assembly name,it will be not to register in blocks system if it is null or empty.
        /// </summary>
        string DomainModule  { get;   }

    }
}