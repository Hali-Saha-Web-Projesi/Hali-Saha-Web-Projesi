using DataAccess.Data;
using HaliSaha_Model.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "NetCoreMvc.Auth"; //cookie ismi
    options.LoginPath = "/Login/Index";     //kullanýcý bulunamazsa nereye yönleneceði(cookie bulunamazsa yani) indexe gider.
    options.AccessDeniedPath = "/Login/Index";//kullanýcýnýn ayný zamanda yetkili olmasý için kullanýlýr. yetkisiz kullanýcýyý da login ýndexe gönder
});
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


//LOCALÝZATÝON ÝÇÝN
builder.Services.AddLocalization(opts => {
    opts.ResourcesPath = "Resources";
});


//var localizationOptions = new RequestLocalizationOptions();

builder.Services.AddMvc()
    .AddViewLocalization(opts => { opts.ResourcesPath = "Resources"; })
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();
     builder.Services.AddControllersWithViews();

builder.Services.Configure<RequestLocalizationOptions>(opts =>
{
        var supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en-US"),
            new CultureInfo("tr"),
        };
    opts.DefaultRequestCulture = new RequestCulture("tr");
    opts.SupportedCultures = supportedCultures;
    opts.SupportedUICultures = supportedCultures;
    //opts.SetDefaultCulture("en-US");
    //opts.ApplyCurrentCultureToResponseHeaders = true;

});


//builder.Services.AddControllers();


//register sayfasý için
//builder.Services.AddDbContext<DbHaliSahaContext>();
builder.Services.AddIdentity<AppUser,AppRole>().AddEntityFrameworkStores<DbHaliSahaContext>();

var provider = builder.Services.BuildServiceProvider();

var configuration = provider.GetRequiredService<IConfiguration>();

builder.Services.AddDbContext<DbHaliSahaContext>(item => item.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));




builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 3;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;


});
var app = builder.Build();

//LOCALÝZATÝON

//app.UseRequestLocalization(localizationOptions);

//var options = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
//app.UseRequestLocalization(options.Value);

builder.Services.AddControllersWithViews();





//builder.Services.AddHttpContextAccessor(); //bir bak

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

//LOCALÝZATÝON
var options = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);



app.UseCookiePolicy();  //fazladan olabilir.

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();
