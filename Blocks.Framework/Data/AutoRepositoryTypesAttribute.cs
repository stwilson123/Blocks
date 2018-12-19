using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Data
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRepositoryTypesAttribute : Attribute
    {
        public Type RepositoryInterface { get; }

        public Type RepositoryInterfaceWithPrimaryKey { get; }

        public Type RepositoryImplementation { get; }

        public Type RepositoryImplementationWithPrimaryKey { get; }

        public bool WithDefaultRepositoryInterfaces { get; set; }

        public AutoRepositoryTypesAttribute(
            Type repositoryInterface,
            Type repositoryInterfaceWithPrimaryKey,
            Type repositoryImplementation,
            Type repositoryImplementationWithPrimaryKey)
        {
            RepositoryInterface = repositoryInterface;
            RepositoryInterfaceWithPrimaryKey = repositoryInterfaceWithPrimaryKey;
            RepositoryImplementation = repositoryImplementation;
            RepositoryImplementationWithPrimaryKey = repositoryImplementationWithPrimaryKey;
        }
    }
}
