using be_quanlytour.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add cors for react call api
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod().AllowAnyOrigin();
           
        });
    options.AddPolicy("AllowDeploy",
       builder =>
       {

           builder.WithOrigins("https://huflittravel.vercel.app")
                 .AllowAnyHeader()
                 .AllowAnyMethod().AllowAnyOrigin();
       });
});

//
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        /*        ValidateLifetime = true,

                ValidIssuer = "your-issuer", // Thay thế bằng thông tin của bạn
                ValidAudience = "your-audience", // Thay thế bằng thông tin của bạn*/
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("zzzzhdz02x1ta1urertv01vwxchjb4h1")) // Thay thế bằng khóa bí mật của bạn
    };
});

//
builder.Services.AddDbContext<QltourDuLichContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Db"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowReactApp");
app.UseCors("AllowDeploy");

app.MapControllers();

app.Run();
