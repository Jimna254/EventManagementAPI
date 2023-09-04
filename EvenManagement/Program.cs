using Auth.Extensions;
using EvenManagement.Data;
using EvenManagement.Extension;
using EvenManagement.Services;
using EvenManagement.Services.IServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//inject DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{ 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

//DI Services

builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IEventServices, EventServices>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Authentication
builder.AddAppAuthentication();
//Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("Role", "Admin");
    });
});

builder.AddSwaggenGenExtension();

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
