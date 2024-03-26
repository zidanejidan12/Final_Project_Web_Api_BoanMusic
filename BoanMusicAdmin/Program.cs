using BoanMusicApp.BLL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure the connection string
var connectionString = builder.Configuration.GetConnectionString("MyDbConnectionString");

// Register BLL services with dependency injection
builder.Services.AddScoped<UserBLL>(provider => new UserBLL(connectionString));
builder.Services.AddScoped<TrackBLL>(provider => new TrackBLL(connectionString));
builder.Services.AddScoped<ArtistBLL>(provider => new ArtistBLL(connectionString));

// Pass the connection string to the PremiumSubscriptionBLL constructor
builder.Services.AddScoped<PremiumSubscriptionBLL>(provider => new PremiumSubscriptionBLL(connectionString));

// Add authentication services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login"; // Specify the login page
        options.AccessDeniedPath = "/Home/AccessDenied"; // Specify the access denied page
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting(); // <-- Ensure this is called before authentication and authorization

// Add authentication before authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controller routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}"); // Set the default route to the login page

app.Run();
