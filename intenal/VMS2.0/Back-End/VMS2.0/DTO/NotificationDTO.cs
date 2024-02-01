using System.ComponentModel.DataAnnotations;

namespace VMS2._0.DTO
{
    public class NotificationDTO
    {
        public string NotificationID { get; set; }

        [Required]
        public string VisitID { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string NotificationType { get; set; }

        [Required]
        public DateTime NotificationGenerated { get; set; }

        [Required]
        public string NotificationStatus { get; set; }
    }
}
