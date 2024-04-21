using API_ClinicalMedics.Domain.DTO;
using API_ClinicalMedics.Domain.Entities;

namespace API_ClinicalMedics.Domain.Interfaces
{
    public interface IAttachamentService
    {
        public Attachaments SaveAttachamentsExam(AttachamentDTO attachamentDto);
        public IEnumerable<UserAttachmentsDTO> GetAttachamentByIdUser(int idUser);
    }
}
