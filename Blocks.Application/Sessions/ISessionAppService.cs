using System.Threading.Tasks;
using Abp.Application.Services;
using Blocks.Sessions.Dto;

namespace Blocks.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
