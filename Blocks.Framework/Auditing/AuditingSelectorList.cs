using System.Collections.Generic;
using Abp;

namespace Blocks.Framework.Auditing
{
    internal class AuditingSelectorList : List<NamedTypeSelector>, IAuditingSelectorList
    {
        public bool RemoveByName(string name)
        {
            return RemoveAll(s => s.Name == name) > 0;
        }
    }
}