﻿using System;
using System.Data.SqlTypes;
using System.Drawing;

namespace farma_tica.DAL.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string ClientId { get; set; }
        public Guid? PrescriptionId { get; set; }
        public Guid PickUpOffice { get; set; }
        public Image InvoiceImage { get; set; }
        public bool HasPrescription { get; set; }
        public int State { get; set; }
        public string Priority { get; set; }
        public string PrefPhoneNum { get; set; }
        public string PickUpdDate { get; set; }
        public Boolean Type { get; set; }
    }
}