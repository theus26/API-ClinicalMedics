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
    public class AttachamentController(
        IBaseService<Attachaments> baseService,
        IAttachamentService attachamentService)
        : ControllerBase
    {

        [HttpGet]
        [AllowAnonymous]
        public ActionResult HealthCheck()
        {
            return Ok("I'm alive and working");
        }

        [HttpGet]
        [Authorize(Roles = "manager,user")]
        public IActionResult GetAttachamentById(int attachamentId)
        {
            try
            {
                var attachament = baseService.GetById(attachamentId);
                return Ok(attachament);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                });
            }
        }

        [HttpGet]
        [Authorize(Roles = "manager")]
        public IActionResult GetAllAttachaments()
        {
            try
            {
                var attachamentsEnumerable = baseService.Get();
                return Ok(attachamentsEnumerable);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAttachamentByIdUser(int idUser)
        {
            try
            {
                var attachametFromUserFound = attachamentService.GetAttachamentByIdUser(idUser);
                return Ok(attachametFromUserFound);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                });
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        public IActionResult DeleteAttachament(int attachamentId)
        {
            try
            {
                baseService.Delete(attachamentId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public IActionResult AddAttachment([FromForm] AttachamentDTO attachmentDTO)
        {
            try
            {
                var attachament = attachamentService.SaveAttachamentsExam(attachmentDTO);
                var insertAttachament = baseService.Add<AttachamentValidator>(attachament);
                return Ok(insertAttachament);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                });
            }

        }
    }
}
