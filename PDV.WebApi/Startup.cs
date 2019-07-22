using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PDV.Domain.Interfaces.Repositories;
using PDV.Domain.Interfaces.Services;
using PDV.Domain.Services;
using PDV.Infra.Data.Context;
using PDV.Infra.Data.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace PDV.WebApi
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
            // Services
            services.AddTransient<ITransacaoService, TransacaoService>();

            // Repositories
            services.AddTransient<ITrasacaoRepository, TransacaoRepository>();

            //Context
            services.AddScoped<PdvContext, PdvContext>();

            services.AddMvc();

            services.AddSwaggerGen(x => {
                x.SwaggerDoc("v1", new Info { Title = "PDV", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PDV v1");
            });

            app.UseMvc();
        }
    }
}
