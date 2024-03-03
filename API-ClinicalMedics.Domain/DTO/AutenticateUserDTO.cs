using System.ComponentModel.DataAnnotations;

namespace API_ClinicalMedics.Domain.DTO
{
    public class AutenticateUserDTO
    {
        [Required]
        public string Cpf { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
