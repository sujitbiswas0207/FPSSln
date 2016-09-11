using FPS.DataModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FPS.Models
{
   
    public class QuoteViewModel
    {
        public int QuickQuoteType { get; set; }

        [Display(Name = "City or zip / Postal code")]
        [Required(ErrorMessage = "Please enter your start location")]
        public string StartLocation { get; set; }
        
        [Display(Name = "City or zip / Postal code")]
        [Required(ErrorMessage = "Please enter your delivery location")]
        public string EndLocation { get; set; }

        [Required(ErrorMessage = "Please enter your start location type")]
        [Display(Name = "Location Type")]
        public int StartLocationType { get; set; }

        [Required(ErrorMessage = "Please enter your delivery location type")]
        [Display(Name = "Location Type")]
        public int EndLocationType { get; set; }

        public int TruckType { get; set; }

        public List<ItemDetail> ItemDetails { get; set; }

        public string Packaging1 { get; set; }
        public int quantity { get; set; }
        public int Weight1 { get; set; }
        public string WeightUOM1 { get; set; }
        public int Length1 { get; set; }
        public int Width1 { get; set; }
        public int Height1 { get; set; }
        public string DimensionsUOM1 { get; set; }
        public double Frightclass { get; set; }
    }
    
}
