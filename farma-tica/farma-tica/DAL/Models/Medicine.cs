﻿using System;

namespace farma_tica.DAL.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Medicine
    {
        public Guid MedicineId { get; set; }
        public string Name { get; set; }
        public bool RequiresPrescription { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int AmmountSold { get; set; }
    }
}