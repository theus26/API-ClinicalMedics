using API_ClinicalMedics.Domain.DTO;
using API_ClinicalMedics.Domain.Entities;

namespace API_ClinicalMedics.Domain.Interfaces
{
    public interface IUserService
    {
        public Users EncryptUserData(UserDTO userDTO);
        public ResultAutenticateDTO AutenticateUser(AutenticateUserDTO autenticateUser);
    }
}
