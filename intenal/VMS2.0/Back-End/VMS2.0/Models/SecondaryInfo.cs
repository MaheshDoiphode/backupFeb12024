using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VMS2._0.Models
{
    public class SecondaryInfo
    {
        [Key]
        public string SecondaryInfoID { get; set; }

        [ForeignKey("VisitorID")]
        public string VisitorID { get; set; }

        [EmailAddress]
        public string AlternateEmail { get; set; }

        public string AlternateContact { get; set; }

        public string AlternateEmergencyContact { get; set; }

        public Visitor Visitor { get; set; }
    }
}
