using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bus_Ticket_Reservation_.Models.BusinessLayer
{
    public class Login
    {

        string Username;
        string Password;
        public Login()
        {
        }
        public Login(string username, string password)
        {
           this.Username = username;
           this.Password = password;
        }

        public string Username1 { get => Username; set => Username = value; }
        public string Password1 { get => Password; set => Password = value; }
    }
}