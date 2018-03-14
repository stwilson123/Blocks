using System.Threading.Tasks;
using Abp.Application.Services;
using Blocks.Configuration.Dto;

namespace Blocks.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}