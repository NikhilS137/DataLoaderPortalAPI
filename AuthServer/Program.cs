using AuthServer.Models;
using AuthServer.Services;
using AuthServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DBDataLoaderPortalContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

//builder.Services.AddSingleton<ITokenService>(new TokenService());


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


//app.MapPost("/Login", object (UserValidationModel request, HttpContext http, ITokenService tokenService) =>
//{
//    var user = request.ValidateCredentials(request.userName, request.password);

//    if (user != null)
//    {
//        var token = tokenService.BuildToken(builder.Configuration["Jwt:Key"],
//                                              builder.Configuration["Jwt:Issuer"],
//                                              new[]
//                                              {
//                                                  builder.Configuration["Jwt:Aud1"],
//                                                      },
//                                              request.userName);
//        dynamic result = new {
//            Token = token,
//            IsAuthenticated = true,
//            User = user
//        };

//        return result;
//        //return new
//        //{
//        //    Token = token,
//        //    IsAuthenticated = true,
//        //    User = user

//        //};
//    }
//    else
//    {
//            return BadRequest();
//    }
  
//}).WithName("Login");

//object BadRequest()
//{
//    return BadRequest();
//}

app.Run();


//internal record UserValidationRequestModel([Required] string UserName, [Required] string Password);

//internal interface ITokenService
//{
//    string BuildToken(string key, string issuer, IEnumerable<string> audience, string userName);
//}

//internal class TokenService : ITokenService
//{
//    private TimeSpan ExpiryDuration = new TimeSpan(20, 30, 0);

//    public string BuildToken(string key, string issuer, IEnumerable<string> audience, string userName)
//    {
//        var claims = new List<Claim>
//        {
//             new Claim(JwtRegisteredClaimNames.UniqueName, userName),
//          };

//        claims.AddRange(audience.Select(aud => new Claim(JwtRegisteredClaimNames.Aud, aud)));

//        var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
//        var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256Signature);
//        var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
//            expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);
//        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
//    }
//}
