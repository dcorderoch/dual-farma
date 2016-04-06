using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace farma_tica.Controllers
{
    public class OrderWithoutPrescription
    {
        /*
        string clientId, string[] medicinesId, string pickupOfficeId,
            string prefPhoneNume, DateTime pickUpdDate, bool type
        */
        public string clientId { get; set; }
        public string[] medicineIds { get; set; }
        public string pickupOfficeId { get; set; }
        public string prefPhoneNumber { get; set; }
        public DateTime pickupDate { get; set; }
        public bool type { get; set; }
    }
}