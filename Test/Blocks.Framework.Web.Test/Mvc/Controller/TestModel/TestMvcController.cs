using Blocks.Framework.ApplicationServices;
using Blocks.Framework.ApplicationServices.Attributes;
using Blocks.Framework.ApplicationServices.Controller.Attributes;
using Blocks.Framework.Web.Mvc.Controllers;
using Blocks.Framework.Web.Web.HttpMethod;

namespace Blocks.Framework.Web.Test.Mvc.Controller.TestModel
{
   
    public class TestMvcController : BlocksWebMvcController
    {
        [BlocksActionName("Default")]
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