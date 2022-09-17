using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TddExample.Application.AutoMapper;
using TddExample.Application.Business;
using TddExample.Application.Configuration;
using TddExample.CrossCutting.Middlewares;
using TddExample.Data.Context;
using TddExample.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

var sqlConnectionString = builder.Configuration.GetValue<string>("ConnectionStrings:Sql");
var secretKey = builder.Configuration.GetValue<string>("AppSettingsConfiguration:SecretKey");

builder.Services.Configure<AppSettingsConfiguration>(builder.Configuration.GetSection("AppSettingsConfiguration"));
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<TddExampleContext>();


#region Repositories

typeof(UserRepository)
        .Assembly
        .GetTypes()
        .Select(x => x.GetInterfaces().Where(y => y.Namespace == "TddExample.Data.Interface"))
        .Where(x => x.Any()).ToList()
        .ForEach(assignedTypes =>
        {
            var interfaceType = assignedTypes.FirstOrDefault(f => f.Name != "IRepository`1");
            if (interfaceType != null)
            {
                var classType = typeof(UserRepository).Assembly.GetTypes()
                    .Where(w => w.Namespace == "TddExample.Data.Repository")
                    .First(f => f.Name == $"{interfaceType.Name.Substring(1)}");

                if (builder.Services.Any(a => a.ServiceType.Name != interfaceType.Name))
                    builder.Services.AddTransient(interfaceType, classType);
            }
        });

#endregion

#region Business

typeof(UserBusiness)
    .Assembly
    .GetTypes()
    .Select(x => x.GetInterfaces().Where(y => y.Namespace == "TddExample.Application.Interface"))
    .Where(x => x.Any()).ToList()
    .ForEach(assignedTypes =>
    {
        var interfaceType = assignedTypes.First();
        var classType = typeof(UserBusiness).Assembly.GetTypes()
            .Where(w => w.Namespace == "TddExample.Application.Business")
            .First(f => f.Name == $"{interfaceType.Name.Substring(1)}");

        builder.Services.AddTransient(interfaceType, classType);
    });

#endregion

#region Clients - CrossCut

builder.Services.AddHttpClient();

#endregion

#region AutoMapper

builder.Services.AddAutoMapper(typeof(AuthProfile));
builder.Services.AddAutoMapper(typeof(UserProfile));

#endregion

var key = Encoding.ASCII.GetBytes(secretKey);
builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
     .AddJwtBearer(x =>
     {
         x.RequireHttpsMetadata = false;
         x.SaveToken = true;
         x.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(key),
             ValidateIssuer = false,
             ValidateAudience = false
         };
     });

builder.Services.AddDbContext<TddExampleContext>(options => options.UseSqlServer(sqlConnectionString));


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

app.UseMiddleware<RequestMiddeware>();

app.Run("https://localhost:5000");

public partial class Program { }