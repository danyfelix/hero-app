using HeroApp.Filters;
using System.Web;
using System.Web.Mvc;

namespace HeroApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
