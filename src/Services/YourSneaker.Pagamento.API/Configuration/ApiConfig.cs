﻿using Microsoft.EntityFrameworkCore;
using YourSneaker.Pagamento.API.Data;
using YourSneaker.WebAPI.Core.Identidade;

namespace YourSneaker.Pagamento.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PagamentosContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            services.AddCors(options =>
            {
                options.AddPolicy("Total", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
            services.AddControllers();
            services.AddEndpointsApiExplorer();
        }
        public static void UseApiConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Total");

            app.UseAuthConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
