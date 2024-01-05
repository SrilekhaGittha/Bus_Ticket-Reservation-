using Bus_Ticket_Reservation_.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bus_Ticket_Reservation_.Models.BusinessLayer
{
    public class customer_bl
    {
        Dboperations db = new Dboperations();
        public string Register(Customer c)
        {
            return db.Register(c);
        }
        public List<country> FetchCountry()
        {
            return db.FetchCountry();
        }
        public List<state> FetchStates(string id)
        {
            return db.FetchStates(id);
        }
        public List<bus_details> FetchDetails(sample s)
        {
            return db.FetchDetails(s);
        }
        public List<bus_details> FetchDetails1(sample1 s)
        {
            return db.FetchDetails1(s);
        }
        public string FetchByBusId(string busid)
        {
            return db.FetchByBusId(busid);  
        }
        public string TicketStatus(string cid,string id,sample s,string time)
        {
            return db.TicketStatus(cid,id,s,time);
        }
        public string GenerateTicketNo(string id) 
        {
            return db.GenerateTicketNo(id);
        }
        public double CancelTicket(string ticketid)
        {
           
            return db.CancelTicket(ticketid);
        }
        public Customer CustomerDetails(string customerid)
        {
            return db.GetCustomerDetails(customerid);
        }
        public string ValidateLogin(string username, string password)
        {
            return db.ValidateLogin(username, password);
        }
        public string FetchByEmail(string email)
        {
            return db.FetchByEmail(email);  
        }
        public string UpdateDetails(Customer cl)
        {
            return db.UpdateDetails(cl);    
        }
        public List<history> GetHistory(String Cid)
        {

            return db.GetHistory(Cid);
        }
        public List<string> fetchrouteid()
        {
            return db.fetchrouteid();
        }
        public List<string> FetchPlaces()
        {
            return db.FetchPlaces();
        }
    }
}