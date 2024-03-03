using API_ClinicalMedics.Domain.DTO;
using API_ClinicalMedics.Domain.Entities;
using API_ClinicalMedics.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace API_ClinicalMedics.Service.Service
{
    public class AttachamentService(IBaseRepository<Attachaments> baseRepository) : IAttachamentService
    {
        public Attachaments AttachamentsExam(AttachamentDTO attachamentDto)
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

        public IEnumerable<ResultAttachamentDTO> GetAttachamentByIdUser(int idUser)
        {
            var ListAttachaments = new List<ResultAttachamentDTO>();
            var attachamentFromUser = GetAttachamentFromUser(idUser);

            if (attachamentFromUser is not null)
            {
                foreach (var att in attachamentFromUser)
                {
                    var resultAttachamentFound = new ResultAttachamentDTO
                    {
                        FileName = att?.FileName,
                        ContentPdf = att?.ContentPDF
                    };
                    ListAttachaments.Add(resultAttachamentFound);
                }

                return ListAttachaments;
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
