using BoanMusicApp.BLL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false);

// Retrieve the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("MyDbConnectionString");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
});

// Register BLL services with dependency injection
builder.Services.AddScoped<UserBLL>(provider => new UserBLL(connectionString));
builder.Services.AddScoped<TrackBLL>(provider => new TrackBLL(connectionString));
builder.Services.AddScoped<ArtistBLL>(provider => new ArtistBLL(connectionString));

// Pass the connection string to the PremiumSubscriptionBLL constructor
builder.Services.AddScoped<PremiumSubscriptionBLL>(provider => new PremiumSubscriptionBLL(connectionString));

// Configure JWT authentication
// Configure JWT authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["Secret"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RegularPolicy", policy => policy.RequireRole("regular"));
    options.AddPolicy("PremiumPolicy", policy => policy.RequireRole("premium"));
});

var app = builder.Build();

// Enable authentication
app.UseAuthentication();

// Enable authorization
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
