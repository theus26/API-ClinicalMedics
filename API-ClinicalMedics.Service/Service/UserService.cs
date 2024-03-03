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
            string role = SetRoleUser(userDTO.Role);
            var hashPassword = CreateHashMd5(userDTO.Password);
            userDTO.Password = hashPassword;
            userDTO.Role = role;
            userDTO.CPF.Replace(".", "").Replace("-", "");
            var user = mapper.Map<Users>(userDTO);
            return user;
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

        private static string SetRoleUser(string role)
        {
            if (role is null)
            {
                return "employee";
            }
            return "manager";
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
    }
}
