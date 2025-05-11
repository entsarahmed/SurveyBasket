using MapsterMapper;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using SurveyBasket.Api.Contracts.Validation;
using SurveyBasket.Api.Persistence;
using System.Reflection;

namespace SurveyBasket.Api;

public static class DependencyInjection
{ 
    public static  IServiceCollection AddDependencies(this IServiceCollection services,IConfiguration configuration)
    {

        // Add services to the container.

        services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();
        //Add Mapster
        #region Swagger
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());
        #endregion

        #region Mapster
        services.AddSingleton<IMapper>(new Mapper(mappingConfig));
        services.AddMapster();
        #endregion

        #region Service
        services.AddScoped<IPollService, PollService>();
        #endregion

        #region Fluent Validation
        services.AddScoped<IValidator<CreatePollRequest>, CreatePollRequestValidator>();
        //services.AddValidatorsFromAssemblyContaining<Program>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        #endregion

        #region Allow Dependence Injection for  ConnectionString?
        var connectionString = configuration.GetConnectionString("DefaultConnection") 
       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        #endregion
        
        return services;
    }
}
