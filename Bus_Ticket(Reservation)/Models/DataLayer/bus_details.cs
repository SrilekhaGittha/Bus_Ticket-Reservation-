using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bus_Ticket_Reservation_.Models.DataLayer
{
    public class bus_details
    {
        string busid;
        string busname;
        string bustype;
        int availableSeats;
        string time;
        int cost;
        public bus_details() { }

        public bus_details(string busname,string busid, string bustype, int availableSeats, string time, int cost)
        {
            this.busid=busid;
            this.busname = busname;
            this.bustype = bustype;
            this.availableSeats = availableSeats;
            this.time = time;
            this.cost = cost;
        }

        public string Busid { get => busid; set => busid = value; }
        public string Busname { get => busname; set => busname = value; }
        public string Bustype { get => bustype; set => bustype = value; }
        public int AvailableSeats { get => availableSeats; set => availableSeats = value; }
        public string Time { get => time; set => time = value; }
       
        public int Cost { get => cost; set => cost = value; }
    }
}