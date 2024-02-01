using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VMS2._0.Models
{
    public class Visitor
    {
        [Key]
        public string? VisitorID { get; set; }

        [Required]
        public string? VisitorName { get; set; }

        [Required]
        [EmailAddress]
        public string VisitorEmail { get; set; }

        [Required]
        public string VisitorNumber { get; set; }

        public string? VisitorAddress { get; set; }
        
        public string? IdentityType { get; set; }

        public string? IdentityNumber { get; set; }

        public byte[]? Image { get; set; }

        public ICollection<Visit> Visits { get; set; }
    }
}
