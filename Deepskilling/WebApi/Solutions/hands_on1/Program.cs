var builder = WebApplication.CreateBuilder(args);

// Register controller services
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();