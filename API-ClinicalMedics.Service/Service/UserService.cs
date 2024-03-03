using API_ClinicalMedics.Domain.DTO;
using API_ClinicalMedics.Domain.Entities;
using API_ClinicalMedics.Domain.Interfaces;
using AutoMapper;
using System.Security.Cryptography;
using System.Text;

namespace API_ClinicalMedics.Service.Service
{
    public class UserService(IBaseRepository<Users> baseRepository, IMapper mapper) : IUserService
    {
        public Users EncryptUserData(UserDTO userDTO)
        {
            userDTO.CPF = CleanCpf(userDTO.CPF);
            bool haveCpf = ValidateExistenceOfCpf(userDTO.CPF);

            if (!haveCpf)
            {
                userDTO.Role = SetRoleUser(userDTO.Role);
                userDTO.Password = CreateHashMd5(userDTO.Password);
                var user = mapper.Map<Users>(userDTO);
                return user;
            }
            throw new ArgumentNullException($"{userDTO.CPF} already exist");
        }
        
        public ResultAutenticateDTO AutenticateUser(AutenticateUserDTO autenticateUser)
        {
            if (string.IsNullOrEmpty(autenticateUser.Cpf) || string.IsNullOrEmpty(autenticateUser.Password))
            {
                throw new ArgumentNullException();
            }

            string hashedPassword = CreateHashMd5(autenticateUser.Password);

            if (string.IsNullOrEmpty(hashedPassword))
            {
                throw new ArgumentNullException();
            }

            var user = baseRepository.Select().FirstOrDefault(x => x.CPF == autenticateUser.Cpf && x.Password == hashedPassword);

            if (user is null)
            {
                throw new ArgumentNullException();
            }

            var token = TokenService.CreateToken(user);

            return new ResultAutenticateDTO()
            {
                Role = user.Role,
                IdUser = user.Id,
                NameUser = user.Name,
                Token = token
            };
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
        private static string CleanCpf(string cpf)
        {
            return cpf.Replace(".", "").Replace("-", "");
        }
        private bool ValidateExistenceOfCpf(string cpf)
        {
            return baseRepository.Select().Any(x => x.CPF == cpf);

        }
        private static string SetRoleUser(string? role)
        {
            return role is null ? "user" : "manager";
        }
    }
}
