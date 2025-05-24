using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using SurveyBasket.Api.Authentication;
using SurveyBasket.Api.Contracts.Polls;
using SurveyBasket.Api.Persistence;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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
        services.AddScoped<IValidator<PollRequest>, PollRequestValidator>();
        //services.AddValidatorsFromAssemblyContaining<Program>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        #endregion

        #region Allow Dependence Injection for  ConnectionString?
        var connectionString = configuration.GetConnectionString("DefaultConnection") 
       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        #endregion

        #region Authenication
        services.AddScoped<IAuthService, AuthService>();
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
       var JwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        services.AddSingleton<IJwtProvider, JwtProvider>();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer( o =>
        {
            o.SaveToken=true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer =true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings?.Key!)),
                ValidIssuer = JwtSettings?.Issuer,
                ValidAudience = JwtSettings?.Audience,
            };
        });
      
        #endregion

        return services;
    }
}
