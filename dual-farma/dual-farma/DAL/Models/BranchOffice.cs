using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dual_farma.DAL.Models
{
    /// <summary>
    /// Represents a branch office with all its atributes
    /// </summary>
    public class BranchOffice
    {
        public Guid BranchOfficeId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string Company { get; set; }
    }
}