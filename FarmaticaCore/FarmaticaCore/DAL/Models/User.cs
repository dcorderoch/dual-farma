﻿namespace FarmaticaCore.DAL.Models
{
    /// <summary>
    /// POCO class to represent a user in the database
    /// </summary>
    public class User
    {
        public string IdUsuario { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
    }
}