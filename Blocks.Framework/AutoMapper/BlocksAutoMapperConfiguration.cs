using System;
using System.Collections.Generic;
using AutoMapper;

namespace Blocks.Framework.AutoMapper
{
    public class BlocksAutoMapperConfiguration : IBlocksAutoMapperConfiguration
    {
        public List<Action<IMapperConfigurationExpression>> Configurators { get; }

        public bool UseStaticMapper { get; set; }

        public BlocksAutoMapperConfiguration()
        {
            UseStaticMapper = true;
            Configurators = new List<Action<IMapperConfigurationExpression>>();
        }
    }
}