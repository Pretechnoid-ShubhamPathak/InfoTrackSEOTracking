using Microsoft.EntityFrameworkCore;
using Backend.Authorization;
using Backend.Helpers;
using Backend.Services;
using Backend.Businessobj;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;
 
    // use sql server db in production and development
    if (env.IsProduction())
        services.AddDbContext<DataContext>();
    else
        services.AddDbContext<DataContext>();
 
    services.AddCors();
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddHttpClient();
    services.AddSwaggerGen(c=>
    {
        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
            Title="InfoTrackSEOTracking",
            Version="v1"
        });
        c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme{
            Name="Authorization",
            Type=Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
            Scheme="Bearer",
            BearerFormat="JWT",
            In=Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description="Enter JWT token with bearer format :- 'Bearer(space)(Token-String)'."
        });
        c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement{{
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme{
                Reference=new Microsoft.OpenApi.Models.OpenApiReference{
                    Type=Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
        });
    }
    );

    // configure automapper with all automapper profiles from this assembly
    services.AddAutoMapper(typeof(Program));

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
    services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

    //Mail Services
    services.AddTransient<IMailService, MailService>();

    // configure DI for application services
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<ISearchHistoryManager, SearchHistoryManager>();
    
    services.AddScoped<IUserBO, UserBO>();
    services.AddScoped<ISearchBO, SearchBO>();
}

var app = builder.Build();

// migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();    
}

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.UseSwagger();
    app.UseSwaggerUI();

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}

app.Run("http://localhost:4000/");