using Appeals.Identity.Common.Helpers;
using Appeals.Identity.Data;
using Appeals.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Appeals.Identity
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string dbConnection = Configuration["DbConnection"];
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddControllersWithViews();
            services.AddDbContext<AuthDbContext>(opt =>
                {
                    opt.UseNpgsql(dbConnection);
                });

            services.AddIdentity<AppUser, IdentityRole>(opt =>
                {
                    opt.Password = AppUserHelper.GetPasswordOptions();
                })
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();
            ;

            services.AddIdentityServer()
                .AddAspNetIdentity<AppUser>()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiScopes(ISConfiguration.ApiScopes)
                .AddInMemoryApiResources(ISConfiguration.ApiResources)
                .AddInMemoryIdentityResources(ISConfiguration.IdentityResources)
                .AddInMemoryClients(ISConfiguration.Clients);

            services.ConfigureApplicationCookie(opt =>
                {
                    opt.Cookie.Name = "Appeals.Identity.Cookie";
                    opt.LoginPath = "/Auth/Login";
                    opt.LogoutPath = "/Auth/Logout";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "Styles")),
                RequestPath = "/styles"
            });
            app.UseRouting();
            app.UseIdentityServer();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}