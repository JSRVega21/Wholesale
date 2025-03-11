global using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

            builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
            });

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

            builder.Services.AddHttpClient<SapService>()
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });

            builder.Services.AddHttpClient<SapService>(client =>
            {
                client.BaseAddress = new Uri("https://172.16.50.45:50000/b1s/v1/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

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

            #region Repositorios

            #region Servicios de usuario
            builder.Services.AddScoped<IUserRepository<User, int>, UserRepository>();
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            #endregion

            #region Servicios de checklist de vendedores
            builder.Services.AddScoped<IRepository<VisitHeader, int>, VisitHeaderRepository>();
            builder.Services.AddScoped<IRepository<VisitDetail, int>, VisitDetailRepository>();
            #endregion

            #endregion


            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.MaxDepth = 64; // Opcional, si quieres más profundidad
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddLocalization();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

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

            app.MapControllers();

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
