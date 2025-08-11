using API;
using BL.DependencyInjections;
using DAL.Context;
using DTO.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddSingleton<IResourceLocalizer, ResourceLocalizer>();

ConfigureServices(builder.Services, configuration);

var app = builder.Build();

ConfigureMiddleware(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration config)
{
    services.RegisterServices();

    services.AddControllers();

    services.AddEndpointsApiExplorer();
    ConfigureSwagger(services);
    ConfigureSecurityPolicies(services);

    services.Configure<RequestLocalizationOptions>(options =>
    {
        var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("tr-TR") };
        options.DefaultRequestCulture = new RequestCulture(config["Localization:DefaultCulture"] ?? "en-US");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });
}

void ConfigureSwagger(IServiceCollection services)
{
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "StockApp API V1",
            Version = "v1"
        });
        c.CustomSchemaIds(CustomSchemaIdStrategy.GetSchemaId);
    });
}

void ConfigureSecurityPolicies(IServiceCollection services)
{
    services.AddCors(options =>
    {
        options.AddPolicy("DefaultPolicy", builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });

    services.AddHsts(options =>
    {
        options.MaxAge = TimeSpan.FromDays(365);
        options.IncludeSubDomains = true;
        options.Preload = true;
    });
}

void ConfigureMiddleware(WebApplication app)
{
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
    app.UseRequestLocalization(locOptions.Value);

    app.UseRouting();

    app.UseCors("DefaultPolicy");

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "StockApp API V1");
            c.RoutePrefix = string.Empty;
        });
    }

    app.MapControllers();
}
