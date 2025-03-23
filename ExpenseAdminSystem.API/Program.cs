using DotNetEnv;
using ExpenseAdminSystem.Model.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Allow Angular frontend
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Register repositories for dependency injection
builder.Services.AddScoped<UserRepository, UserRepository>();
builder.Services.AddScoped<ExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<CurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<CategoryRepository, CategoryRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowFrontend");
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
