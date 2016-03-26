using System;

namespace FarmaticaCore.DAL.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string ClientId { get; set; }
        public int? PrescriptionId { get; set; }
        public Guid MedicationId { get; set; }
        public int PickUpOffice { get; set; }
        public Guid InvoiceCode { get; set; }
        public bool HasPrescription { get; set; }
        public int State { get; set; }
        public string Priority { get; set; }
        public string PrefPhoneNum { get; set; }
        public DateTime PickUpdDate { get; set; } 
    }
}