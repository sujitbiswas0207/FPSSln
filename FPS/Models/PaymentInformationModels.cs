using FPS.DataModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FPS.Models
{
    public partial class PaymentInformationModel
    {
        public PaymentsDetail PaymentDetail { get; set; }
        public AspNetUser AspNetUserDetail { get; set; }        
    }
}
