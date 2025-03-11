global using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;          // Para configurar FormOptions
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Wholesale.Server.Data;
using Wholesale.Models;
using Wholesale.Server.Service;
using Wholesale.Server.Repository;

namespace Wholesale.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1) Configuración de EF Core
            builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
            });

            // 2) Configuración de CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Wholesale.Server", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .WithExposedHeaders("Authorization");
                });
            });

            // 3) Configuración para subida de archivos grandes (opcional, si lo necesitas)
            builder.Services.Configure<FormOptions>(options =>
            {
                // Límite de 100 MB, por ejemplo
                options.MultipartBodyLengthLimit = 100_000_000;
            });

            // 4) Configuración de autenticación JWT (si la usas)
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "Wholesale.Server",
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes("aplicacion_FFACSA_Wholesale.Server_ClaveSecreta"))
                    };
                });

            builder.Services.AddAuthorization();

            // 5) Registrar tus repositorios
            #region Repositorios

            #region Servicios de usuario
            builder.Services.AddScoped<IUserRepository<User, int>, UserRepository>();
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            #endregion

            #region Servicios de checklist de vendedores
            builder.Services.AddScoped<IRepository<VisitHeader, int>, VisitHeaderRepository>();
            builder.Services.AddScoped<IRepository<VisitDetail, int>, VisitDetailRepository>();
            #endregion

            #region Servicios para los catalogos
            builder.Services.AddScoped<IRepository<RegionHeader, int>, RegionHeaderRepository>();
            builder.Services.AddScoped<IRepository<RegionDetail, int>, RegionDetailRepository>();
            builder.Services.AddScoped<IRepository<VisitType, int>, VisitTypeRepository>();
            #endregion

            #endregion

            // 6) Configuración de serialización
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Evitar problemas de referencias cíclicas
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    // Aumentar la profundidad si fuera necesario
                    options.JsonSerializerOptions.MaxDepth = 64;
                });

            // 7) Swagger (para documentar tu API)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 8) Resto de servicios
            builder.Services.AddLocalization();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Swagger solo en Development (por ejemplo)
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("Wholesale.Server");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });

            // Para debug de token
            app.Use(async (context, next) =>
            {
                if (context.Request.Headers.ContainsKey("Authorization"))
                {
                    var token = context.Request.Headers["Authorization"].ToString();
                    Console.WriteLine($"Token recibido en el servidor: {token}");
                }
                else
                {
                    Console.WriteLine("No se recibió el encabezado Authorization.");
                }
                await next();
            });

            app.Run();
        }
    }
}
