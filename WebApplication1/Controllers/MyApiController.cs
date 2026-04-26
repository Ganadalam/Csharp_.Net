using Microsoft.AspNetCore.Mvc;

namespace HelloApi.Controllers
{
  [ApiController]
  [Route("[controller]")] // 주소창에 api/hello 라고 치면 이리로 들어와
public class MyApiController : ControllerBase
{
    // GET: /MyApi/time
    [HttpGet("time")]
    public IActionResult GetTime()
    {
        return Ok($"Current server time : {DateTime.Now}");
    }
    //GET: /MyApi/greet/{name}
    [HttpGet ("greet/{name}")]
    public IActionResult Greet(string name)
    {
        return Ok($"Hi, {name}! welcome to me!");
    }
}
}
