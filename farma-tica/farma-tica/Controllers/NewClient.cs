using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace farma_tica.Controllers
{
    public class NewClient
    {
        public string NumCed { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public int PenaltiesNumber { get; set; }
        public string PlaceResidence { get; set; }
        public string MedicalHistory { get; set; }
        public string BornDate { get; set; }
        public string PhoneMum { get; set; }
    }
}