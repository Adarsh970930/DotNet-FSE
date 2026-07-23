using hands_on3.Filters;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ✅ Register controllers and add global exception filter
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomExceptionFilter>();
});

// ✅ Register custom filters with DI container
builder.Services.AddScoped<CustomAuthFilter>();
builder.Services.AddScoped<CustomExceptionFilter>();

// ✅ Swagger service setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Swagger Demo",
        Version = "v1",
        Description = "Demo of WebAPI with Filters and Models",
        Contact = new OpenApiContact
        {
            Name = "John Doe",
            Email = "john@xyzmail.com",
            Url = new Uri("https://www.example.com")
        }
    });
});

// ✅ Build the app
var app = builder.Build();

// ✅ Enable Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Demo");
});

// ✅ Add middleware
app.UseAuthorization();
app.MapControllers();

// ✅ Run the application
app.Run();