using JustGoApi.Data;
using JustGoApi.Services;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// I Injected following service 

builder.Services.AddScoped<IJustGoService, JustGoService>();
builder.Services.AddScoped<IListingService, ListingService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// I added these lines for InMemoryDatabase
//builder.Services.AddDbContext<ContactsDbContext>(options =>
//         options.UseInMemoryDatabase("ContactsDetails"));


//I added to connect with Sql Server
builder.Services.AddDbContext<ContactsDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ContactsApiConnectionString")));


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

// added folowing line to connect with react
   app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
