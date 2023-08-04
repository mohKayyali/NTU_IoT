using NTU.IoT.DataAccess;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NTU.IoT.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using NTU.IoT.DataAccess.DbInitializer;

Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");
Console.WriteLine($"EnvironmentEnvironmentEnvironmentEnvironmentEnvironment");

var influxDBSection = app.Configuration.GetSection("InfluxDB");

// Fetch the token value from the configuration
var influxDBToken = influxDBSection["Token"];


Console.WriteLine(influxDBToken.ToString());

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDBContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddSingleton<InfluxDBService>();
builder.Services.AddControllersWithViews().AddRazorPagesOptions(options => {
    options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "");
});

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

SeedDatabase();
app.MapRazorPages();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



await app.RunAsync();



void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}