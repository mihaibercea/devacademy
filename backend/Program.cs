using Microsoft.AspNetCore.HttpLogging;

namespace DotNetWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //var userService = new UserService();

            //builder.Services.AddSingleton<IUserService>(userService);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                                       builder =>
                                       {
                                           builder.AllowAnyOrigin();
                                           builder.AllowAnyMethod();
                                           builder.AllowAnyHeader();
                                       });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            //app.UseAuthorization();

            app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                                                        //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
                    .AllowCredentials());

            app.MapControllers();


            //enable web api diagnostic middleware




            app.Run();
        }
    }
}
