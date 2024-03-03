using API_ClinicalMedics.Domain.DTO;
using API_ClinicalMedics.Domain.Entities;
using API_ClinicalMedics.Domain.Interfaces;
using API_ClinicalMedics.Service.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_ClinicalMedics.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController(IBaseService<Users> baseService, IUserService userService)
        : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult HealthCheck()
        {
            return Ok("I'm alive and working");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateUser(UserDTO userDTO)
        {
            try
            {
                var user = userService.EncryptUserData(userDTO);
                var insertUser = baseService.Add<UserValidator>(user);
                return Ok(insertUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed, new ResponseDTO()
                {
                    Status = StatusCodes.Status412PreconditionFailed,
                    Message = ex.Message
                });
            }

        }

        [HttpGet]
        [Authorize(Roles="manager")]
        public IActionResult GetAllPatientes()
        {
            try
            {
                var allPatientes = baseService.Get();
                return Ok(allPatientes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed, new ResponseDTO()
                {
                    Status = StatusCodes.Status412PreconditionFailed,
                    Message = ex.Message
                });
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetPatientById(int idUser)
        {
            try
            {
                var patientFoud = baseService.GetById(idUser);
                return Ok(patientFoud);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed, new ResponseDTO()
                {
                    Status = StatusCodes.Status412PreconditionFailed,
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult AutenticateUser(AutenticateUserDTO autenticateUser)
        {
            try
            {
                var autentication = userService.AutenticateUser(autenticateUser);
                return Ok(autentication);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
