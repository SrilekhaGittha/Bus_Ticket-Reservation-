using Bus_Ticket_Reservation_.Models.BusinessLayer;
using Bus_Ticket_Reservation_.Models.DataLayer;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Bus_Ticket_Reservation_.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        customer_bl bl = new customer_bl();
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email,string password)
        {   

            string s = bl.ValidateLogin(email, password);
            FormsAuthentication.SetAuthCookie(email, false);
            if (s.Equals("Successfully Loggedin"))
            {
                Session["type"] = "Customer";
                string cid=bl.FetchByEmail(email);
                HttpCookie ck = new HttpCookie("cid1");
                ck.Value = cid;
                int maxAgeInSeconds = 10 * 24 * 60 * 60;
                ck.Expires = DateTime.Now.AddSeconds(maxAgeInSeconds);
                Response.Cookies.Add(ck);
                return RedirectToAction("Home", "Customer");
            }
            else if(s.Equals("Admin"))
            {
                Session["type"] = "Admin";
                return RedirectToAction("Home", "Admin");
            }
            else{
                ViewBag.msg = s;
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return View();
        }
        public ActionResult SearchBuses()
        {
            return View();
        }
        public ActionResult SearchBuses1(sample1 s)
        {
            if (ModelState.IsValid)
            { 
                sample1 sl = new sample1(s.FromLocation, s.ToLocation, s.DateOfJourney);
                List<bus_details> buses = bl.FetchDetails1(sl);
                if (buses == null || buses.Count==0)
                {
                    ViewBag.msg = "Sorry No Buses Available....";
                    return View("SearchBuses");
                }
                return View(buses); 
            }
            return View("SearchBuses");
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

    }
}