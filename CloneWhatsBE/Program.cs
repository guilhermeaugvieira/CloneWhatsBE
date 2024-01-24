using CloneWhatsBE.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Net.Sockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(["http://localhost:4200"]);
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowCredentials();
        policy.SetIsOriginAllowed(_ => true);
    })
);

// Add Jwt Settings Globally
builder.Services.Configure<JwtSettingsOptions>(builder.Configuration.GetRequiredSection(JwtSettingsOptions.SectionName));
builder.Services.AddSingleton(provider => provider.GetRequiredService<IOptions<JwtSettingsOptions>>().Value);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var jwtSettingsOptionsSection = builder.Configuration.GetRequiredSection(JwtSettingsOptions.SectionName);
    var jwtSettingsOptions = jwtSettingsOptionsSection.Get<JwtSettingsOptions>();
    
    var secretByte = Encoding.UTF8.GetBytes(jwtSettingsOptions.Secret);
    var secretKey = new SymmetricSecurityKey(secretByte);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = secretKey,
        ValidateLifetime = false,
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
