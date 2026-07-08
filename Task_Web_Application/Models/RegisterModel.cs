using System.ComponentModel.DataAnnotations;

namespace Task_Web_Application.Models
{
    public class RegisterModel
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [MinLength(3, ErrorMessage = "Username must contain at least 3 characters")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must contain at least 6 characters")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime Dob { get; set; }

        [Required(ErrorMessage = "Account type is required")]
        public string Type { get; set; } = string.Empty;

        public bool AcceptTerms { get; set; }
    }
}