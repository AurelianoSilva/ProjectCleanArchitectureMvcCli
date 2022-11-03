using CleanArchMvc.Infra.IoC;

namespace CleanArchMvc.WebUI
{
    public class Startup : IStartup
    {
        public IConfiguration configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureService(IServiceCollection services)
        {
            // Add services to the container.

            /*
             * Adicionando referencia ao projeto infra.IoC para implementar o conceito de 
             * Dependende injection 
             */
            services.addInfrastructure(configuration);
            services.AddControllersWithViews();
            
        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

        }
    }

    public interface IStartup
    {
        IConfiguration configuration { get; }
        void Configure(WebApplication app, IWebHostEnvironment environment);
        void ConfigureService(IServiceCollection services);
    }

    public static class StartupExtensions
    {
        public static WebApplicationBuilder UserStartup<TStartup>(this WebApplicationBuilder WebAppBuilder) where TStartup : IStartup
        {
            var startup = Activator.CreateInstance(typeof(TStartup), WebAppBuilder.Configuration) as IStartup;
            if (startup == null) throw new ArgumentException("Class Startup.cs invalid!");

            startup.ConfigureService(WebAppBuilder.Services);

            var app = WebAppBuilder.Build();
            startup.Configure(app, app.Environment);

            app.Run();

            return WebAppBuilder;
        }
    }
}
