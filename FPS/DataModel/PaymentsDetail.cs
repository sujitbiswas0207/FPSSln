//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FPS.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class PaymentsDetail
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string ExDate_Month { get; set; }
        public string ExDate_Year { get; set; }
        public Nullable<System.DateTime> PaymentDAte { get; set; }
        public string CVVCode { get; set; }
        public Nullable<bool> IsStoredCard { get; set; }
        public string CouponCode { get; set; }
        public string QuoteID { get; set; }
        public string CardType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string PaymentID { get; set; }
    }
}
