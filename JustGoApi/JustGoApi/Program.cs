using JustGoApi.Data;
using JustGoApi.Handler;
using JustGoApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
//using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// I Injected following service 
builder.Services.AddScoped<IListingService, ListingService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // I added this for basic authentication in swagger
    c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
    {
        Description = "Basic auth added to authorization header",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "basic",
        Type = SecuritySchemeType.Http
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Basic" }
                    },
                    new List<string>()
                }
            });
});

// I added for authentication
builder.Services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions,BasicAuthenticationHandler>("BasicAuthentication", null);

// I added these lines for InMemoryDatabase
//builder.Services.AddDbContext<ContactsDbContext>(options =>
//         options.UseInMemoryDatabase("ContactsDetails"));


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
    app.UseSwaggerUI();
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
