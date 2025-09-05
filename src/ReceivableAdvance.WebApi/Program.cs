using Asp.Versioning;
using Microsoft.OpenApi.Models;
using ReceivableAdvance.Setup;
using ReceivableAdvance.WebApi.Endpoints.ReceivableAdvanceRequests;


var builder = WebApplication.CreateBuilder(args);

builder.Services.SetupReceivableAdvance();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    })
    .AddApiExplorer(options =>
      {
          options.GroupNameFormat = "'v'VVV"; // Format for OpenAPI document names
          options.SubstituteApiVersionInUrl = true; // Replace {version:apiVersion} in URLs
      });

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Advance Request API",
        Version = "v1"
    });

    options.CustomSchemaIds(type =>
    {
        if (type.IsNested && type.DeclaringType is not null)
        {
            return $"{type.DeclaringType.Name.Replace("Endpoint", ".")}{type.Name}";
        }
        return type.Name;
    });
});

var app = builder.Build();

var versions = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1, 0))
    .Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapReceivableAdvanceRequestsGroup(versions);

app.Run();

