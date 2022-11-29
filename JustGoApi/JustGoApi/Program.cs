using JustGoApi.Data;
using JustGoApi.Handler;
using JustGoApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Linq.Expressions;
using System.Security.Cryptography.Xml;
using System.Text;
//using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//From .AddJsonOptions I added for token
builder.Services.AddControllers().AddJsonOptions(option =>
{
  option.JsonSerializerOptions.PropertyNamingPolicy = null;
});



// I added for token
builder.Services.AddDbContext<ReuseDbContext>(option 
=> option.UseSqlServer(builder.Configuration.GetConnectionString("ReuseApiConnectionString")));


// I Injected following service 
builder.Services.AddScoped<IListingService, ListingService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JustGoApi", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement { 
    {
        new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
        }
    },
      new string[]{}
    }
        
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))

};
});


//I added to connect with Sql Server
builder.Services.AddDbContext<ReuseDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ReuseApiConnectionString")));


//I added to communicate with react
  builder.Services.AddCors();


builder.Services.AddHttpContextAccessor();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    //I added for token
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoJWTToken v1"));
}
// I added for retrieving static image files from folder
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Images")),
    RequestPath = "/Images"
});

// added folowing line to connect with react
   app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));


app.UseHttpsRedirection();

// I added for authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
