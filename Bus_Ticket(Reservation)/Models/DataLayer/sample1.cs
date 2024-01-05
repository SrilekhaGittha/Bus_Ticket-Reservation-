using Bus_Ticket_Reservation_.Models.DataLayer.validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bus_Ticket_Reservation_.Models.DataLayer
{
    public class sample1
    {
        string fromLocation;
        string toLocation;
        DateTime dateOfJourney;
  
        [Required(ErrorMessage = "cant be blank")]
        public string FromLocation { get => fromLocation; set => fromLocation = value; }
        [Required(ErrorMessage = "cant be blank")]
        public string ToLocation { get => toLocation; set => toLocation = value; }
        [Required(ErrorMessage = "cant be blank")]
        public DateTime DateOfJourney { get => dateOfJourney; set => dateOfJourney = value; }
      

        public sample1() { }

        public sample1(string fromLocation, string toLocation, DateTime dateOfJourney)
        {
            this.fromLocation = fromLocation;
            this.toLocation = toLocation;
            this.dateOfJourney = dateOfJourney;
           
        }

    }
}