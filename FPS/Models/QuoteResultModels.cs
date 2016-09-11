using FPS.DataModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FPS.Models
{   
    public class QuoteResultModels
    {
        public string startLocation { get; set; }
        public string endLocation { get; set; }
        public string itemInfo { get; set; }
        public string includedstart { get; set; }
        public string includedend { get; set; }
        public string CarriersDetailsID { get; set; }
        public string QuoteID { get; set; }
        public List<CarriersDetail> CarriersDetails { get; set; }
    }    
}
