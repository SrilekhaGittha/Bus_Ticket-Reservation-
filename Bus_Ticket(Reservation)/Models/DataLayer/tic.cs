using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bus_Ticket_Reservation_.Models.DataLayer
{
    public class tic
    {
        string cid;
        string tid;
        string sid;
        DateTime date;

        public string Cid { get => cid; set => cid = value; }
        public string Tid { get => tid; set => tid = value; }
        public string Sid { get => sid; set => sid = value; }
        public DateTime Date { get => date; set => date = value; }

        public tic(string cid, string tid, string sid, DateTime date)
        {
            this.cid = cid;
            this.tid = tid;
            this.sid = sid;
            this.date = date;
        }

        public  tic()
        {
        }
    }
}