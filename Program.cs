using asp_net_restful_api_jwt.Authentication;
using asp_net_restful_api_jwt.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AspNetCoreWebApi6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

            // Add services to the container.
            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseContext")));

            builder.Services.AddControllers();

            //builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {   // for development only
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["ApplicationSettings:JWT:SecretKey"])),
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["ApplicationSettings:JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["ApplicationSettings:JWT:Audience"]
                };
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}