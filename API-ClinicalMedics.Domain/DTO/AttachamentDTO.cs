﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace API_ClinicalMedics.Domain.DTO
{
    public class AttachamentDTO
    {
        [Required]
        public int IdUser { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
