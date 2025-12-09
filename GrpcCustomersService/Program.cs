using GrpcCustomersService.Services;
using Hulujan_Iulia_Petruta_proiectNouM.Data;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext <Hulujan_Iulia_Petruta_proiectNouMContext> (options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddScoped<GrpcCRUDService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GrpcCRUDService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
