using Microsoft.AspNetCore.Mvc;



namespace ASPNETAPI.API
{
  [ApiController]
  [Route("[controller]")]
  public class ExampleController : ControllerBase
  {
      [HttpGet]
      public string Get() => "Hello from the controller!";
  }
}