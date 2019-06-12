using System.Linq;
using System.Reflection;
using Abp.Dependency;
using Blocks.Framework.Environment.Extensions;
using Blocks.Framework.Types;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using ISingletonDependency = Blocks.Framework.Ioc.Dependency.ISingletonDependency;

namespace Blocks.Framework.Localization
{
    public class LocalzaionHelper : ISingletonDependency
    {
        private IocManager _ioc;
        public LocalzaionHelper(IocManager ioc)
        {
            _ioc = ioc;
        }

        public ILocalizableString CreateModuleLocalizableString(TypeInfo type,string text,params object[] args)
        {
            if(TypeHelper.GetActualType(type, out var instanceType))
                throw new LocalizationException(StringLocal.Format($"type {type} not found ActualType"));
            var moduleId = type.Assembly.GetName().Name;
            var AvailableExtensions = _ioc.Resolve<IExtensionManager>().AvailableFeatures();
            var ModuleName = AvailableExtensions.FirstOrDefault(f => f.Id == moduleId)?.Name;
            if (string.IsNullOrEmpty(ModuleName))
                ModuleName = AvailableExtensions.FirstOrDefault(t => t.SubAssembly.Contains(moduleId))?.Name;
                    
            Check.NotNull(ModuleName, $"Can't find [{moduleId}] in AvailableFeatures");
            return new LocalizableString(ModuleName,text, args);
        }
        
        
        public ILocalizableString CreateModuleLocalizableString(TypeInfo type,MethodInfo methodInfo,params object[] args)
        {
            Check.NotNull(methodInfo, "methodInfo");
            var attribute = methodInfo.GetCustomAttribute<LocalizedDescriptionAttribute>();
            
            return attribute != null ? CreateModuleLocalizableString(type, attribute.Name,args) : null;
        }
    }
}