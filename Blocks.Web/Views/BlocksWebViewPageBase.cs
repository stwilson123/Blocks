using Abp.Web.Mvc.Views;

namespace Blocks.Web.Views
{
    public abstract class BlocksWebViewPageBase : BlocksWebViewPageBase<dynamic>
    {

    }

    public abstract class BlocksWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected BlocksWebViewPageBase()
        {
            LocalizationSourceName = BlocksConsts.LocalizationSourceName;
        }
    }
}