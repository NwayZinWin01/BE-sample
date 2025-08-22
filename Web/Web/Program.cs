using Data_Access;
using Data_Access.Interface;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Web;

var builder = WebApplication.CreateBuilder(args);

//add dbcontext
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorNumbersToAdd: null)
    ).EnableDetailedErrors()
);


//add controller
builder.Services.AddControllers();

builder.Services.AddRepositories(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// add swaggergen
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
