using ASPNETAPI.Data.Utility.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;



namespace ASPNETAPI.API
{
  [ApiController]
  [Route("[controller]")]
  public class ExampleController : ControllerBase
  {

      private readonly IUnitOfWork _uow;

      public ExampleController(
          IUnitOfWork uow
      )
      {
          _uow = uow;
      }

    [HttpGet]
    public string Get()
    {
      // Use reflection to get all method names from the IUnitOfWork interface
      var methods = _uow.GetType()
                        .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                        .Select(m => m.Name)
                        .Distinct(); // Ensure unique method names

      return string.Join(", ", methods);
    }
  }
}