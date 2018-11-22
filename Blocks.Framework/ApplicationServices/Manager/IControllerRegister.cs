using Blocks.Framework.ApplicationServices.Controller;

namespace Blocks.Framework.ApplicationServices.Manager
{
    public interface IControllerRegister
    {
        void Register(IControllerInfo controllerInfo);
    }
}