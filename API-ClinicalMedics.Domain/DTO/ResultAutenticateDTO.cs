namespace API_ClinicalMedics.Domain.DTO
{
    public class ResultAutenticateDTO
    {
        public string? Token { get; set; }
        public string? Role { get; set; }
        public string? NameUser { get; set; }
        public int? IdUser { get; set; }
    }
}
