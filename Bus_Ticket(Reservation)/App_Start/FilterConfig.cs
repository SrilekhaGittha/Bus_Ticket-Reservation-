using System.Web;
using System.Web.Mvc;

namespace Bus_Ticket_Reservation_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
