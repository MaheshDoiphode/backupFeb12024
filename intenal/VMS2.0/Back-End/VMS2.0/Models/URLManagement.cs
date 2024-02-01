using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VMS2._0.Models
{
    public class URLManagement
    {
        [Key]
        public string URLID { get; set; }

        [ForeignKey("VisitID")]
        public string VisitID { get; set; }

        [Required]
        public DateTime ExpirationTime { get; set; }

        [Required]
        public DateTime GenerateDate { get; set; }

        [Required]
        public string URLStatus { get; set; }

        [Required]
        public string URLType { get; set; }

        public Visit Visit { get; set; }
    }
}
