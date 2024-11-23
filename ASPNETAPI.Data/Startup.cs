using Microsoft.OpenApi.Models;

namespace ASPNETAPI.Data{
  public class Startup
  {
      private readonly IConfiguration _configuration;

      public Startup(IConfiguration configuration)
      {
          _configuration = configuration;
      }

      // ConfigureServices is used to register services
      public void ConfigureServices(IServiceCollection services)
      {
          services.AddEndpointsApiExplorer();
          services.AddSwaggerGen(c =>
          {
              c.SwaggerDoc(
                  "v1",
                  new OpenApiInfo
                  {
                      Title = "Todo API",
                      Description = "Keep track of your tasks",
                      Version = "v1"
                  });
          });
          services.AddControllers();
      }

      // Configure is used to set up the HTTP request pipeline
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
          if (env.IsDevelopment())
          {
              app.UseSwagger();
              app.UseSwaggerUI(c =>
              {
                  c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
              });
          }

          app.UseRouting();

          app.UseEndpoints(endpoints =>
          {
              endpoints.MapControllers();
          });
      }
  }
}
