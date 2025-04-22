using Scalar.AspNetCore;
using SurveyBasket.Api.Middlewares;
using SurveyBasket.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//builder.Services.AddScoped<IOS,WindowsOsService>();
builder.Services.AddTransient<IOperationTransient, WindowsOsService>();
builder.Services.AddScoped<IOperationScoped, WindowsOsService>();
builder.Services.AddSingleton<IOperationSingleton,WindowsOsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
   // app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json","v1"));
   app.MapScalarApiReference();
}
app.UseMiddleware<Middleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
