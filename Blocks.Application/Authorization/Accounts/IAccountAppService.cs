using System.Threading.Tasks;
using Abp.Application.Services;
using Blocks.Authorization.Accounts.Dto;

namespace Blocks.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
