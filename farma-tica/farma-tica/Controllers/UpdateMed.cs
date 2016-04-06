using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace farma_tica.Controllers
{
    public class UpdateMed
    {
        //string medicineId, string price, string branchOffice, string stock, string numberSold)
        public string medID { get; set; }
        public string price { get; set; }
        public string branchOffice { get; set; }
        public string stock { get; set; }
        public string NoSold { get; set; }
    }
}