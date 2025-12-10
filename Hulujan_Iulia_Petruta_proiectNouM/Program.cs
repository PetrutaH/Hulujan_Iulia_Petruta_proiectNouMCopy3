using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Hulujan_Iulia_Petruta_proiectNouM.Data;
using Hulujan_Iulia_Petruta_proiectNouM.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Hulujan_Iulia_Petruta_proiectNouMContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Hulujan_Iulia_Petruta_proiectNouMContext") ?? throw new InvalidOperationException("Connection string 'Hulujan_Iulia_Petruta_proiectNouMContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IStudentPredictionService, StudentPredictionService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:50737"); 
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DbInitializer.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
