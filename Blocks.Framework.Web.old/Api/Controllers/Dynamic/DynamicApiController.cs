using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Dependency;
using Blocks.Framework.Localization;

namespace Blocks.Framework.Web.Api.Controllers.Dynamic
{
    /// <summary>
    /// This class is used as base class for all dynamically created ApiControllers.  
    /// </summary>
    /// <typeparam name="T">Type of the proxied object</typeparam>
    /// <remarks>
    /// A dynamic ApiController is used to transparently expose an object (Generally an Application Service class)
    /// to remote clients.
    /// </remarks>
    public class DynamicApiController<T> : DynamicApiController, IDynamicApiController, IAvoidDuplicateCrossCuttingConcerns
    {
        public List<string> AppliedCrossCuttingConcerns { get; }


        public Localizer localizer { set; get; }

        public DynamicApiController(IocManager iocManager)
        {
            AppliedCrossCuttingConcerns = new List<string>();
        }
    }
}