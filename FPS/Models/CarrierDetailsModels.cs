using FPS.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FPS.Models
{   
    public class CarrierDetails
    {
        public int Id { get; set; }
        public string CarriersDetails { get; set; }
        public Nullable<int> TransitTime { get; set; }
        public Nullable<double> DiscountedPrice { get; set; }
        public string CarriersLogo { get; set; }
        public Nullable<System.DateTime> CreatedDAte { get; set; }
        public string QuoteID { get; set; }
    }    
}
