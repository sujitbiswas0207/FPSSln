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
    
    public partial class QuickQuoteDetail
    {
        public int Id { get; set; }
        public int QuickQuoteType { get; set; }
        public int StartLocationType { get; set; }
        public int EndLocationType { get; set; }
        public int TruckType { get; set; }
        public System.DateTime PickupTime { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string StartLocationExtraService { get; set; }
        public string EndLocationExtraService { get; set; }
        public Nullable<int> CarrierID { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}