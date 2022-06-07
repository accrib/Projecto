using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using _01_DAL;
using _02_WebService.Models.DataManager;
using _02_WebService.Models.Repository;
using _02_WebService.Controllers;

namespace _02_WebService
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
            // Vai fazer a ligação do código json que se encontra no appsettings.json á base de dados
            services.AddDbContext<BD>(
                options => options.UseSqlServer(Configuration.GetConnectionString("cns")));

            // É aqui que é feita a configuração do repositório para cada class 
            services.AddScoped<IDataRepository<Empregado>, Empregado_DataManager>();
            services.AddScoped<IDataRepository<Produto>, Produto_DataManager>();
            services.AddScoped<IDataRepository<Fatura>, Fatura_DataManager>();
            services.AddScoped<IDataRepository<Linha_Fatura>, Linha_Fatura_DataManager>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
