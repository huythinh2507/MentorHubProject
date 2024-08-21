using HotChocolate.AspNetCore;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using YPP.MH.BusinessLogicLayer.Services.PostManagement;
using YPP.MH.DataAccessLayer.Models;
using YPP.MH.DataAccessLayer.Repositories.Base;
using YPP.MH.BusinessLogicLayer.Services.Query;
using Microsoft.OpenApi.Models;
using System.Reflection;
using YPP.MH.BusinessLogicLayer.Services.UserManagement;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using YPP.MH.DigitalAssetManagement;
using YPP.MH.BusinessLogicLayer.Services.CourseManagement;
using Microsoft.Identity.Client;
using YPP.MH.BusinessLogicLayer.Services.TenantManagement;
using YPP.MH.BusinessLogicLayer.Services.SpaceService;

var builder = WebApplication.CreateBuilder(args);

// Register the DbContextFactory
builder.Services.AddPooledDbContextFactory<MHDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// Register your repositories
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = int.MaxValue;
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = int.MaxValue;
});

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue;
    x.MultipartHeadersLengthLimit = int.MaxValue;
});

// Register your services
builder.Services.AddScoped<PostService, PostService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<Dam>();
builder.Services.AddScoped<TenantService>();
builder.Services.AddScoped<SpaceService>();
// Add GraphQL services
builder.Services
    .AddGraphQLServer()
    .AddQueryType<PostQuery>(); // Replace with your query type

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:4200")
            .WithOrigins("http://10.6.20.146:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

// Add other services (e.g., controllers, AutoMapper, etc.)
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();
app.UseAuthorization();
// Map GraphQL endpoint
app.MapGraphQL();

app.MapControllers();

await app.RunAsync();
