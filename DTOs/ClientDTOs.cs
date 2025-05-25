using System.ComponentModel.DataAnnotations;

namespace jycbackend.DTOs
{
    public class ClientCreateDto
    {
        public string ClientDoc { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; } 

        public string Address { get; set; } = string.Empty; 
    }

    public class ClientGetDto
    {
        public Guid Id { get; set; }
        public string ClientDoc { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string Address { get; set; } = string.Empty; 
        public DateTime CreatedAt { get; set; }
    }

    public class ClientUpdateDto
    {

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
    }

    public class ClientDto
    {
        public Guid Id { get; set; }
        public string ClientDoc { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string Address { get; set; } = string.Empty; 
    }

}
