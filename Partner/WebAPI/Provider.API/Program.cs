
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;

using Microsoft.EntityFrameworkCore;
using Provider.Application.Interfaces;
using Provider.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Provider.Domain.Interfaces;
using Provider.Infrastructure.Repositories;
using Provider.Infrastructure.Persistence;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        //add swagger
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Where(e => e.Value!.Errors.Count > 0)
                    .SelectMany(e => e.Value!.Errors)
                    .Select(e => e.ErrorMessage);

                return new UnprocessableEntityObjectResult(errors);
            };
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options => {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidIssuer = builder.Configuration["Jwt:Issuer"],
                   ValidateAudience = true,
                   ValidAudience = builder.Configuration["Jwt:Audience"],
                   ValidateIssuerSigningKey = true,
                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.Zero,



                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt").GetSection("Key").Value ?? "")),

               };
           }
        );
        builder.Services.AddAuthorization();


        builder.Services.AddSwaggerGen(options =>
        {

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Provider API",
                Version = "v1"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Enter: Bearer {your token}"
            });

            options.AddSecurityRequirement(doc => new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecuritySchemeReference("Bearer",doc),
                        new List<string>()
                    }
                }
            );



        });

        string? connectionString = builder.Configuration.GetConnectionString("DBContextConnection");

        builder.Services.AddDbContext<ServicesDbContext>(options =>options.UseSqlServer(connectionString));


        //// Repositories
        //builder.Services.AddScoped<IUserRepository, UserRepository>();
        //builder.Services.AddScoped<IUtilityService, UtilityService>();
        //builder.Services.AddScoped<IJwtService, JwtService>();
        //builder.Services.AddScoped<ISecretGeneratorService, SecretGeneratorService>();
        builder.Services.AddScoped<IServiceProviderRepository, ServiceProviderRepository>();

        // Services
        builder.Services.AddScoped<IServiceProviderService, ServiceProviderService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }


}

