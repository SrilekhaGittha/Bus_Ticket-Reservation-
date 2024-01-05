using Bus_Ticket_Reservation_.Models.BusinessLayer;
using Microsoft.Ajax.Utilities;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Routing;

namespace Bus_Ticket_Reservation_.Models.DataLayer
{
    public class Dboperations
    {
        BusTicketReservationSystemEntities pr = null;
        public Dboperations()
        {
            pr = new BusTicketReservationSystemEntities();
        }
        public string Register(Customer c)
        {
            while (true)
            {
                int currentYear = DateTime.Now.Year;
                int currentMonth = DateTime.Now.Month;

                // Generate a random unique number (4 digits)
                Random random = new Random();
                int uniqueNumber = int.Parse(random.Next(10).ToString() + random.Next(10).ToString() + random.Next(10).ToString() + random.Next(10).ToString());

                // Combine the parts to form the Customer ID (CID)
                string cid = $"{currentYear:D4}{currentMonth:D2}{uniqueNumber:D4}";
                var e1 = (from e in pr.Customers
                          where e.CID == cid
                          select e).FirstOrDefault();
                if (e1 == null)
                {
                    c.CID = cid;
                    break;
                }
            }

            try
            {
                c.password = BCrypt.Net.BCrypt.HashPassword(c.password);
                pr.Customers.Add(c);
                pr.SaveChanges();
                return "Registered Successfully.....!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public List<country> FetchCountry()
        {
            pr.Configuration.ProxyCreationEnabled = false;
            List<country> cl = pr.countries.ToList();
            return cl;
        }
        public List<state> FetchStates(string id)
        {
            pr.Configuration.ProxyCreationEnabled = false;
            List<state> cl = pr.states.Where(x => x.countryid == id).ToList();
            return cl;
        }
        public string FetchByBusId(string busid)
        {
            var s = (from e1 in pr.buses
                     where e1.busid == busid
                     select e1.busname).FirstOrDefault();
            return s.ToString();
        }
        public List<bus_details> FetchDetails(sample s)
        {
            List<bus_details> all_buses = new List<bus_details>();

            pr.Configuration.ProxyCreationEnabled = false;
            var e = (from e1 in pr.routes
                     where e1.pickup == s.FromLocation && e1.dropout == s.ToLocation
                     select e1).FirstOrDefault();
            if (e != null)
            {
                List<string> schedules = new List<string>();
                List<int> seats = new List<int>();
                foreach (var r in pr.seatAvailabilities)
                {
                    if (r.dateofJourney == s.DateOfJourney && s.Tickets <= r.availableSeats)
                    {
                        schedules.Add(r.scheduleid);
                        seats.Add(int.Parse(r.availableSeats.ToString()));
                    }
                }
                foreach (var r in pr.schedules)
                {
                    if (schedules.Contains(r.SID) && r.RouteId == e.routeid)
                    {
                        var bus = (from e1 in pr.buses
                                   where e1.busid == r.BusId
                                   select e1).FirstOrDefault();
                        var seatavailable = seats[schedules.IndexOf(r.SID)];
                        all_buses.Add(new bus_details(bus.busname, bus.busid, bus.bustype, seatavailable, r.Time, e.cost.Value * s.Tickets));
                    }
                }
            }
            else
            {
                return null;
            }
            return all_buses;
        }
        public List<bus_details> FetchDetails1(sample1 s)
        {
            List<bus_details> all_buses = new List<bus_details>();

            pr.Configuration.ProxyCreationEnabled = false;
            var e = (from e1 in pr.routes
                     where e1.pickup == s.FromLocation && e1.dropout == s.ToLocation
                     select e1).FirstOrDefault();

            if (e != null)
            {

                List<string> schedules = new List<string>();
                List<int> seats = new List<int>();
                foreach (var r in pr.seatAvailabilities)
                {
                    if (r.dateofJourney == s.DateOfJourney)
                    {
                        schedules.Add(r.scheduleid);
                        seats.Add(int.Parse(r.availableSeats.ToString()));
                    }
                }

                foreach (var r in pr.schedules)
                {
                    if (schedules.Contains(r.SID))
                    {
                        var bus = (from e1 in pr.buses
                                   where e1.busid == r.BusId
                                   select e1).FirstOrDefault();
                        var seatavailable = seats[schedules.IndexOf(r.SID)];
                        all_buses.Add(new bus_details(bus.busname, bus.busid, bus.bustype, seatavailable, r.Time, e.cost.Value));
                    }
                }
            }
            else
            {
                return null;
            }
            if(all_buses==null)
            {
                return null;
            }
            return all_buses;
        }
        public string TicketStatus(string cid, string busid, sample s,string bustime)
        {
            string ticktidvalue = null;
            var flag = 0;
            string x1=s.DateOfJourney.ToString();
            string overalldate1 = x1.Substring(0, 11);
            using (var pr1 = new BusTicketReservationSystemEntities())
            {
                pr1.Configuration.ProxyCreationEnabled = false;
                var e = (from e1 in pr1.routes
                         where e1.pickup == s.FromLocation && e1.dropout == s.ToLocation
                         select e1).FirstOrDefault();
                if (e != null)
                {
                    List<string> schedules = new List<string>();
                    List<int> seats = new List<int>();
                    foreach (var r in pr1.seatAvailabilities)
                    {
                        if (r.dateofJourney == s.DateOfJourney && s.Tickets <= r.availableSeats)
                        {
                            schedules.Add(r.scheduleid);
                            seats.Add(int.Parse(r.availableSeats.ToString()));
                        }
                    }
                    foreach (   var r   in pr1.schedules)
                      {
                        if (schedules.Contains(r.SID) && r.RouteId == e.routeid && r.Time==bustime)
                        {
                            var bus = (from e1 in pr1.buses
                                       where e1.busid == r.BusId
                                       select e1).FirstOrDefault();
                            if (bus.busid == busid)
                            {
                                overalldate1 = overalldate1  + r.Time;
                                var x = (from e1 in pr1.seatAvailabilities
                                         where e1.scheduleid == r.SID && e1.dateofJourney == s.DateOfJourney
                                         select e1).FirstOrDefault();
                                x.availableSeats = x.availableSeats - s.Tickets;
                                flag = 1;
                                

                                ticket newTicket = new ticket
                                {
                                    customerid = cid,
                                    ticketid = GenerateTicketNo(busid),
                                    scheduleid = x.scheduleid,
                                    dateofjourney = DateTime.Parse(overalldate1) ,
                                    no_ofseats = s.Tickets   
                                };
                                ticktidvalue = newTicket.ticketid;
                                pr1.tickets.Add(newTicket);
                            }
                        }
                    }
                    if (flag == 1)
                    {
                        history his = new history
                        {
                            cid = cid,
                            ticketid = ticktidvalue,
                            fromaddress = s.FromLocation,
                            toaddress = s.ToLocation,
                            status = "Booked",
                            datetime = DateTime.Parse(overalldate1)
                        };
                        pr1.histories.Add(his);
                        pr1.SaveChanges();
                        return ticktidvalue;
                    }

                }
            }
            return "";
        }
        public string GenerateTicketNo(string busid)
        {

            int currentDay = DateTime.Now.Day;
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;

            Random random = new Random();
            int uniqueNumber = random.Next(1000, 10000); 

            
            string ticketID = $"{busid}{currentDay:D2}{currentMonth:D2}{currentYear % 100:D2}{uniqueNumber:D4}";

            return ticketID;
        }
        public double CancelTicket(string ticketid)
        {
            var ticketDetails = (from e1 in pr.tickets
                                 where e1.ticketid == ticketid
                                 select e1).FirstOrDefault();
            if (ticketDetails != null)
            {
                var availabilityDetails = (from e1 in pr.seatAvailabilities
                                           where e1.scheduleid == ticketDetails.scheduleid &&
                                           e1.dateofJourney == ticketDetails.dateofjourney
                                           select e1).FirstOrDefault();
                availabilityDetails.availableSeats = availabilityDetails.availableSeats + ticketDetails.no_ofseats;
                var route = (from e1 in pr.schedules
                             where e1.SID == ticketDetails.scheduleid
                             select e1).FirstOrDefault();
                var percost = (from e1 in pr.routes
                               where e1.routeid == route.RouteId
                               select e1).FirstOrDefault();

                double amount = ticketDetails.no_ofseats.Value * percost.cost.Value;
                DateTime startDate = DateTime.Now;
                DateTime endDate = ticketDetails.dateofjourney.Value;
                TimeSpan timeDifference = endDate - startDate;
                double totalHours = timeDifference.TotalHours;
                if (totalHours >= 48)
                {
                    amount = amount - amount * 0.1;
                }
                else if (totalHours >= 24 && totalHours < 48)
                {
                    amount = amount - amount * 0.25;
                }
                else if (totalHours >= 12 && totalHours < 24)
                {
                    amount = amount - amount * 0.5;
                }
                else
                {
                    amount = amount - amount * 1;
                }
                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

               
                DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, indianTimeZone);

                // Format the Indian time to include both date and time components
                string formattedIndianTime = indianTime.ToString("yyyy-MM-dd HH:mm:ss");
                history his = new history
                {
                    cid = ticketDetails.customerid,
                    ticketid = ticketid,
                    fromaddress = percost.pickup,
                    toaddress = percost.dropout,
                    status = "Cancelled",
                    datetime = DateTime.Parse(formattedIndianTime)
                };
                pr.histories.Add(his);
                pr.tickets.Remove(ticketDetails);
                pr.SaveChanges();
                return amount;
            }
            return -1;
        }
        public Customer GetCustomerDetails(string customerid)
        {
            var customer = from c in pr.Customers
                           where c.CID == customerid
                           select c;
            Customer cu = customer.FirstOrDefault();
            return cu;
        }
        public string ValidateLogin(string username, string password)
        {
            var e = (from c in pr.Customers
                     where c.Email == username
                     select c).FirstOrDefault();
            if (e != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, e.password))
                {
                    return "Successfully Loggedin";
                }
                else
                {
                    return "Incorrect Password";
                }
            }
            else
            {
                var x1 = (from c in pr.admins
                          where c.username == username
                          select c).FirstOrDefault();
                if (x1 != null)
                {
                    if (x1.password == password)
                    {
                        return "Admin";
                    }
                    return "Incorrect Password";
                }
                return "Invalid User";
            }
        }
        public string FetchByEmail(string email)
        {
            var ex = (from e1 in pr.Customers
                      where e1.Email == email
                      select e1.CID).FirstOrDefault();
            return ex.ToString();

        }
        public string UpdateDetails(Customer cl)
        {
            var p = (from c in pr.Customers
                     where c.CID == cl.CID
                     select c).FirstOrDefault();
            p.Name = cl.Name;
            p.Address = cl.Address;
            p.City = cl.City;
            p.Country = cl.Country;
            p.State = cl.State;
            p.Pincode = cl.Pincode;
            p.Email = cl.Email;
            p.Gender = cl.Gender;
            p.contactno = cl.contactno;
            p.dob = cl.dob;
            p.customertype = cl.customertype;
            pr.SaveChanges();
            return "Updated Successfully";
        }
        public string AddBus(bus busDetails)
        {
            
            while (true)
            {
                Random random = new Random();
                int uniqueNumber = int.Parse(random.Next(10).ToString() + random.Next(10).ToString() + random.Next(10).ToString());
               string busId = $"B{uniqueNumber:D3}";

                var e1 = (from e in pr.buses
                          where e.busid == busId
                          select e).FirstOrDefault();

                if (e1 == null) {
                    busDetails.busid = busId;
                    break;
                }
            }
            try
            {
                pr.buses.Add(busDetails);
                pr.SaveChanges();
                return  "Bus added successfully with BusId : "+ busDetails.busid ;
            }
            catch (DbUpdateException db)
            {
                SqlException e = db.GetBaseException() as SqlException;
                return e.Message;
            }
            
           
        }
        public List<bus> FetchBusDetails()
        {
            return pr.buses.ToList();
        }
        public string deleteBus(string busId)
        {

            try
            {
                var bus = (from b in pr.buses
                           where b.busid == busId
                           select b).FirstOrDefault();
                pr.buses.Remove(bus);
                pr.SaveChanges();
                return "Bus " + busId + " removed successfully";

            }
            catch (DbUpdateException db)
            {
                SqlException e = db.GetBaseException() as SqlException;
                if (e.Message.Contains("FK_schedule_bus"))
                {
                    return "Cannot delete Scheduled Bus";
                }
            }
            return "";


        }
        public bus GetBusDetailsById(string busid)
        {
            var a = (from u in pr.buses
                     where u.busid == busid
                     select u).FirstOrDefault();
            bus b = a;
            return a;
        }
        public string UpdateBusDetails(bus b)
        {
            var a = (from u in pr.buses
                     where u.busid == b.busid
                     select u).FirstOrDefault();
            if (a != null)
            {
                a.busid = b.busid;
                a.busname = b.busname;
                a.bustype = b.bustype;
                a.no_ofseats = b.no_ofseats;
                pr.buses.AddOrUpdate();
                pr.SaveChanges();
                return "" + b.busid + " Updated Successfully";
            }
            else
                return "BusId cannot be changed";
        }
        public string AddRoute(route routeDetails)
        {
            try
            {
                var e1 = (from e in pr.routes
                          where e.pickup == routeDetails.pickup && e.dropout == routeDetails.dropout
                          select e).FirstOrDefault();
                if (e1 == null)
                {
                    while (true)
                    {
                        Random random = new Random();
                        int uniqueNumber = int.Parse(random.Next(10).ToString() + random.Next(10).ToString() + random.Next(10).ToString());
                        string routeId = $"R{uniqueNumber:D3}";

                        var e2 = (from e in pr.routes
                                  where e.routeid == routeId
                                  select e).FirstOrDefault();

                        if (e2 == null)
                        {
                            routeDetails.routeid = routeId;
                            break;
                        }
                    }
                    pr.routes.Add(routeDetails);
                    pr.SaveChanges();
                    return "Route added successfully with RouteId : "+routeDetails.routeid;
                }
                else
                    return "Route already exists";
            }
            catch (DbUpdateException db)
            {
                SqlException e = db.GetBaseException() as SqlException;
                return e.Message;
            }
          
        }
        public List<route> FetchRouteDetails()
        {
            return pr.routes.ToList();
        }
        public string DeleteRoute(string routeId)
        {
            try
            {
                if (routeId == null)
                {
                    return "Please Select the route";
                }
                else
                {
                    var route = (from r in pr.routes
                                 where r.routeid == routeId
                                 select r).FirstOrDefault();
                    pr.routes.Remove(route);
                    pr.SaveChanges();
                    return "Route " + routeId + " removed successfully";
                }
            }
            catch (DbUpdateException db)
            {
                SqlException e = db.GetBaseException() as SqlException;
                if (e.Message.Contains("FK_schedule_schedule"))
                {
                    return "Cannot delete Scheduled route";
                }
            }
            return "";

        }
        public route GetRouteDetailsById(string routeid)
        {
            var route = (from r in pr.routes
                         where r.routeid == routeid
                         select r).FirstOrDefault();
            return route;
        }

        public string UpdateRouteDetails(route routeDetails)
        {
            var route = (from r in pr.routes
                         where r.routeid == routeDetails.routeid
                         select r).FirstOrDefault();

            route.routeid = routeDetails.routeid;
            route.pickup = routeDetails.pickup;
            route.dropout = routeDetails.dropout;
            route.cost = routeDetails.cost;
            pr.routes.AddOrUpdate();
            pr.SaveChanges();
            return "Route " + routeDetails.routeid + " Updated Successfully";
        }
        public string AddSchedule(schedule scheduleDetails)
        {
            var route = (from r in pr.routes
                         where r.routeid == scheduleDetails.RouteId
                         select r).FirstOrDefault();
            var bus = (from b in pr.buses
                       where b.busid == scheduleDetails.BusId
                       select b).FirstOrDefault();
            var time = (from s in pr.schedules
                        where s.BusId == scheduleDetails.BusId && s.Time == scheduleDetails.Time
                        select s).FirstOrDefault();

            try
            {
                if (route != null && bus != null && time == null)
                {

                    while (true)
                    {
                        Random random = new Random();
                        int uniqueNumber = int.Parse(random.Next(10).ToString() + random.Next(10).ToString() + random.Next(10).ToString());
                        string scheduleId = $"S{uniqueNumber:D3}";

                        var e1 = (from e in pr.schedules
                                  where e.SID == scheduleId
                                  select e).FirstOrDefault();

                        if (e1 == null)
                        {
                            scheduleDetails.SID = scheduleId;
                            break;
                        }
                    }

                    pr.schedules.Add(scheduleDetails);
                    pr.SaveChanges();
                    return "Schedule added successfully with ScheduleId : "+scheduleDetails.SID;
                }
                else
                {
                    if (route == null)
                    {
                        return "RouteId doesn't exist";
                    }
                    else if (bus == null)
                    {
                        return "BusId doesn't exist";
                    }
                    else
                    {
                        return "Cannot schedule one bus on the same time ";
                    }
                }
            }

            catch (DbUpdateException db)
            {
                SqlException e = db.GetBaseException() as SqlException;
                if (e.Message.Contains("PK_schedule"))
                {
                    return "scheduled already existed";
                }
            }
            return "";
        }
        public List<schedule> FetchScheduleDetails()
        {
            return pr.schedules.ToList();
        }
        public string DeleteSchedule(string scheduleId)
        {
            try
            {
                var schedule = (from s in pr.schedules
                                where s.SID == scheduleId
                                select s).FirstOrDefault();
                pr.schedules.Remove(schedule);
                pr.SaveChanges();
                return "Schedule " + scheduleId + " removed successfully";
            }
            catch (DbUpdateException db)
            {
                SqlException e = db.GetBaseException() as SqlException;
                if (e.Message.Contains("FK_seatAvailability_schedule"))
                {
                    return "Cannot delete";
                }
            }
            return "";

        }
        public schedule GetScheduleDetailsById(string scheduleid)
        {
            var schedule = (from s in pr.schedules
                            where s.SID == scheduleid
                            select s).FirstOrDefault();
            return schedule;
        }

        public string UpdateScheduleDetails(schedule scheduleDetails)
        {
            try
            {
                var schedule = (from s in pr.schedules
                                where s.SID == scheduleDetails.SID
                                select s).FirstOrDefault();

                schedule.SID = scheduleDetails.SID;
                schedule.RouteId = scheduleDetails.RouteId;
                schedule.BusId = scheduleDetails.BusId;
                schedule.Time = scheduleDetails.Time;
                pr.schedules.AddOrUpdate();
                pr.SaveChanges();
                return "" + scheduleDetails.SID + " Updated Successfully";
            }
            catch (DbUpdateException D)
            {
                SqlException e = D.GetBaseException() as SqlException;
                if (e.Message.Contains("FK_schedule_schedule"))
                {
                    return "Invalid RouteId";
                }
                return "";
            }

        }
        public List<history> GetHistory(String Cid)
        {
            var history = (from e1 in pr.histories
                           where e1.cid == Cid
                           select e1);
            return history.ToList();
        }
        public List<string> fetchrouteid()
        {
            var x = (from e1 in pr.routes
                     select e1.routeid).ToList();
            return x;
        }
        public List<string> fetchbusid()
        {
            var x = (from e1 in pr.buses
                     select e1.busid).ToList();
            return x;
        }
        public List<string> FetchPlaces()
        {
            var x= (from e1 in pr.places
                    select e1.places).ToList();
            return x;
        }
    }
}
