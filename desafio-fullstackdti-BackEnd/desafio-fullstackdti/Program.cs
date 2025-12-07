using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Domain.Repositories;
using Application.Interfaces;
using Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000") // origem permitida
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI - repositories, services
builder.Services.AddScoped<ILeadRepository, LeadRepository>();
builder.Services.AddScoped<IEmailService, EmailFakeService>();
builder.Services.AddScoped<LeadService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DevCors");
app.UseRouting();
app.MapControllers();
app.Run();
