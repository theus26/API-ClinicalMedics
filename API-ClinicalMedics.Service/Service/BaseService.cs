using System.Security.Cryptography;
using System.Text;
using API_ClinicalMedics.Domain.DTO;
using API_ClinicalMedics.Domain.Entities;
using API_ClinicalMedics.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace API_ClinicalMedics.Service.Service
{
    public class BaseService<TEntity>(IBaseRepository<TEntity> baseRepository, IMapper mapper) : IBaseService<TEntity>
        where TEntity : BaseEntity
    {
        public TEntity Add<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            
            Validate(obj, Activator.CreateInstance<TValidator>());
            baseRepository.Insert(obj);
            return obj;
        }

        public Attachaments AttachamentsExam(AttachamentDTO attachamentDto)
        {
            string base64 = ConvertFileToBase64(attachamentDto.File);

            var attachaments = new Attachaments()
            {
                ContentPDF = base64,
                FileName = Path.GetExtension(attachamentDto.File.FileName),
                IdUser = attachamentDto.IdUser,
                TypeDocument = attachamentDto.DocumentType
            };
            return attachaments;
        }

        public TEntity Update<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            baseRepository.Update(obj);
            return obj;
        }
        public Users EncryptUserData(UserDTO userDTO)
        {
            var hashPassword = CreateHashMd5(userDTO.Password);
            userDTO.Password = hashPassword;
            var user = mapper.Map<Users>(userDTO);
            return user;
        }

        public void Delete(int id) => baseRepository.Delete(id);

        public IList<TEntity> Get() => baseRepository.Select();

        public TEntity GetById(int id) => baseRepository.Select(id);

        private static void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }

        private static string CreateHashMd5(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Convert string to bytes
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create an StringBuilder
            StringBuilder sBuilder = new StringBuilder();

            // Format every byte as an hex
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private string ConvertFileToBase64(IFormFile file)
        {
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            var fileByte = ms.ToArray();
            string base64 = Convert.ToBase64String(fileByte);
            return base64;
        }
    }
}

