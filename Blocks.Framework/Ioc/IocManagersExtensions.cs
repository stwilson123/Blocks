using System.Reflection;
using Abp.Dependency;

namespace Blocks.Framework.Ioc
{
    public static class IocManagersExtensions
    {
        public static void RegisterAssemblyByConventionEx(this IIocManager iocManager,Assembly assembly)
        {
          
            iocManager.RegisterAssemblyByConvention(assembly);
        }
    }
}