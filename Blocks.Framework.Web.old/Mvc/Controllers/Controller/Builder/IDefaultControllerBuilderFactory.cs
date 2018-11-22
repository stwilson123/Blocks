namespace Blocks.Framework.Web.Mvc.Controllers.Controller.Builder
{
    public interface IDefaultControllerBuilderFactory
    {
        /// <summary>
        /// Generates a new dynamic api controller for given type.
        /// </summary>
        /// <param name="serviceName">Name of the Api controller service. For example: 'myapplication/myservice'.</param>
        /// <typeparam name="T">Type of the proxied object</typeparam>
        IDefaultControllerBuilder<T> For<T>(string serviceName);

        /// <summary>
        /// Generates multiple dynamic api controllers.
        /// </summary>
        /// <typeparam name="T">Base type (class or interface) for services</typeparam>
        /// <param name="assembly">Assembly contains types</param>
        /// <param name="servicePrefix">Service prefix</param>
        IBatchDefaultControllerBuilder<T> ForAll<T>(string servicePrefix);
    }
}