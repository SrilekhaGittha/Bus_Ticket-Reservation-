//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bus_Ticket_Reservation_.Models.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class ticket
    {
        public string customerid { get; set; }
        public string ticketid { get; set; }
        public string scheduleid { get; set; }
        public Nullable<System.DateTime> dateofjourney { get; set; }
        public Nullable<int> no_ofseats { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}
