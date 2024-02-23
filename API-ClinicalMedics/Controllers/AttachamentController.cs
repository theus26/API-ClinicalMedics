using API_ClinicalMedics.Domain.DTO;
using API_ClinicalMedics.Domain.Entities;
using API_ClinicalMedics.Domain.Interfaces;
using API_ClinicalMedics.Service.Validators;
using Microsoft.AspNetCore.Mvc;

namespace API_ClinicalMedics.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AttachamentController : ControllerBase
    {
        private IBaseService<Attachaments> _baseAttachamentService;

        public AttachamentController(IBaseService<Attachaments> baseAttachamentService)
        {
            _baseAttachamentService = baseAttachamentService;
        }

        [HttpGet]
        public ActionResult HealthCheck()
        {
            return Ok("I'm alive and working");
        }

        [HttpGet]
        public IActionResult GetAnexosById(int id)
        {
            try
            {
                var getAnexoById = _baseAttachamentService.GetById(id);
                return Ok(getAnexoById);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                });
            }
        }

        [HttpGet]
        public IActionResult GetAllAnexo()
        {
            try
            {
                var getPacientes = _baseAttachamentService.Get();
                return Ok(getPacientes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                });
            }
        }

        [HttpPost]
        public IActionResult AddAttachment([FromForm] AttachamentDTO attachmentDTO)
        {
            try
            {
                var attachament = _baseAttachamentService.AttachamentsExam(attachmentDTO);
                var insertAttachament = _baseAttachamentService.Add<AttachamentValidator>(attachament);
                return Ok(insertAttachament);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                });
            }

        }
    }
}
