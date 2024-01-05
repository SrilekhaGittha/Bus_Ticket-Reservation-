using Bus_Ticket_Reservation_.Models.DataLayer.validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bus_Ticket_Reservation_.Models.DataLayer
{
    public class sample
    {
        string fromLocation;
        string toLocation;
        DateTime dateOfJourney;
        int tickets;
        [Required(ErrorMessage = "cant be blank")]
        public string FromLocation { get => fromLocation; set => fromLocation = value; }
        [Required(ErrorMessage = "cant be blank")]
        public string ToLocation { get => toLocation; set => toLocation = value; }
        [Required(ErrorMessage = "cant be blank")]
        public DateTime DateOfJourney { get => dateOfJourney; set => dateOfJourney = value; }
        [Required(ErrorMessage = "cant be blank")]
        [customfortickets(ErrorMessage = "Tickets cannot be greater than 4")]
        public int Tickets { get => tickets; set => tickets = value; }

        public sample() { }

        public sample(string fromLocation, string toLocation, DateTime dateOfJourney, int tickets)
        {
            this.fromLocation = fromLocation;
            this.toLocation = toLocation;
            this.dateOfJourney = dateOfJourney;
            this.tickets = tickets;
        }

        
       
    }
}