using Blocks.Framework.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Security.Authorization.Permission
{
    public interface IPermission
    {
        string Name { get;  }

        string Navigation { get;  }

        string ResourceKey { get; }

        ILocalizableString DisplayName { get;  }


        string Type { get;  }
    }
}
