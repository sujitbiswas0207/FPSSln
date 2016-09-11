using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPSEntity
{
    public class QuickQuoteDetails:BaseEntity<Int32>
    {
        public int QuickQuoteType { get; set; }

        public string StartLocation { get; set; }

        public string EndLocation { get; set; }

        public int StartLocationType { get; set; }

        public int EndLocationType { get; set; }

        public int TruckType { get; set; }

        public DateTime PickupTime { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UserName { get; set; }
        public string UserId { get; set; }
    }

    public class ItemDetails : BaseEntity<Int32>
    {
        public int QuoteID { get; set; }
        public string ItemName { get; set; }
        public Int32 PackgingID { get; set; }
        public Int32? Quantity { get; set; }

        public Int32? Total { get; set; }
        public string WeightType { get; set; }

        public Int32? Dimention_Length { get; set; }
        public Int32? Dimention_Width { get; set; }

        public Int32? Dimention_Height { get; set; }
        public string HightUnit { get; set; }
        public double FrightClass { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; } 
        public string UserId { get; set; }

    }
}
