using CollectInfo.Business;
using CollectInfo.Domain.Abstractions;
using CollectInfo.Domain.Utils;
using CollectInfo.Infrastructure;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IGitHubRepositoryService, GitHubRepositoryService>();
builder.Services.AddScoped<IGitHubRepository, GitHubRepository>();

builder.Services.AddScoped(setting =>
{
    var config = setting.GetRequiredService<IConfiguration>();
    var appSetting = config.GetSection("AppSettings").Get<AppSettings>();
    appSetting.TokenAccessRepository = builder.Configuration["TokenAccessRepository"];
    return appSetting;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(doc =>
{
    doc.SwaggerDoc("v1", new OpenApiInfo { Title = "Collect info from git repository" , Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    doc.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
