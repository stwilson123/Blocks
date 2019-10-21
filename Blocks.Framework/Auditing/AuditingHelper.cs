using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.Timing;
using Blocks.Framework.AutoMapper;
using Blocks.Framework.Localization;
using Blocks.Framework.Security;
using Blocks.Framework.Utility.Extensions;
using Castle.Core.Logging;
using CollectionExtensions = Abp.Collections.Extensions.CollectionExtensions;

namespace Blocks.Framework.Auditing
{
    public class AuditingHelper : IAuditingHelper, ITransientDependency
    {
        public ILogger Logger { get; set; }
        public IAbpSession AbpSession { get; set; }
        public IAuditingStore AuditingStore { get; set; }

        private readonly IAuditInfoProvider _auditInfoProvider;
        private readonly IAuditingConfiguration _configuration;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAuditSerializer _auditSerializer;
        private readonly LocalzaionHelper _localzaionHelper;
        private readonly IUserContext _userContext;
        private readonly LocalizedSerializer _localizedSerializer;

        public AuditingHelper(
            IAuditInfoProvider auditInfoProvider,
            IAuditingConfiguration configuration,
            IUnitOfWorkManager unitOfWorkManager,
            IAuditSerializer auditSerializer,
            LocalzaionHelper localzaionHelper,
            IUserContext userContext,
            LocalizedSerializer localizedSerializer
            )
        {
            _auditInfoProvider = auditInfoProvider;
            _configuration = configuration;
            _unitOfWorkManager = unitOfWorkManager;
            _auditSerializer = auditSerializer;
            _localzaionHelper = localzaionHelper;
            _userContext = userContext;
            _localizedSerializer = localizedSerializer;

            AbpSession = NullAbpSession.Instance;
            Logger = NullLogger.Instance;
            AuditingStore = SimpleLogAuditingStore.Instance;
        }

        public bool ShouldSaveAudit(MethodInfo methodInfo, bool defaultValue = false)
        {
            if (!_configuration.IsEnabled)
            {
                return false;
            }

            if (!_configuration.IsEnabledForAnonymousUsers && (AbpSession?.UserId == null))
            {
                return false;
            }

            if (methodInfo == null)
            {
                return false;
            }

            if (!methodInfo.IsPublic)
            {
                return false;
            }

            if (methodInfo.IsDefined(typeof(AuditedAttribute), true))
            {
                return true;
            }

            if (methodInfo.IsDefined(typeof(DisableAuditingAttribute), true))
            {
                return false;
            }

            var classType = methodInfo.DeclaringType;
            if (classType != null)
            {
                if (classType.GetTypeInfo().IsDefined(typeof(AuditedAttribute), true))
                {
                    return true;
                }

                if (classType.GetTypeInfo().IsDefined(typeof(DisableAuditingAttribute), true))
                {
                    return false;
                }

                if (_configuration.Selectors.Any(selector => selector.Predicate(classType)))
                {
                    return true;
                }
            }

            return defaultValue;
        }

        public AuditInfo CreateAuditInfo(Type type, MethodInfo method, object[] arguments)
        {
            return CreateAuditInfo(type, method, CreateArgumentsDictionary(method, arguments));
        }

        public AuditInfo CreateAuditInfo(Type type, MethodInfo method, IDictionary<Tuple<string,IEnumerable<Attribute>>, object> arguments)
        {
            var userAccount  = _userContext.GetCurrentUser()?.UserAccount;
            
            var auditInfo = new AuditInfo
            {
                TenantId = AbpSession.TenantId,
                UserId = AbpSession.UserId,
                UserAccount = userAccount,
                ImpersonatorUserId = AbpSession.ImpersonatorUserId,
                ImpersonatorTenantId = AbpSession.ImpersonatorTenantId,
                ServiceName = type != null
                    ? type.FullName
                    : "",
                MethodName = method.Name,
                MethodDescription = _localzaionHelper.CreateModuleLocalizableString(type.GetTypeInfo(), method)?.Name,
                Parameters = ConvertArgumentsToJson(arguments.ToDictionary(k => k.Key.Item1,v => v.Value)),
                ParametersDescription = ConvertLocalizedArgumentsToJson(arguments),
                ExecutionTime = Clock.Now
            };

            try
            {
                _auditInfoProvider.Fill(auditInfo);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
            }

            return auditInfo;
        }

        public AuditInfo UpdateAuditInfo(AuditInfo auditInfo, Exception ex, object returnParams)
        {
            auditInfo.Exception = ex;
            auditInfo.OutParameters = _auditSerializer.Serialize(returnParams);
            auditInfo.OutParametersDescription = _localizedSerializer.Serialize(returnParams);

            return auditInfo;
        }

        public void Save(AuditInfo auditInfo)
        {
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.Suppress))
            {
                AuditingStore.Save(auditInfo);
                uow.Complete();
            }
        }

        public async Task SaveAsync(AuditInfo auditInfo)
        {
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.Suppress))
            {
                await AuditingStore.SaveAsync(auditInfo);
                await uow.CompleteAsync();
            }
        }

        private string ConvertArgumentsToJson(IDictionary<string, object> arguments)
        {
            try
            {
                if (CollectionExtensions.IsNullOrEmpty(arguments))
                {
                    return "{}";
                }

                var dictionary = new Dictionary<string, object>();

                foreach (var argument in arguments)
                {
                    if (argument.Value != null && _configuration.IgnoredTypes.Any(t => t.IsInstanceOfType(argument.Value)))
                    {
                        dictionary[argument.Key] = null;
                    }
                    else
                    {

                        var typeConvert =
                            _configuration.TypeConverts.FirstOrDefault(t => t.Key.IsInstanceOfType(argument.Value));
                        
                        dictionary[argument.Key] = typeConvert.Key == null ? argument.Value : 
                            typeConvert.Value(argument.Value);
                    }
                }

                return _auditSerializer.Serialize(dictionary);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
                return "{}";
            }
        }
        
        
        private string ConvertLocalizedArgumentsToJson(IDictionary<Tuple<string,IEnumerable<Attribute>>, object> arguments)
        {
            try
            {
                if (CollectionExtensions.IsNullOrEmpty(arguments))
                {
                    return "{}";
                }

                var dictionary = new Dictionary<string, object>();

                foreach (var argument in arguments)
                {
                    var key = ((LocalizedDescriptionAttribute) argument.Key.Item2.FirstOrDefault(i =>
                        i is LocalizedDescriptionAttribute))?.Name;
                    var value = argument.Value;
                    if (key == null)
                    {
                        continue;
                    }
                    if (argument.Value != null && _configuration.IgnoredTypes.Any(t => t.IsInstanceOfType(argument.Value)))
                    {
                        dictionary[key] = null;
                    }
                    else
                    {

                        var typeConvert =
                            _configuration.TypeConverts.FirstOrDefault(t => t.Key.IsInstanceOfType(argument.Value));
                        
                        dictionary[key] = typeConvert.Key == null ? value : 
                            typeConvert.Value(value);
                    }
                }

                return _localizedSerializer.Serialize(dictionary);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
                return "{}";
            }
        }

        private static Dictionary<Tuple<string,IEnumerable<Attribute>>, object> CreateArgumentsDictionary(MethodInfo method, object[] arguments)
        {
            var parameters = method.GetParameters();
            var dictionary = new Dictionary<Tuple<string,IEnumerable<Attribute>>, object>();

            for (var i = 0; i < parameters.Length; i++)
            {
                dictionary[new Tuple<string,IEnumerable<Attribute>>(parameters[i].Name,parameters[i].GetCustomAttributes())] = arguments[i];
            }

            return dictionary;
        }
    }
}