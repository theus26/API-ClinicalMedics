﻿using System.ComponentModel.DataAnnotations;

namespace API_ClinicalMedics.Domain.Entities
{
    public class Users : BaseEntity
    { 
        [Required]
        public string Name { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
