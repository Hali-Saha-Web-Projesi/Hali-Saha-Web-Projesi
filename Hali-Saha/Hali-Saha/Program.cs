using DataAccess.Data;
using HaliSaha_Model.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "NetCoreMvc.Auth"; //cookie ismi
    options.LoginPath = "/Login/Index";     //kullan�c� bulunamazsa nereye y�nlenece�i(cookie bulunamazsa yani) indexe gider.
    options.AccessDeniedPath = "/Login/Index";//kullan�c�n�n ayn� zamanda yetkili olmas� i�in kullan�l�r. yetkisiz kullan�c�y� da login �ndexe g�nder
});
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


//LOCAL�ZAT�ON ���N
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


//register sayfas� i�in
//builder.Services.AddDbContext<DbHaliSahaContext>();
builder.Services.AddIdentity<AppUser,AppRole>().AddEntityFrameworkStores<DbHaliSahaContext>();

var provider = builder.Services.BuildServiceProvider();

var configuration = provider.GetRequiredService<IConfiguration>();

builder.Services.AddDbContext<DbHaliSahaContext>(item => item.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

//LOCAL�ZAT�ON
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
