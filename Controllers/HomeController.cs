using Microsoft.AspNetCore.Mvc;


//health check
namespace Blog.Controllers
{

    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {


        [HttpGet]
        public IActionResult Get()
        {
            //return Ok(new
            //{
            //    fruta = "banana"
            //});
            return Ok();
        }

    }
}
