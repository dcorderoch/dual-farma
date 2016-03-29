using System;
using System.Drawing;

namespace FarmaticaCore.DAL.Models
{
    public class Prescription
    {
        public Guid PrescriptionID { get; set; }
        public string Doctor { get; set; }
        public Image Image { get; set; } 
    }
}