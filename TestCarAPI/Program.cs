using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TestCarAPI.Context;
using TestCarAPI.Repositories;
using TestCarAPI.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var name = builder.Configuration.GetSection("ApiTitle").Value;
var version = builder.Configuration.GetSection("ApiVersion").Value;

builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = name, Version = version });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

var app = builder.Build();

{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DapperContext>();
    await context.Init();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelExpandDepth(-1); //'hide' schema info in Swagger UI
        c.SwaggerEndpoint($@"/swagger/{version}/swagger.json", name);
    });
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();