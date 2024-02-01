using System.ComponentModel.DataAnnotations;

namespace VMS2._0.DTO
{
    public class UserDTO
    {
        public string UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
    }
}
