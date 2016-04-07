using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace farma_tica.Controllers.Models
{
    public class OrderWithPrescription
    {
        public string clientId { get; set; }
        public string[] medicinesId { get; set; }
        public string[] prescriptedMedicinesId { get; set; }
        public Image prescriptionImage { get; set; }
        public string DoctorId { get; set; }
        public string pickupOfficeId { get; set; }
        public string prefPhoneNumber { get; set; }
        public DateTime pickupDate { get; set; }
        public bool type { get; set; }
    }
}