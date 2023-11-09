using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Utils;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => {

    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    
    x.AddSecurityRequirement(new OpenApiSecurityRequirement() {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

// Dependecy Injection
builder.Services.AddTransient<Interfaces.IUserRepository, Repositories.UserRepository>();
builder.Services.AddTransient<Interfaces.IAddressRepository, Repositories.AddressRepository>();
builder.Services.AddTransient<Interfaces.ICompanyRepository, Repositories.CompanyRepository>();
builder.Services.AddTransient<Interfaces.ICrypt, Crypt>();
if (builder.Environment.IsDevelopment()) {
    builder.Services.AddTransient<Interfaces.IStorage, DiskStorage>();
} else {
    builder.Services.AddTransient<Interfaces.IStorage, AwsStorage>();
}

// CORS
builder.Services.AddCors(options => {
    options.AddPolicy(name: "MyPolice", 
        Policy => {
            Policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
        }
    );
});

// JWT config
var key = Encoding.ASCII.GetBytes(Token.tokenSecret);
builder.Services.AddAuthentication(x => 
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error-development");
    app.UseSwagger();
    app.UseSwaggerUI();

} else {
    app.UseExceptionHandler("/error");
};

// app.UseHttpsRedirection(); me gastou 1000000 horas do dia

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
