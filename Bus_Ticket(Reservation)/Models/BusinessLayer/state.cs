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
    
    public partial class state
    {
        public string stateid { get; set; }
        public string statename { get; set; }
        public string countryid { get; set; }
    
        public virtual country country { get; set; }
    }
}
