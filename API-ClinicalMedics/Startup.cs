using API_ClinicalMedics.Domain.Entities;
using API_ClinicalMedics.Domain.Interfaces;
using API_ClinicalMedics.Infra.Data.Repository;
using API_ClinicalMedics.Service.Service;
using API_ClinicalMedics.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using API_ClinicalMedics.Infra.CrossCutting.IMapper;
using System.Text;
using API_ClinicalMedics.Infra.CrossCutting.Utils;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API_ClinicalMedics
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var key = Encoding.ASCII.GetBytes(ChaveJWT.ChaveSecreta);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddScoped<IBaseRepository<Users>, BaseRepository<Users>>();
            services.AddScoped<IBaseRepository<Attachaments>, BaseRepository<Attachaments>>();
            services.AddScoped<IBaseService<Users>, BaseService<Users>>();
            services.AddScoped<IBaseService<Attachaments>, BaseService<Attachaments>>();
            services.AddAutoMapper(typeof(Mappers));

            services.AddHttpClient();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<ClinicalsMedicsContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 23))));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}


