using E_Commerce.API.Extensions;

namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region DI - Container
            var builder = WebApplication.CreateBuilder(args);

            // WebApi Services
            builder.Services.AddWebApiServices(builder.Configuration);

            // Infrastructure Services
            builder.Services.AddInfrastructureServices(builder.Configuration);

            //Core Services
            builder.Services.AddCoreServices(builder.Configuration); 
            #endregion

            #region Pipelines - Middleware
            var app = builder.Build();
            await app.SeedDatabaseAsync();

            // Configure the HTTP request pipeline.
            app.UseExceptionHandlingMiddlewares();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();
            app.UseAuthorization();
            app.MapControllers();

            app.Run(); 
            #endregion
        }
    }
}
