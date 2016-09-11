using FPS.DataModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FPS.Models
{
    public partial class PickupDropoffModel
    {
        public ItemPickupDetail PickupDetail { get; set; }
        public ItemDropoffDetail DropoffDetail { get; set; }        
    }
}
