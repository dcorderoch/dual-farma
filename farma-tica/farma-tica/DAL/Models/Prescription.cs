using System;
using System.Drawing;

namespace farma_tica.DAL.Models
{
    public class Prescription
    {
        public Guid PrescriptionID { get; set; }
        public string Doctor { get; set; }
        public Image Image { get; set; } 
    }
}