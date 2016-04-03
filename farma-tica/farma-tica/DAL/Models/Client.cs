﻿using System;

namespace farma_tica.DAL.Models
{
    /// <summary>
    /// Client POCO 
    /// </summary>
    public class Client
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public int PenaltiesNumber { get; set; }
        public string PlaceResidence { get; set; }
        public string MedicalHistory { get; set; }
        public DateTime BornDate { get; set; }
        public string PhoneMum { get; set; }
    }
}