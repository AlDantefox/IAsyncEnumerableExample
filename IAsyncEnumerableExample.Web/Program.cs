using IAsyncEnumerableExample.Web.Repository;
using Microsoft.AspNetCore.Server.IISIntegration;

var builder = WebApplication.CreateBuilder(args);

string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

var configuration = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json")
    .AddEnvironmentVariables()
    .Build();

try
{
    var port = configuration.GetValue<int>("VitePortNumber", 5555);
    builder.Services.AddAuthentication(IISDefaults.AuthenticationScheme);
    builder.Services.AddAuthorization();
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
            builder.SetIsOriginAllowed(_ => true)
                .WithOrigins($"http://localhost:{port}")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
    });
    // Add services to the container.
    builder.Services.AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNameCaseInsensitive = true);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddHttpContextAccessor();
    //DI
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    if (builder.Environment.IsStaging() || builder.Environment.IsProduction())
    {
        // In production, the Vite files will be served from this directory
        builder.Services.AddSpaStaticFiles(configuration =>
        {
            configuration.RootPath = "ClientApp/dist";
        });
    }
    //
    var app = builder.Build();
    app.UseStaticFiles();
    if (builder.Environment.IsStaging() || builder.Environment.IsProduction())
    {
        app.UseSpaStaticFiles();
    }
    else
    {
        app.UseCors();
    }
    app.UseRouting();
    app.UseAuthorization();
    app.UseAuthentication();

#pragma warning disable ASP0014
    //every other solution didn't work
    app.UseEndpoints(endpoints => endpoints.MapControllers());
#pragma warning restore ASP0014

    app.UseSpa(spa =>
    {
        if (app.Environment.IsDevelopment())
        {
            spa.UseProxyToSpaDevelopmentServer($"http://localhost:{port}");
        }
    });
    app.Run();
}
catch (Exception e)
{
    //Log etc
}
