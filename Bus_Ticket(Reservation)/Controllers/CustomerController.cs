using Bus_Ticket_Reservation_.Models;
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
    public class CustomerController : Controller
    {
        customer_bl bl = new customer_bl();
        // GET: Customer
        public ActionResult Home()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(Customer c)
        {
            if(ModelState.IsValid) 
            {
                ViewBag.msg = bl.Register(c);
                ModelState.Clear();
                return View();
            }
            return View();
        }
        [AllowAnonymous]
        public ActionResult Countries()
        {
            List<country> list = bl.FetchCountry();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult States(string CiD)
        {
            return Json(bl.FetchStates(CiD));
        }
        public ActionResult History()
        {
            HttpCookie cid = Request.Cookies["cid1"];
             List<history> history= bl.GetHistory(cid.Value.ToString());
            return View(history);
            
        }
        public ActionResult Book_Ticket()
        {
            HttpCookie s = Request.Cookies["cid1"];
            string s1 = s.Value.ToString();
            return View();
        }

        public ActionResult Book_Ticket1(sample s)
        {
            if (ModelState.IsValid)
            {
                Session["from"] = s.FromLocation;
                Session["to"] = s.ToLocation;
                Session["d"] = s.DateOfJourney;
                Session["t"] = s.Tickets;
                TempData["from"] = s.FromLocation;
                TempData["to"] = s.ToLocation;
                TempData["d"] = s.DateOfJourney;
                TempData["tickets"] = s.Tickets;
                sample sl = new sample(s.FromLocation, s.ToLocation, s.DateOfJourney, s.Tickets);
                List<bus_details> buses = bl.FetchDetails(sl);
                if (buses == null || buses.Count()==0)
                {
                    ViewBag.msg = "Sorry No Buses Available....";
                    return View("Book_Ticket");
                }
                return View(buses);
            }
            return View("Book_Ticket");
        }

        public ActionResult Ticket(string busid,string bustime)
        {

            HttpCookie sk = Request.Cookies["cid1"];
            string cid = sk.Value.ToString();
            string s = Session["from"].ToString();
            string s1 = Session["to"].ToString();
            DateTime s2 = DateTime.Parse(Session["d"].ToString());
            int s3 = int.Parse(Session["t"].ToString());
            sample obj = new sample(s, s1, s2, s3);
            string status = bl.TicketStatus(cid, busid, obj, bustime);
            if (status != null)
            {
                string tcno = bl.GenerateTicketNo(busid);
                HttpCookie ck = new HttpCookie("tcno");
                ck.Value = tcno;
                int maxAgeInSeconds = 10 * 24 * 60 * 60;
                ck.Expires = DateTime.Now.AddSeconds(maxAgeInSeconds);
                Response.Cookies.Add(ck);
                ViewBag.msg = status;
                return View();
            }
            return View();
        }
        public ActionResult CancelTicket()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CancelTicket(string ticketid)
        {
            ViewBag.result = bl.CancelTicket(ticketid);
            
            return View();
        }
       
        public ActionResult Getdetails()
        {
            HttpCookie s = Request.Cookies["cid1"];
            string s1 = s.Value.ToString();
            Customer obj = bl.CustomerDetails(s1);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Getdetails(Customer cl)
        {
            if (ModelState.IsValid)
            {
                TempData["msg"] = bl.UpdateDetails(cl);
                return RedirectToAction("Getdetails");
            }
            return RedirectToAction("Getdetails");

        }
        [AllowAnonymous]
        public JsonResult FetchPlaces()
        {
            List<string> places = bl.FetchPlaces();
            return Json(places, JsonRequestBehavior.AllowGet);
        }
    }
}