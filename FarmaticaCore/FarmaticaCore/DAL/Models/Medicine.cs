﻿using System;

namespace FarmaticaCore.DAL.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Medicine
    {
        public Guid MedicineId { get; set; }
        public string Name { get; set; }
        public bool RequiresPrescription { get; set; }
        public  int Price   { get; set; }
        public int OriginOffice { get; set; }
        public string House { get; set; }
        public int Stock { get; set; }
        public int NumberSold { get; set; }
    }
}