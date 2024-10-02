using Microsoft.OpenApi.Models;
using MTCA.API.Configuration;
using MTCA.Infrastructure;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using MTCA.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Host.AddConfigurations();

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

builder.Services.AddControllers();
//await builder.Services.AddAndMigrateTenantDatabases(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    AddSwaggerDocumentation(config);
    config.CustomSchemaIds(x => x.FullName);

    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    config.AddSecurityRequirement(new OpenApiSecurityRequirement {
                            {
                                new OpenApiSecurityScheme {
                                    Reference = new OpenApiReference {
                                        Type = ReferenceType.SecurityScheme,
                                            Id = "Bearer"
                                    }
                                },
                                new string[] {}
                            }
                        });


});

var app = builder.Build();
await app.Services.InitializeDatabasesAsync();

app.UseInfrastructure(builder.Configuration);

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DocExpansion(DocExpansion.None);
    c.DisplayRequestDuration();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
//ApplicationDataSeed.Seed(app);

app.Run();
static void AddSwaggerDocumentation(SwaggerGenOptions o)
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
}