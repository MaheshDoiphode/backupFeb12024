using System.ComponentModel.DataAnnotations;

namespace VMS2._0.DTO
{
    public class SecondaryInfoDTO
    {
        public string SecondaryInfoID { get; set; }

        [Required]
        public string VisitorID { get; set; }

        [EmailAddress]
        public string AlternateEmail { get; set; }

        public string AlternateContact { get; set; }

        public string AlternateEmergencyContact { get; set; }
    }

}
