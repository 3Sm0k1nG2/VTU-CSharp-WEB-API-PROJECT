using Microsoft.EntityFrameworkCore;
using WorkOrders_DAL;
using WorkOrders_DAL.Interfaces;
using WorkOrders_DAL.Mappers.Excel;
using WorkOrders_DAL.DbContexts;
using WorkOrders_DAL.Mappers.Entity.Base;
using WorkOrders_DAL.Repositories;
using WorkOrders_DAL.Interfaces.Repositories;
using WorkOrders_DAL.Mappers.Repositories;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WorkOrderApi.Errors;
using WorkOrderApi.Filters;
using WorkOrders_DAL.Interfaces.Mappers.Entity;
using WorkOrders_DAL.Interfaces.Mappers.Entity.Base;
using WkHtmlToPdfDotNet.Contracts;
using WkHtmlToPdfDotNet;
using WorkOrderApi.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WorkOrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(Constants.SQL_CONNECTION_STRING_DEFAULT)));

builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //{
    //    Description = "JWT Authorization using bearer scheme",
    //    Name = "Authorize",
    //    In = ParameterLocation.Header,
    //    Type = SecuritySchemeType.ApiKey
    //});
    //options.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme{Reference = new OpenApiReference
    //        {
    //            Id = "Bearer",
    //            Type = ReferenceType.SecurityScheme
    //        }}, new List<string>() }
    //});
});

builder.Services.AddSingleton<ProblemDetailsFactory, WorkOrderApiProblemDetailsFactory>();

// Data Scopes
builder.Services.AddScoped<WorkOrderDbContext, WorkOrderDbContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<WorkOrderController, WorkOrderController>();

// Excel Scope
builder.Services.AddScoped(typeof(IEntityMapper<,,>), typeof(EntityMapper<,,>));
builder.Services.AddScoped<WorkOrderExcelMapper, WorkOrderExcelMapper>();

builder.Services.AddScoped<IWorkOrderRepository, WorkOrderRepository>();
builder.Services.AddScoped<IWorkOrderMapper, WorkOrderMapper>();
builder.Services.AddScoped<WorkOrderRequiredRepositories, WorkOrderRequiredRepositories>();

// Register DinkToPdf to services.
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

// Add In-Memory caching
builder.Services.AddMemoryCache();

// Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<WorkOrderDbContext>()
    .AddDefaultTokenProviders();

// Authentication
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Fixes 'System.NotSupportedException: No data is available for encoding 1252'
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

app.Run();
