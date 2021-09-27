using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartianRobots.Database.Contexts;
using MartianRobots.Database.Entities;
using MartianRobots.Database.Repositores;
using MartianRobots.Database.Repositores.Base;
using MartianRobots.Servicies;
using MartianRobots.Shared.Inferfaces.Servicies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MartianRobots
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<MartianRobotsContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MartianRobots"));
            });
            RegisterRepositories(services);
            RegisterServicies(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            
        }

        private void RegisterServicies(IServiceCollection services)
        {
            
            services.AddScoped<IRobotService, RobotService>();
        }
    }
}
