using HeroApp.Logger;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HeroApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly Log _logger = new Log(new DataBaseFirstEntities());

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        
        protected void Application_Error()
        {
            //log the error!
            _logger.LogException(Server.GetLastError());
        }
    }
}
