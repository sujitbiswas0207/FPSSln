using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS.Entity
{
    public class QuickQuoteDetail : BaseEntity<Int32>
    {
        public int Id { get; set; }

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
    }
}
