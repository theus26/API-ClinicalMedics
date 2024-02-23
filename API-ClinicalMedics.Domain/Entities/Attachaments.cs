using System.ComponentModel.DataAnnotations;

namespace API_ClinicalMedics.Domain.Entities
{
    public class Attachaments : BaseEntity
    {
        [Required]
        public int IdUser { get; set; }
        [Required]
        public string TypeDocument { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required] 
        public string ContentPDF { get; set; }

    }
}
