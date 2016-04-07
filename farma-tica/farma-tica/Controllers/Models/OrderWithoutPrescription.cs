using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace farma_tica.Controllers.Models
{
    public class OrderWithoutPrescription
    {
        public string clientId { get; set; }
        public string[] medicineIds { get; set; }
        public string pickupOfficeId { get; set; }
        public string prefPhoneNumber { get; set; }
        public DateTime pickupDate { get; set; }
        public bool type { get; set; }
    }
}