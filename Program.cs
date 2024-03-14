using System.Text;
using clinic.Core;
using clinic.Data;
using clinic.Infrastructure;
using clinic.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


//install the package
//dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 7.0.0



builder.Services.AddControllers()
.AddNewtonsoftJson(options=>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);





// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options => {
        options.AddSecurityDefinition( "oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme{
In = Microsoft.OpenApi.Models.ParameterLocation.Header , 
Name ="Authorization" , 
Type=Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
        });
options.OperationFilter<SecurityRequirementsOperationFilter>();

    }
);

builder.Services.AddAuthentication(
 x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(

    options=> options.TokenValidationParameters = new
     TokenValidationParameters{
ValidateIssuerSigningKey=true , 
ValidateAudience=true , 
ValidateIssuer =true ,  
ValidateLifetime=true,

  ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new
             SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!)),
    }


    
);
//database 

builder.Services.AddDbContext<ClinicDb>();

///TODOS:  add package Microsoft.AspNetCore.Authentication.Jwtearer
///TODO: add issure key and 

builder.Services.AddScoped<IClinicRepository,ClinicRepository>();

builder.Services.AddScoped<IAppRepository,AppRepository>();
builder.Services.AddScoped<IHospitalRepository,HospitalRepository>();
builder.Services.AddScoped<IImageRepository,ImageRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IDoctorRepository,DoctorRepository>();


//core
builder.Services.AddScoped<IPasswordHasher,PasswordHasher>();
builder.Services.AddScoped<IJwtProvider,JwtProvider>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});
app.UseAuthorization();

app.MapControllers();

app.Run();
