using Abp.Dependency;
using Newtonsoft.Json;

namespace Blocks.Framework.Auditing
{
    public class JsonNetAuditSerializer : IAuditSerializer, ITransientDependency
    {
        private readonly IAuditingConfiguration _configuration;

        public JsonNetAuditSerializer(IAuditingConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual string Serialize(object obj)
        {
            var options = new JsonSerializerSettings
            {
                ContractResolver = new AuditingContractResolver(_configuration.IgnoredTypes,_configuration.TypeConverts),
            };

            return JsonConvert.SerializeObject(obj, options);
        }
    }
}