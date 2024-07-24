using Microsoft.IdentityModel.Tokens;
using System.Text;
using testNewAPI.Config;
using testNewAPI.Models.Connection;
using testNewAPI.Repository;
using testNewAPI.Repository.Contract;
using testNewAPI.Services;
using testNewAPI.Services.Contract;

var builder = WebApplication.CreateBuilder(args);
// Basic Auth
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add(new BasicAuth());
//});
// Add services to the container.
// Add Connectsting
ClsConstring.Constr = builder.Configuration.GetSection("ConnectionString").Value.ToString();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IUserLoginRepository, UserLoginRepository>();
builder.Services.AddScoped<IUserLoginService, UserLoginService>();
// AutoMapper
builder.Services.AddAutoMapper(typeof(DtoMapping));
// Jwt 
builder.Services.AddAuthentication()
.AddJwtBearer("JwtBearer", jwtBearerOptions =>
{
    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZXIiOiJJc3N1ZXIiLCJVc2VybmFtZSI6IkphdmFJblVzZSIsImV4cCI6MTcyMTM1MTM0NywiaWF0IjoxNzIxMzUxMzQ3fQ.5UgVI840kT-U8mxqbH_o8gn9normQHQrl9XgNM8x6qc")),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
    };
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
