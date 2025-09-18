using MapServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Mongo settings
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// Register Services
builder.Services.AddSingleton<PolygonService>();
builder.Services.AddSingleton<ObjectService>();

// Controllers
builder.Services.AddControllers();

// ---- CORS ----
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // הכתובת של ה-React client
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ---- שימוש ב-CORS ----
app.UseCors("AllowClient");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
