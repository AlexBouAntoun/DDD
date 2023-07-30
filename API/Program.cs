using System.Reflection;
using Application.Handlers;
using Application.Queries;
using Persistance;

using Application.Services;
using Domain;
using Firebase.Auth;
using Firebase.Auth.Providers;
 using FirebaseAdmin;
 using Persistance;
 using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using User = Firebase.Auth.User;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS",
//      @"C:\Users\alex_\RiderProjects\DDD\Infrastructure\university-management-sy-d79a1-firebase-adminsdk-zaorj-52872fd400.json");
//  builder.Services.AddSingleton(FirebaseApp.Create());

builder.Services.AddControllers();

// builder.Services.AddSession();
 // var app = builder.Build();
// app.UseSession();
// app.Use(async (context, next) =>
// {
//     var token = context.Session.GetString("token");
//     if (!string.IsNullOrEmpty(token))
//     {
//         context.Request.Headers.Add("Authorization", "Bearer " + token);
//     }
//     await next();
// });

// builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).GetTypeInfo().Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCoursesQuery).Assembly));

var firebaseProjectName = "university-management-sy-d79a1";
builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig
{
    ApiKey = "AIzaSyCtFX_ewa-JQGeC9epLpItJzrLXYZRGnaE",
    AuthDomain = $"{firebaseProjectName}.firebaseapp.com",//university-management-sy-d79a1.firebaseapp.com.firebaseapp.com
    Providers = new FirebaseAuthProvider[]
    {
        new EmailProvider(),
        new GoogleProvider()
    }
}));
 builder.Services.AddSingleton<IFirebaseAuthService, FirebaseAuthService>(); 
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.Authority = $"https://securetoken.google.com/{firebaseProjectName}";
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidIssuer = $"https://securetoken.google.com/{firebaseProjectName}",
//             ValidateAudience = true,
//             ValidAudience = firebaseProjectName,
//             ValidateLifetime = true
//         };
//     });

builder.Services.AddControllers()
    .AddOData(options => options
        .AddRouteComponents("odata", GetEdmModel())
        .Select()
        .Filter()
        .OrderBy()
        .SetMaxTop(2)//
        .Count()
        .Expand()
    );

builder.Services.AddDbContext<dbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.Run();

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    //builder.EntitySet<User>("Users");
    builder.EntitySet<Role>("Roles");
    builder.EntitySet<Course>("Courses");
    return builder.GetEdmModel();
}