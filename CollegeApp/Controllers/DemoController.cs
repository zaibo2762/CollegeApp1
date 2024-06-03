using CollegeApp.MyLogging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        //Strongly Coupled
        private readonly ILogger<DemoController> _mylogger;
        //public DemoController()
        //{
        // _mylogger = new LogToFile();
        //}
        //loosely Coupled
        public DemoController(ILogger<DemoController> mylogger)
        {
            _mylogger = mylogger;
        }
        [HttpGet]
        public ActionResult Index()
        {
            _mylogger.LogTrace("Log Message From Trace Method");
            _mylogger.LogDebug("Log Message From Debug Method");
            _mylogger.LogInformation("Log Message From Information Method");
            _mylogger.LogWarning("Log Message From Warning Method");
            _mylogger.LogError("Log Message From Error Method");
            _mylogger.LogCritical("Log Message From Critical Method");
           
            return Ok();
        }

    }
}
