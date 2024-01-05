using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bus_Ticket_Reservation_.Models.BusinessLayer
{
    public class customvalidation:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dateValue)
            {
                // Calculate the date 18 years ago from now
                DateTime minDate = DateTime.Today.AddYears(-18);

                // Calculate the date 80 years ago from now
                DateTime maxDate = DateTime.Today.AddYears(-80);

                // Check if the date is between the min and max dates
                return dateValue >= maxDate && dateValue <= minDate;
            }

            return false;
        }
    }
}