using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bus_Ticket_Reservation_.Models.DataLayer.validations
{
    public class customfortickets:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if(value is int tickets) 
            { 
                if(tickets>4 || tickets<=0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
    }
}