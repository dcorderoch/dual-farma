using System;
using System.Drawing;

namespace dual_farma.DAL.Models
{
    public class Prescription
    {
        public Guid PrescriptionID { get; set; }
        public string Doctor { get; set; }
        public Image Image { get; set; } 
    }
}