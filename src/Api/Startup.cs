using System.Reflection;
using Api.DataAccess;
using Api.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;

namespace Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(options =>
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddTransient<StudentRepository>();
            services.AddTransient<CourseRepository>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandler>();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
