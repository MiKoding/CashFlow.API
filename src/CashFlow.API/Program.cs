using CashFlow.API.CultureMiddleware;
using CashFlow.API.Filters;
using CashFlow.Application;
using CashFlow.Infraestructure;
using CashFlow.Infraestructure.Migrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using CashFlow.Infraestructure.Extensions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = @"JWT Authorization header using the Bearer scheme.
                    Enter 'Bearer'[space] and then your token in the text input below.
                    Example: 'Bearer 1234abcdef",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        Type = SecuritySchemeType.ApiKey
    });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

//builder.Configuration.GetConnectionString("Connection"); //recupera connection string do appsetings

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

//resumido de DependencyInjectionExtension.AddInfraestructure(builder.Services); ao utilizar THIS em DependencyINjection
// this deixa implicito o valor de IServiceCollection
builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddAplication();

var signingKey = builder.Configuration.GetValue<string>("Settings:Jwt:SigningKey");

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.TokenValidationParameters = new TokenValidationParameters 
    { 
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = new TimeSpan(0),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey!))
    };
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (builder.Configuration.IsTestEnvironment() == false)
{
    await MigrateDataBase();
}

app.Run();

async Task MigrateDataBase()
{
    await using var scope = app.Services.CreateAsyncScope();

    await DataBaseMigration.MigrateDatabase(scope.ServiceProvider);

}

public partial class Program { }
