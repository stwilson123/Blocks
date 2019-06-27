using Newtonsoft.Json;

namespace Blocks.Framework.Auditing
{
    public class LocalizedSerializer : JsonNetAuditSerializer
    {
        private readonly IAuditingConfiguration _configuration;
        public LocalizedSerializer(IAuditingConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }
        
        public override string Serialize(object obj)
        {
            var options = new JsonSerializerSettings
            {
                ContractResolver = new LocalizedContractResolver(_configuration.IgnoredTypes,_configuration.TypeConverts),
            };

            return JsonConvert.SerializeObject(obj, options);
        }
    }
}