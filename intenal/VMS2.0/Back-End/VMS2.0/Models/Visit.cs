using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VMS2._0.Models
{
    public class Visit
    {
        [Key]
        public string VisitID { get; set; }

        public string? ParentVisitID { get; set; }

        [ForeignKey("VisitorID")]
        public string VisitorID { get; set; }

        [Required]
        public string HostName { get; set; }

        [Required]
        [EmailAddress]
        public string HostEmail { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public DateTime ExpectedArrival { get; set; }

        [Required]
        public DateTime ExpectedDepart { get; set; }

        public DateTime? CheckIn { get; set; }

        public DateTime? CheckOut { get; set; }

        [Required]
        public string RequestStatus { get; set; }

        public string? Feedback { get; set; }

        [Required]
        public string VisitStatus { get; set; }

        public Visitor Visitor { get; set; }
    }
}
