using CSharpClicker.Web.Infrastructure.DataAccess;
using CSharpClicker.Web.Initializers;

namespace CSharpClicker.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services);

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            using var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            DbContextInitializer.InitializeDbContext(appDbContext);

            //app.MapControllers();

            app.UseMvc();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddSwaggerGen();
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddMediatR(o => o.RegisterServicesFromAssemblies(typeof(Program).Assembly));
            services.AddMvcCore(o => o.EnableEndpointRouting = false)
                .AddApiExplorer();

            IdentityInitializer.AddIdentity(services);
            DbContextInitializer.AddAppDbContext(services);
        }
    }
}
