using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin;
using SECapstoneEvaluation.Infrastructure.Data;
using SECapstoneEvaluation.Domain.Interfaces.Repositories;
using SECapstoneEvaluation.Infrastructure.Data.Repositories;
using SECapstoneEvaluation.APIs.Services.Constracts;
using SECapstoneEvaluation.APIs.Services;
using SECapstoneEvaluation.Domain.Interfaces.UnitOfWork;
using SECapstoneEvaluation.Infrastructure.Data.UnitOfWork;
using Serilog;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Config Log
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//Config CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin();
                      });
});

// Add services to the container.
services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

services.AddRouting(options => options.LowercaseUrls = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();

services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "SECapstoneEvaluation Project", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.ApiKey,
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new string[]{}
        }
    });
});

//private key
var pathToKey = Path.Combine(Directory.GetCurrentDirectory(), configuration["Firebase:AdminSdkJsonFile"]);
GoogleCredential credential = GoogleCredential.FromFile(pathToKey);

//Create Firebase app
FirebaseApp.Create(new AppOptions
{
    Credential = credential,
    ProjectId = "se-capstone-project-management",
    ServiceAccountId = "firebase-adminsdk-n20mx@se-capstone-project-management.iam.gserviceaccount.com"
});

//Add Application DB Context 
//services.AddDbContextPool<AppDbContext>(options =>
//    options.UseSqlServer(configuration.GetConnectionString("SECapstoneEvaluationConnection"))
//);

string connectionString = null;

string? envVar = Environment.GetEnvironmentVariable("PostgreSQLSECapstoneEvaluationConnection");

if (string.IsNullOrEmpty(envVar))
{

    connectionString = configuration.GetConnectionString("PostgreSQLSECapstoneEvaluationConnection");

}
else
{
    //parse database URL. Format is postgres://<username>:<password>@<host>/<dbname>

    connectionString = envVar;
}

services.AddDbContextPool<AppDbContext>(options =>
        options.UseNpgsql(connectionString)
);

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = configuration["Jwt:Issuer"];
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
    };
    options.RequireHttpsMetadata = false;
});

// IOC for Configuration
services.AddSingleton<IConfiguration>(configuration);

#region IOC for Repositories
services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IRoleUserRepository, RoleUserRepository>();
services.AddScoped<IRoleRepository, RoleRepository>();
services.AddScoped<ICampusRepository, CampusRepository>();
#endregion

#region IOC for Services
services.AddTransient<IUserService, UserService>();
services.AddTransient<ICampusService, CampusService>();
#endregion

#region IOC for UnitOfWork
services.AddTransient<IUnitOfWork, UnitOfWork>();
#endregion

#region IOC for Logging
services.AddLogging();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // do nothing
}
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SECapstoneEvaluationAPI v1"));

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
