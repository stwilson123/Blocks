namespace Blocks.Framework.Web.Mvc.Controllers.Controller.Builder
{
    public interface IBatchApiControllerBuilder<T>
    {
        /// <summary>
        /// Builds the controller.
        /// This method must be called at last of the build operation.
        /// </summary>
        void Build();
    }
}