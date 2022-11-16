using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using DataAccess;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("dbConnection"),
                                                                    sinkOptions: new MSSqlServerSinkOptions{
                                                                                TableName = "Logs",
                                                                                AutoCreateSqlTable = true
                                                                    }));
builder.Services.AddScoped<ConnectionFactory>(ctx => new ConnectionFactory(builder.Configuration.GetConnectionString("dbConnection")));
builder.Services.AddScoped<IAuthenticationRepo, AuthenticationRepo>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<IAdminRepo, AdminRepo>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<IProfileRepo, ProfileRepo>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<BuyTroopService>();
builder.Services.AddScoped<BuyTroopRepo>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configuring JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
        ValidateIssuer = false,
        ValidateAudience = false,
        //ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

// Enabling Cors
// Able to fetch data from localhost(API) to localhost(Angular)
builder.Services.AddCors(options =>

    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
        }
    )

);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

// Applies JWT Authentication Middleware
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
