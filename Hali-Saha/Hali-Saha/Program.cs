using DataAccess.Data;
using HaliSaha_Model.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

//LOCALÝZATÝON ÝÇÝN
builder.Services.AddLocalization();
var localizationOptions = new RequestLocalizationOptions();
builder.Services.AddMvc().AddViewLocalization(); //bak
var supportedCultures = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("tr")
};
localizationOptions.SupportedCultures = supportedCultures;
localizationOptions.SupportedUICultures = supportedCultures;
localizationOptions.SetDefaultCulture("tr");
localizationOptions.ApplyCurrentCultureToResponseHeaders = true;

//builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddControllersWithViews();
//register sayfasý için
//builder.Services.AddDbContext<DbHaliSahaContext>();
builder.Services.AddIdentity<AppUser,AppRole>().AddEntityFrameworkStores<DbHaliSahaContext>();

var provider = builder.Services.BuildServiceProvider();

var configuration = provider.GetRequiredService<IConfiguration>();

builder.Services.AddDbContext<DbHaliSahaContext>(item => item.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

//LOCALÝZATÝON
app.UseRequestLocalization(localizationOptions);


builder.Services.AddHttpContextAccessor(); //bir bak

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Register}/{action=Index}/{id?}");

app.Run();
