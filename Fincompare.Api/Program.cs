using Fincompare.Api.Middleware;
using Fincompare.Api.Services;
using Fincompare.Application;
using Fincompare.Application.Repositories;
using Fincompare.Infrastructure;
using Fincompare.Persitence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureSwaggerServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("Fincompare-CorsPolicy",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Fincompare-CorsPolicy");

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
