using Bus_Ticket_Reservation_.Models.BusinessLayer;
using Bus_Ticket_Reservation_.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bus_Ticket_Reservation_.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
       
        // GET: Admin
        AdminBL bl = new AdminBL();
        [AllowAnonymous]
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Bus()
        {
            return View();
        }

        public ActionResult Route()
        {
            return View();
        }
        public ActionResult Schedule()
        {
            return View();
        }
        public ActionResult AddNewBus()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddNewBus(bus busDetails)
        {
            if (ModelState.IsValid)
            {
                ViewBag.result = bl.AddBus(busDetails);
                ModelState.Clear();
                return View();
            }
            return View();
        }

        public ActionResult DeleteBus()
        {
            List<bus> busDetails = bl.FetchBusDetails();
            return View(busDetails);
        }
        [HttpPost]
        public ActionResult DeleteBus(string busid)
        {
            ViewBag.msg = bl.deleteBus(busid);
            return View();
        }
        public ActionResult UpdateBus()
        {
            List<bus> busDetails = bl.FetchBusDetails();
            return View(busDetails);
        }
        public ActionResult UpdateBus1(string busid)
        {
            bus b = bl.GetBusDetailsById(busid);
            return View(b);
        }

        public ActionResult UpdateBus2(bus b)
         {
            if (ModelState.IsValid)
            {
                TempData["msg"] = bl.UpdateBusDetails(b);
                return View();
            }            
            return View();
        }
        public ActionResult ViewBusData()
        {
            List<bus> busDetails = bl.FetchBusDetails();
            return View(busDetails);
        }
        public ActionResult AddNewRoute()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewRoute(route routeDetails)
        {
            if(ModelState.IsValid)
            {
                ViewBag.result = bl.AddRoute(routeDetails);
                ModelState.Clear();
            }
            return View();
        }
        public ActionResult DeleteRoute()
        {
            List<route> routeDetails = bl.FetchRouteDetails();
            return View(routeDetails);
        }
        [HttpPost]
        public ActionResult DeleteRoute(string routeid)
        {
            ViewBag.msg = bl.DeleteRoute(routeid);
            return View();
        }
        public ActionResult UpdateRoute()
        {
            List<route> routeDetails = bl.FetchRouteDetails();
            return View(routeDetails);
        }

        public ActionResult UpdateRoute1(string routeid)
        {
            route routeDetails = bl.GetRouteDetailsById(routeid);
            return View(routeDetails);
        }

        public ActionResult UpdateRoute2(route routeDetails)
        {
            ViewBag.msg = bl.UpdateRouteDetails(routeDetails);
            return View();
        }
        public ActionResult viewroutedata()
        {
            List<route> routeDetails = bl.FetchRouteDetails();
            return View(routeDetails);
        }
        public ActionResult AddNewSchedule()
        {
            List<string> routeIds = bl.fetchrouteid();
            List<SelectListItem> routeItems = new List<SelectListItem>();
            for (int i = 0; i < routeIds.Count; i++)
            {
                routeItems.Add(new SelectListItem { Value = routeIds[i], Text = routeIds[i] });
            }

            // Add a default "Select" option
            ViewBag.routes = routeItems;

            List<string> busids = bl.fetchbusid();

            List<SelectListItem> BusItems = new List<SelectListItem>();
            for (int i = 0; i < busids.Count; i++)
            {
                BusItems.Add(new SelectListItem { Value = busids[i], Text = busids[i] });
            }
            ViewBag.busids = BusItems;
            return View();
        }
        [HttpPost]
        public ActionResult AddNewSchedule(schedule routeDetails)
        {
            List<string> routeIds = bl.fetchrouteid();
            List<SelectListItem> routeItems = new List<SelectListItem>();
            for (int i = 0; i < routeIds.Count; i++)
            {
                routeItems.Add(new SelectListItem { Value = routeIds[i], Text = routeIds[i] });
            }

            // Add a default "Select" option
            ViewBag.routes = routeItems;

            List<string> busids = bl.fetchbusid();

            List<SelectListItem> BusItems = new List<SelectListItem>();
            for (int i = 0; i < busids.Count; i++)
            {
                BusItems.Add(new SelectListItem { Value = busids[i], Text = busids[i] });
            }
            ViewBag.busids = BusItems;
            ViewBag.result = bl.AddSchedule(routeDetails);
            ModelState.Clear();
            return View();
        }
        public ActionResult DeleteSchedule()
        {
            List<schedule> scheduleDetails = bl.FetchScheduleDetails();
            return View(scheduleDetails);
        }
        [HttpPost]
        public ActionResult DeleteSchedule(string scheduleid)
        {
            ViewBag.msg = bl.DeleteSchedule(scheduleid);
            return View();
        }
        public ActionResult UpdateSchedule()
        {
            List<schedule> scheduleDetails = bl.FetchScheduleDetails();
            return View(scheduleDetails);
        }

        public ActionResult UpdateSchedule1(string scheduleid)
        {
            List<string> routeIds = bl.fetchrouteid();
            List<SelectListItem> routeItems = new List<SelectListItem>();
            for (int i = 0; i < routeIds.Count; i++)
            {
                routeItems.Add(new SelectListItem { Value = routeIds[i], Text = routeIds[i] });
            }

            // Add a default "Select" option
            ViewBag.routes = routeItems;

            List<string> busids = bl.fetchbusid();

            List<SelectListItem> BusItems = new List<SelectListItem>();
            for (int i = 0; i < busids.Count; i++)
            {
                BusItems.Add(new SelectListItem { Value = busids[i], Text = busids[i] });
            }

            // Add a default "Select" option



            ViewBag.busids = BusItems;
            schedule scheduleDetails = bl.GetScheduleDetailsById(scheduleid);
            return View(scheduleDetails);
        }

        public ActionResult UpdateSchedule2(schedule scheduleDetails)
        {
            TempData["msg"] = bl.UpdateScheduleDetails(scheduleDetails);
            return View("UpdateSchedule1");
        }
    }
}