using Microsoft.AspNetCore.Builder;
using Scalar.AspNetCore;
using SurveyBasket.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //  app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();//(options => options.SwaggerEndpoint("/openapi/v1.json","v1"));
    // app.MapScalarApiReference();
    

}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
