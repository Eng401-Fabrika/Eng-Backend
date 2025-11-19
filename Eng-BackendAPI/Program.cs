using System.Text;
using Eng_Backend.DAL.DbContext;
using Microsoft.EntityFrameworkCore;
using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.BusinessLayer.Managers;
using Eng_Backend.BusinessLayer.Utils;
using Eng_Backend.DAL.Entities;
using Eng_Backend.DAL.Interfaces;
using Eng_Backend.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IAuthService, AuthManager>();


// Configure Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Swagger'da kilit ikonunu çıkarmak için bu ayar şart
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Eng_Backend API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Örnek: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
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
            new string[] { }
        }
    });
});

// Kendi servislerimizi bağlıyoruz
// Eski satırı silip bunu yapıştır:
builder.Services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));

// Bu satır aynı kalabilir (Manager hala aynı servisi implemente ediyor):
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));
builder.Services.AddScoped<JwtHelper>(); // Token üretici
builder.Services.AddScoped<IAuthService, AuthManager>(); // Auth iş mantığı
// İleride buraya generic servisleri de ekleyeceksin:
// builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>)); 
// 1. Önce Identity Servisi Eklenir
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// 2. Sonra Authentication Ayarları Eklenir (Identity'nin Cookie ayarını eziyoruz)
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
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