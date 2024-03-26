using BoanMusicApp.BLL;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the PremiumSubscriptionBLL class with the connection string injected
builder.Services.AddScoped<PremiumSubscriptionBLL>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("MyDbConnectionString");
    return new PremiumSubscriptionBLL(connectionString);
});

// Register the UserBLL class with the connection string injected
builder.Services.AddScoped<UserBLL>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("MyDbConnectionString");
    return new UserBLL(connectionString);
});

var app = builder.Build();

// Enable middleware to serve generated Swagger as a JSON endpoint.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
