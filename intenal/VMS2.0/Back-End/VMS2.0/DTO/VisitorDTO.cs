using System.ComponentModel.DataAnnotations;

namespace VMS2._0.DTO
{
    public class VisitorDTO
    {
        public string? VisitorID { get; set; }

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
    }
}
