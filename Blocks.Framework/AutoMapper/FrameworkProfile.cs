using System;
using AutoMapper;

namespace Blocks.Framework.AutoMapper
{
    public static class FrameworkProfile  
    {
        public static Action<IMapperConfigurationExpression> DefaultAutoMapperConfig()
        {
            return (mapper) => mapper.ForAllMaps((tm, t) => t.ValidateMemberList(MemberList.Source));
        }
    }
}