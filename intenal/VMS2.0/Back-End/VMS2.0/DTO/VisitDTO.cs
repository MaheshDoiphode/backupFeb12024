using System.ComponentModel.DataAnnotations;

namespace VMS2._0.DTO
{
    public class VisitDTO
    {
        public string VisitID { get; set; }

        public string? ParentVisitID { get; set; }

        [Required]
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
    }
}
