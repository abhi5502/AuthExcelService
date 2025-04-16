using AuthExcelService.API.ServiceExtensions;
using AuthExcelService.Common.Extensions;
using AuthExcelService.Domain.Entities;
using AuthExcelService.Persistence.Data;
using AuthExcelService.Persistence.Seeding;
using AuthExcelService.Repositoies.Contracts.IEmaiService;
using AuthExcelService.Repositoies.GenericRepository;
using AuthExcelService.Repositoies.IGenericRepository;
using AuthExcelService.Repositoies.RepositoryService.EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services for FluentValidation
builder.Services.AddFluentValidationServices();

// Add services to the container.
builder.Services.AddControllers();

// Add common services (including Serilog logging)
builder.Services.AddCommonServices(builder.Configuration, typeof(Program).Assembly, "api");

// For Email Service
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddScoped<ICustomDbContextFactory, CustomDbContextFactory>();



// Call the extension method to register Identity and other services
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "AuthExcelService API",
        Version = "v1",
        Description = "API for AuthExcelService"
    });

    // Enable JWT Authentication in Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token as: Bearer {your_token_here}"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                         Array.Empty<string>()
                    }
                });


});

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMvcApp",
        policy => policy.WithOrigins("https://localhost:7008/") // <-- MVC App URL
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
});

var app = builder.Build();

////  Seed roles
//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
//    await RoleSeeder.SeedRolesAsync(roleManager);
//}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDBContext1>();
    await CountrySeeder.SeedCountryAsync(dbContext);
}




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowMvcApp");
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
