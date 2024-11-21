using Microsoft.AspNetCore.Mvc;



namespace Metaufiq.Simple.API.Controller
{
  [ApiController]
  [Route("[controller]")]
  public class ExampleController : ControllerBase
  {
      [HttpGet]
      public string Get() => "Hello from the controller!";
  }
}