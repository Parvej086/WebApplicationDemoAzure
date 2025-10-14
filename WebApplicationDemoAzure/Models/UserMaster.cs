using System.ComponentModel.DataAnnotations;

namespace WebApplicationDemoAzure.Models
{
    public class UserMaster
    {
        public int Id { get; set; }
        [Required]
        public string Email_Id { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Role { get; set; }
    }
}
