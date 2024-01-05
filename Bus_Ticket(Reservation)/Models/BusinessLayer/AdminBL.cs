using Bus_Ticket_Reservation_.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bus_Ticket_Reservation_.Models.BusinessLayer
{
    public class AdminBL
    {
        Dboperations db = new Dboperations();
        public string AddBus(bus busDetails)
        {
            return db.AddBus(busDetails);
        }
        public List<bus> FetchBusDetails()
        {
            return db.FetchBusDetails();
        }
        public string deleteBus(string busId)
        {
            return db.deleteBus(busId);
        }
        public bus GetBusDetailsById(string busid)
        {
            return db.GetBusDetailsById(busid);
        }
        public string UpdateBusDetails(bus b)
        {
            return db.UpdateBusDetails(b);
        }
        public string AddRoute(route routeDetails)
        {
            return db.AddRoute(routeDetails);
        }
        public List<route> FetchRouteDetails()
        {
            return db.FetchRouteDetails();
        }
        public string DeleteRoute(string routeId)
        {
            return db.DeleteRoute(routeId);
        }
        public route GetRouteDetailsById(string routeid)
        {
            return db.GetRouteDetailsById(routeid);
        }
        public string UpdateRouteDetails(route routeDetails)
        {
            return db.UpdateRouteDetails(routeDetails);
        }
        public string AddSchedule(schedule scheduleDetails)
        {
            return db.AddSchedule(scheduleDetails);
        }
        public List<schedule> FetchScheduleDetails()
        {
            return db.FetchScheduleDetails();
        }
        public string DeleteSchedule(string scheduleId)
        {
            return db.DeleteSchedule(scheduleId);
        }
        public schedule GetScheduleDetailsById(string scheduleId)
        {
            return db.GetScheduleDetailsById(scheduleId);
        }
        public string UpdateScheduleDetails(schedule scheduleDetails)
        {
            return db.UpdateScheduleDetails(scheduleDetails);
        }
        public List<string> fetchrouteid()
        {
            return db.fetchrouteid();
        }
        public List<string> fetchbusid()
        {
            return db.fetchbusid();
        }
    }
}