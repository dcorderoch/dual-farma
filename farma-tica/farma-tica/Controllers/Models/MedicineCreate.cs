using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace farma_tica.Controllers.Models
{
    public class MedicineCreate
    {
        public string name { get; set; }
        public string requiresPrescription { get; set; }
        public string price { get; set; }
        public string originOffice { get; set; }
        public string stock { get; set; }
    }
}