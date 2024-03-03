using API_ClinicalMedics.Domain.DTO;
using API_ClinicalMedics.Domain.Entities;

namespace API_ClinicalMedics.Domain.Interfaces
{
    public interface IAttachamentService
    {
        public Attachaments AttachamentsExam(AttachamentDTO attachamentDto);
        public IEnumerable<ResultAttachamentDTO> GetAttachamentByIdUser(int idUser);
    }
}
