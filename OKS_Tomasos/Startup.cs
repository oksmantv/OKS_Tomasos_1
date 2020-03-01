using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using OKS_Tomasos.Models;
using OKS_Tomasos.Repositories;
using OKS_Tomasos.Services.RegisterService;
using OKS_Tomasos.Services.LoginService;
using Microsoft.AspNetCore.Http;

namespace OKS_Tomasos
{
    public class Startup
    {
        IConfiguration _Configuration;
        public Startup(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            /// Setup Database
            var conn = _Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<TomasosContext>(options => options.UseSqlServer(conn));
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<RegisterConnection>();
            services.AddHttpContextAccessor();

            /// Setup Session
            services.AddDistributedMemoryCache(); 
            services.AddSession();

            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(5));
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
