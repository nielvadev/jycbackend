using System.ComponentModel.DataAnnotations;

namespace jycbackend.Models
{
    public class Client
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string ClientDoc { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? Phone { get; set; }

        [Required]
        public string Address { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
