using API_ClinicalMedics.Domain.DTO;
using API_ClinicalMedics.Domain.Entities;
using API_ClinicalMedics.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace API_ClinicalMedics.Service.Service
{
    public class AttachamentService(IBaseRepository<Attachaments> baseRepository, IMapper _mapper) : IAttachamentService
    {
        public Attachaments SaveAttachamentsExam(AttachamentDTO attachamentDto)
        {
            string base64 = ConvertFileToBase64(attachamentDto.File);

            var attachaments = new Attachaments
            {
                ContentPDF = base64,
                FileName = attachamentDto.File.FileName.Replace(Path.GetExtension(attachamentDto.File.FileName), ""),
                IdUser = attachamentDto.IdUser,
                TypeDocument = Path.GetExtension(attachamentDto.File.FileName)
            };
            return attachaments;
        }

        public IEnumerable<UserAttachmentsDTO> GetAttachamentByIdUser(int idUser)
        {
            var attachamentFromUser = GetAttachamentFromUser(idUser);

            if (attachamentFromUser.Any())
            {
                return _mapper.Map<IEnumerable<UserAttachmentsDTO>>(attachamentFromUser);
            }

            throw new ArgumentNullException($"Attachaments don´t find for user {idUser}");
        }

        private IQueryable<Attachaments?> GetAttachamentFromUser(int idUser)
        {
            return baseRepository.Select().Where(x => x.IdUser == idUser).AsQueryable();
        }
        private static string ConvertFileToBase64(IFormFile file)
        {
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            var fileByte = ms.ToArray();
            string base64 = Convert.ToBase64String(fileByte);
            return base64;
        }
    }
}
