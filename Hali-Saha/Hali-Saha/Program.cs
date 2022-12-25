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
    options.LoginPath = "/Login/Index";     //kullanýcý bulunamazsa nereye yönleneceði(cookie bulunamazsa yani) indexe gider.
    options.AccessDeniedPath = "/Login/Index";//kullanýcýnýn ayný zamanda yetkili olmasý için kullanýlýr. yetkisiz kullanýcýyý da login ýndexe gönder
});
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
