using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Blocks.MultiTenancy.Dto;

namespace Blocks.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
