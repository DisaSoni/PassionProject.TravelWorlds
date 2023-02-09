using System.Web;
using System.Web.Mvc;

namespace PassionProject.TravelWorlds
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
