using Microsoft.AspNetCore.Mvc;

namespace API_ClinicalMedics.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ManagerController : ControllerBase
    {
        [HttpGet]
        public ActionResult HealthCheck()
        {
            return Ok("I'm alive and working");
        }
    }
}
