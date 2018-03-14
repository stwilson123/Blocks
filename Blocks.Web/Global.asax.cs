using Blocks.Framework.Web;
using Castle.Facilities.Logging;
using Abp.Castle.Logging.Log4Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Abp;
using Abp.Dependency;
using Abp.PlugIns;
using Abp.Threading;
using Abp.Web;
using Abp.Web.Localization;
using Blocks.Framework.FileSystems.VirtualPath;
using Blocks.Framework.Web.FileSystems.VirtualPath;

namespace Blocks.Web
{
    public class MvcApplication : BlocksWebApplication<BlocksWebModule>
    {


        protected  void Application_Start(object sender, EventArgs e)
        {
     
            base.Application_Start(sender, e);
        
        }

        protected  void Application_End(object sender, EventArgs e)
        {
            base.Application_End(sender, e);
        }

        protected  void Session_Start(object sender, EventArgs e)
        {
        }

        protected  void Session_End(object sender, EventArgs e)
        {
        }

        protected  void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected  void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected  void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            base.Application_PostAuthenticateRequest(sender, e);

        }

        protected  void Application_EndRequest(object sender, EventArgs e)
        {
        }

        protected  void Application_Error(object sender, EventArgs e)
        {
        }

        
    }
}