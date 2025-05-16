using Microsoft.AspNetCore.Builder;
using Scalar.AspNetCore;
using SurveyBasket.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
     app.MapOpenApi();
   //  app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json","v1"));
     app.MapScalarApiReference();
    

}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapIdentityApi<ApplicationUser>();

app.MapControllers();

app.Run();
