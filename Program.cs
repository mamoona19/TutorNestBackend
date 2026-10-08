using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Security.Claims;
using System.Text;
using TutorNest.API.Interfaces;
using TutorNest.API.Repositories;
using TutorNest.API.Repositories.Area;
using TutorNest.API.Repositories.Auth_Repository;
using TutorNest.API.Repositories.City;
using TutorNest.API.Repositories.Parent;
using TutorNest.API.Repositories.Payment;

using TutorNest.API.Repositories.Student;
using TutorNest.API.Repositories.Subject;
using TutorNest.API.Repositories.TutorRequest;
using TutorNest.API.Services;
using TutorNest.API.Services.Area;
using TutorNest.API.Services.Auth_Service;
using TutorNest.API.Services.City;
using TutorNest.API.Services.Parent;
using TutorNest.API.Interfaces;
using TutorNest.API.Repositories.Payment;
using TutorNest.API.Services;
using TutorNest.API.Services.Student;
using TutorNest.API.Services.Subject;
using TutorNest.API.Services.TuitionService;



var builder = WebApplication.CreateBuilder(args);
// jwt
var jwtKey = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey!)
            ),

            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            ValidateLifetime = true,

            ClockSkew = TimeSpan.Zero,

            RoleClaimType =
                "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("JWT AUTH FAILED:");
                Console.WriteLine(context.Exception.Message);

                return Task.CompletedTask;
            },

            OnTokenValidated = context =>
            {
                Console.WriteLine("JWT TOKEN VALIDATED");

                foreach (var claim in context.Principal!.Claims)
                {
                    Console.WriteLine(
                        $"CLAIM: {claim.Type} = {claim.Value}"
                    );
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();
//
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter JWT token only. Do not include the Bearer prefix."
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddScoped<ITuitionRequestService, TuitionRequestService>();
builder.Services.AddScoped<ITuitionRequestRepository,TuitionRequestRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITutorRequestRepository, TutorRequestRepository>();
builder.Services.AddScoped<ITutorRequestService, TutorRequestService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<IParentRepository, ParentRepository>();
builder.Services.AddScoped<IParentService, ParentService>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var connectionString =
        sp.GetRequiredService<IConfiguration>()
          .GetConnectionString("DefaultConnection");

    return new SqlConnection(connectionString);
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
var app = builder.Build();
app.UseCors("AngularPolicy");
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
