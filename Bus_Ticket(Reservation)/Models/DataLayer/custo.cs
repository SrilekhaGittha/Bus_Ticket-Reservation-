using Bus_Ticket_Reservation_.Models.BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bus_Ticket_Reservation_.Models.DataLayer
{
    public class custo
    {
        

        public string CID { get; set; }
        [Required(ErrorMessage = "cant be blank")]
        [RegularExpression(pattern: "^[a-zA-Z\\s]+$", ErrorMessage = "Must contain Only Alphabets and Spaces")]
        public string Name { get; set; }
        [Required(ErrorMessage = "cant be blank")]
        public string Address { get; set; }
        [Required(ErrorMessage = "cant be blank")]
        public string City { get; set; }
        [Required(ErrorMessage = "cant be blank")]
        public string State { get; set; }
        [Required(ErrorMessage = "cant be blank")]
        public string Country { get; set; }
        [Required(ErrorMessage = "cant be blank")]
        [RegularExpression(pattern: "^[0-9]{6}$", ErrorMessage = "Invalid Pincode")]

        public string Pincode { get; set; }
        [Required(ErrorMessage = "cant be blank")]
        [RegularExpression(pattern: "^[a-zA-Z0-9.]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "cant be blank")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "cant be blank")]
        [RegularExpression(pattern: "^[0-9]{10}$", ErrorMessage = "PhoneNumber must contain 10 digits")]
        public string contactno { get; set; }
        [Required(ErrorMessage = "cant be blank")]
        [customvalidation(ErrorMessage = "Age must be in between 18 to 80")]
        public Nullable<System.DateTime> dob { get; set; }
        [Required(ErrorMessage = "cant be blank")]
        public string customertype { get; set; } 
        
    }
}