using Blocks.Framework.ApplicationServices.Attributes;
using Blocks.Framework.ApplicationServices.Controller.Attributes;
using Blocks.Framework.Security.Authorization.Permission.Attributes;
using Blocks.Framework.Web.Mvc.Controllers;
using Blocks.Framework.Web.Web.HttpMethod;

namespace Blocks.Core.Test.Navigation.TestModels
{
    public class TestMvcController : BlocksWebMvcController
    {
        [BlocksActionName("Default")]
        [BlocksAuthorize("View")]
        public void Default()
        {
            throw new System.NotImplementedException();
        }

     
        [BlocksActionName("TestDelete"),HttpMethod(HttpVerb.Delete)]
        public void TestDelete()
        {
            throw new System.NotImplementedException();
        }

        [BlocksActionName("TestIgnore"), RemoteService(false)]
        public void TestIgnore()
        {
            throw new System.NotImplementedException();
        }

        public void TestNoActionName()
        {
            
        }
    }
}