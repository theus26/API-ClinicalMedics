using API_ClinicalMedics.Domain.DTO;
using API_ClinicalMedics.Domain.Entities;
using API_ClinicalMedics.Domain.Interfaces;
using API_ClinicalMedics.Service.Validators;
using Microsoft.AspNetCore.Mvc;

namespace API_ClinicalMedics.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private IBaseService<Users> _baseUserService;

        public UserController(IBaseService<Users> baseUserService)
        {
            _baseUserService = baseUserService;
        }

        [HttpGet]
        public ActionResult HealthCheck()
        {
            return Ok("I'm alive and working");
        }

        [HttpPost]
        public IActionResult CreateUser(UserDTO userDTO)
        {
            try
            {
                var user = _baseUserService.EncryptUserData(userDTO);
                var insertUser = _baseUserService.Add<UserValidator>(user);
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
        public IActionResult GetAllPacientes()
        {
            try
            {
                var getAllPacientes = _baseUserService.Get();
                return Ok(getAllPacientes);
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

        [HttpGet("{id:int}")]
        public IActionResult GetPatientById(int id)
        {
            try
            {
                var getPacientById = _baseUserService.GetById(id);
                return Ok(getPacientById);

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
    }
}
