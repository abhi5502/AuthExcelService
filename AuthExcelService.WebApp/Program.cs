using AuthExcelService.Common.Extensions;
using AuthExcelService.Utility;
using AuthExcelService.WebApp.WebExtensions;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// ...existing code...
builder.Services.AddWebAppServices();
builder.Services.AddHttpContextAccessor();
//StaticDetails.AuthAPIBase= (builder.Configuration["ServiceUrls:AuthAPI"] ?? string.Empty).TrimEnd('/');
StaticDetails.AuthAPIBase = (builder.Configuration["ServiceUrls:AuthAPI"]);

// Add common services (including Serilog logging)
builder.Services.AddCommonServices(builder.Configuration, typeof(Program).Assembly, "web");

builder.Services.AddAuthentication
(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.Cookie.HttpOnly = true;
                  options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                  options.LoginPath = "/Auth/Login";
                  options.AccessDeniedPath = "/Auth/AccessDenied";
                  options.SlidingExpiration = true;
              });
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
